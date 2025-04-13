namespace Storeify.Web.Controllers
{
    [Authorize(Roles = $"{AppRoles.Admin},{AppRoles.Manager},{AppRoles.InventoryManager}")]

    public class ProductsController : Controller
    {
        private readonly IService<Product> _productService;
        private readonly IService<Category> _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        public ProductsController(IService<Product> productService, IMapper mapper, IService<Category> categoryService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _mapper = mapper;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }
        private List<String> _allowedExtensions = new() { ".jpg", ".jpeg", ".png" };
        private int _maxAllowedSize = 2097152;

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var product = await _productService.GetAllIncludingAsync(nameof(Category));
            var modelList = _mapper.Map<List<ProductViewModel>>(product);
            return View(modelList);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetWithIncludingAsync(id, nameof(Category));
            if (product is null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<ProductViewModel>(product);

            if (viewModel is null)
                return NotFound();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View("Form", await PopulateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(viewName: "Form", PopulateViewModel(model));

            var product = _mapper.Map<Product>(model);

            if (model.Image is not null)
            {
                var extension = Path.GetExtension(model.Image.FileName);
                if (!_allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError(nameof(model.Image), Errors.NotAllowedExtension);
                    return View(viewName: "Form", PopulateViewModel(model));
                }

                if (model.Image.Length > _maxAllowedSize)
                {
                    ModelState.AddModelError(nameof(model.Image), Errors.MaxSize);
                    return View(viewName: "Form", PopulateViewModel(model));
                }
                var imageName = $"{Guid.NewGuid()}{extension}";

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products", imageName);
                using var stream = System.IO.File.Create(path);
                await model.Image.CopyToAsync(stream);
                product.ImageUrl = imageName;
                product.CreatedOn = DateTime.Now;
                product.IsDeleted = !model.IsDeleted;
                product.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            }

            await _productService.CreateAsync(product);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetWithIncludingAsync(id, nameof(Category));
            if (product is null)
            {
                return NotFound();
            }
            var model = _mapper.Map<ProductViewModel>(product);
            var viewModel = await PopulateViewModel(model);
            viewModel.IsDeleted = !model.IsDeleted;
            return View("Form", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(viewName: "Form", await PopulateViewModel(model));
            var product = await _productService.GetWithIncludingAsync(model.Id, nameof(Category));
            if (product is null)
            {
                return NotFound();
            }

            if (model.Image is not null)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products", product.ImageUrl);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                var extension = Path.GetExtension(model.Image.FileName);
                if (!_allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError(nameof(model.Image), Errors.NotAllowedExtension);
                    return View(viewName: "Form", await PopulateViewModel(model));
                }

                if (model.Image.Length > _maxAllowedSize)
                {
                    ModelState.AddModelError(nameof(model.Image), Errors.MaxSize);
                    return View(viewName: "Form", PopulateViewModel(model));
                }
                var imageName = $"{Guid.NewGuid()}{extension}";

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products", imageName);
                using var stream = System.IO.File.Create(path);
                await model.Image.CopyToAsync(stream);
                model.ImageUrl = imageName;

            }
            else if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                model.ImageUrl = product.ImageUrl;
            }
            model.CreatedOn = product.CreatedOn;
            model.CreatedById = product.CreatedById;
            product = _mapper.Map(model, product);
            product.UpdatedOn = DateTime.Now;
            product.UpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            product.IsDeleted = !model.IsDeleted;

            await _productService.UpdateAsync(product);
            return RedirectToAction(nameof(Index));
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product is null)
                return NotFound();

            if (!product.IsDeleted)
            {
                product.IsDeleted = !product.IsDeleted;
                product.DeletedOn = DateTime.Now;
               product.DeletedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            }
            else
            {
                product.IsDeleted = !product.IsDeleted;
                product.DeletedOn = null;
                product.DeletedById = null;
                product.DeletedReason = null;
            }

            await _productService.UpdateAsync(product);

            return Ok(product.UpdatedOn.ToString());
        }
        private async Task<ProductViewModel> PopulateViewModel(ProductViewModel? model = null)
        {
            ProductViewModel viewModel = model is null ? new ProductViewModel() : model;
            var categories = await _categoryService.GetAllActiveAsync();
            var sortedcategories = categories.ToList().OrderBy(s => s.Name).ToList();
            viewModel.Categories = _mapper.Map<IEnumerable<SelectListItem>>(sortedcategories);
            return viewModel;
        }

        public async Task<IActionResult> AllowItem(ProductViewModel model)
        {
            var product = await _productService.GetSingleAsync(b => b.Barcode.Trim() == model.Barcode.Trim());
            var isAllowed = product is null || product.Id.Equals(model.Id);
            return Json(isAllowed);
        }
    }
}
