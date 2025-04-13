
namespace Storeify.Web.Controllers
{
   [Authorize(Roles = AppRoles.Admin)]
    public class BranchesController : Controller
    {
        private readonly IService<Branch> _branchService;
        private readonly IService<Store> _storeService;
        private readonly IMapper _mapper;
        public BranchesController(ApplicationDbContext context, IService<Branch> branchService, IMapper mapper, IService<Store> storeService)
        {
            _branchService = branchService;
            _mapper = mapper;
            _storeService = storeService;
        }

        // GET: Branches
        public async Task<IActionResult> Index()
        {
            var branches = await _branchService.GetAllIncludingAsync("Store");
            var modelList = _mapper.Map<List<BranchViewModel>>(branches);
            return View(modelList);

        }

        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> Create()
        {
            return PartialView("_Form", await PopulateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BranchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Form", await PopulateViewModel(model));
            }
            //model.CreatedById = 1;
            var branch = _mapper.Map<Branch>(model);
            await _branchService.CreateAsync(branch);
            var viewModel = _mapper.Map<BranchViewModel>(branch);
            if (viewModel.StoreId != 0)
            {
                viewModel.Store = await _storeService.GetByIdAsync(viewModel.StoreId);
            }
            return PartialView("_BranchRow", viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> Edit(int id)
        {
            var branch = await _branchService.GetWithIncludingAsync(id, nameof(Store))!;

            if (branch is null)
                return NotFound();

            var viewModel = _mapper.Map<BranchViewModel>(branch);
            if (viewModel.StoreId != 0)
            {
                viewModel.Store = await _storeService.GetByIdAsync(viewModel.StoreId);
            }
            return PartialView("_Form", await PopulateViewModel(viewModel));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BranchViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView("_Form", await PopulateViewModel(model));

            var branch = await _branchService.GetByIdAsync(model.Id);

            if (branch is null)
                return NotFound();

            branch.Name = model.Name;
            branch.StoreId = model.StoreId;
            branch.Phone = model.Phone;
            //branch = _mapper.Map<Branch>(model);
            branch.UpdatedOn = DateTime.Now;
            branch.UpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            await _branchService.UpdateAsync(branch);

            var viewModel = _mapper.Map<BranchViewModel>(branch);
            if (viewModel.StoreId != 0)
            {
                viewModel.Store = await _storeService.GetByIdAsync(viewModel.StoreId);
            }
            return PartialView("_BranchRow", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var branch = await _branchService.GetByIdAsync(id);

            if (branch is null)
                return NotFound();

            if (!branch.IsDeleted)
            {
                branch.IsDeleted = !branch.IsDeleted;
                branch.DeletedOn = DateTime.Now;
                branch.DeletedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            }
            else
            {
                branch.IsDeleted = !branch.IsDeleted;
                branch.DeletedOn = null;
                branch.DeletedById = null;
                branch.DeletedReason = null;
            }

            await _branchService.UpdateAsync(branch);

            return Ok(branch.UpdatedOn.ToString());
        }

        private async Task<BranchViewModel> PopulateViewModel(BranchViewModel? model = null)
        {
            BranchViewModel viewModel = model is null ? new BranchViewModel() : model;
            var stores = await _storeService.GetAllActiveAsync();
            var sortedStores = stores.ToList().OrderBy(s => s.Name).ToList();
            viewModel.Stores = _mapper.Map<IEnumerable<SelectListItem>>(sortedStores);
            return viewModel;
        }

        public async Task<IActionResult> AllowItem(BranchViewModel model)
        {
            var branch = await _branchService.GetSingleAsync(b => b.Name.Trim() == model.Name.Trim() && b.StoreId == model.StoreId);
            // (b => b.Title == model.Title && b.AuthorId == model.AuthorId);
            var isAllowed = branch is null || branch.Id.Equals(model.Id);

            return Json(isAllowed);
        }
    }
}
