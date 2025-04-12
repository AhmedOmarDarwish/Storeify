using Microsoft.EntityFrameworkCore;
using Storeify.Core.Services;
using Storeify.Data.Entities;

namespace Storeify.Web.Controllers
{
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
                product.CreatedDate = DateTime.Now;
                product.IsDeleted = !model.IsDeleted;
                product.CreatedBy = 1;
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
            model.CreatedDate = product.CreatedDate;
            model.CreatedBy = product.CreatedBy;
            product = _mapper.Map(model, product);
            product.UpdatedDate = DateTime.Now;
            product.UpdatedBy = 1;
            product.IsDeleted = !model.IsDeleted;

            await _productService.UpdateAsync(product);
            return RedirectToAction(nameof(Index));
        }

        //// GET: Products/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products
        //        .Include(p => p.Category)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        //// GET: Products/Create
        //public IActionResult Create()
        //{
        //    ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Name");
        //    return View();
        //}

        //// POST: Products/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Barcode,Name,Description,ImageUrl,StockQuantity,Price,CategoryID,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,IsDeleted,DeletedBy,DeletedDate,DeletedReason")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(product);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryID);
        //    return View(product);
        //}

        //// GET: Products/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryID);
        //    return View(product);
        //}

        //// POST: Products/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Barcode,Name,Description,ImageUrl,StockQuantity,Price,CategoryID,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,IsDeleted,DeletedBy,DeletedDate,DeletedReason")] Product product)
        //{
        //    if (id != product.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(product);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProductExists(product.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryID);
        //    return View(product);
        //}

        //// GET: Products/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products
        //        .Include(p => p.Category)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var product = await _context.Products.FindAsync(id);
        //    if (product != null)
        //    {
        //        _context.Products.Remove(product);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ProductExists(int id)
        //{
        //    return _context.Products.Any(e => e.Id == id);
        //}

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
                product.DeletedDate = DateTime.Now;
                product.DeletedBy = 1;
            }
            else
            {
                product.IsDeleted = !product.IsDeleted;
                product.DeletedDate = null;
                product.DeletedBy = null;
                product.DeletedReason = null;
            }

            await _productService.UpdateAsync(product);

            return Ok(product.UpdatedDate.ToString());
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
