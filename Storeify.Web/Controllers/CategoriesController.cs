using Storeify.Data.Entities;

namespace Storeify.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IService<Category> _categoryService;
        private readonly IMapper _mapper;
        public CategoriesController(ApplicationDbContext context, IService<Branch> branchService, IMapper mapper, IService<Category> categoryService)
        {
            _context = context;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        // GET: Branches
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            var modelList = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
            return View(modelList);

        }

        [HttpGet]
        [AjaxOnly]
        public IActionResult Create()
        {
            return PartialView("_Form");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Form", model);
            }
            model.CreatedBy = 1;
            var category = _mapper.Map<Category>(model);
            await _categoryService.CreateAsync(category);
            var viewModel = _mapper.Map<CategoryViewModel>(category);
            return PartialView("_CategoryRow", viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category is null)
                return NotFound();

            var viewModel = _mapper.Map<CategoryViewModel>(category);

            return PartialView("_Form", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView("_Form", model);

            var category = await _categoryService.GetByIdAsync(model.Id);

            if (category is null)
                return NotFound();

            category.Name = model.Name;
            category.Description = model.Description;
            category.UpdatedDate = DateTime.Now;
            category.UpdatedBy = 1;

            await _categoryService.UpdateAsync(category);

            var viewModel = _mapper.Map<CategoryViewModel>(category);
            return PartialView("_CategoryRow", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category is null)
                return NotFound();

            if (!category.IsDeleted)
            {
                category.IsDeleted = !category.IsDeleted;
                category.DeletedDate = DateTime.Now;
                category.DeletedBy = 1;
            }
            else
            {
                category.IsDeleted = !category.IsDeleted;
                category.DeletedDate = null;
                category.DeletedBy = null;
                category.DeletedReason = null;
            }

                category.UpdatedDate = DateTime.Now;
            await _categoryService.UpdateAsync(category);

            return Ok(category.UpdatedDate.ToString());
        }
        public async Task<IActionResult> AllowItem(CategoryViewModel model)
        {
            var category =  await _categoryService.GetSingleAsync(c => c.Name.Trim() == model.Name.Trim());
            var isAllowed = category is null || category.Id.Equals(model.Id);

            return Json(isAllowed);
        }

    }
}
