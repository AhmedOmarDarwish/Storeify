﻿namespace Storeify.Web.Controllers
{
    [Authorize(Roles = $"{AppRoles.Admin},{AppRoles.InventoryManager}")]

    public class InventoriesController : Controller
    {
        private readonly IService<Branch> _branchService;
        private readonly IService<Inventory> _inventoryService;
        private readonly IMapper _mapper;
        public InventoriesController(IService<Branch> branchService, IMapper mapper, IService<Inventory> inventoryService)
        {
            _branchService = branchService;
            _mapper = mapper;
            _inventoryService = inventoryService;
        }

        public async Task<IActionResult> Index()
        {
            var inventories = await _inventoryService.GetAllIncludingAsync("Branch");
            var modelList = _mapper.Map<List<InventoryViewModel>>(inventories);
            return View(modelList);

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return PartialView("_Form", await PopulateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InventoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Form", await PopulateViewModel(model));
            }
            var inventory = _mapper.Map<Inventory>(model);
            await _inventoryService.CreateAsync(inventory);
            var viewModel = _mapper.Map<InventoryViewModel>(inventory);
            if (viewModel.BranchId != 0)
            {
                viewModel.Branch = await _branchService.GetByIdAsync(viewModel.BranchId);
            }
            return PartialView("_InventoryRow", viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> Edit(int id)
        {
            var inventory = await _inventoryService.GetWithIncludingAsync(id, nameof(Branch))!;

            if (inventory is null)
                return NotFound();

            var viewModel = _mapper.Map<InventoryViewModel>(inventory);
            if (viewModel.BranchId != 0)
            {
                viewModel.Branch = await _branchService.GetByIdAsync(viewModel.BranchId);
            }
            return PartialView("_Form", await PopulateViewModel(viewModel));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InventoryViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView("_Form", await PopulateViewModel(model));

            var inventory = await _inventoryService.GetByIdAsync(model.Id);

            if (inventory is null)
                return NotFound();

            inventory.Name = model.Name;
            inventory.BranchId = model.BranchId;
            inventory.UpdatedOn = DateTime.Now;
            inventory.UpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;


           await _inventoryService.UpdateAsync(inventory);

            var viewModel = _mapper.Map<InventoryViewModel>(inventory);
            if (viewModel.BranchId != 0)
            {
                viewModel.Branch = await _branchService.GetByIdAsync(viewModel.BranchId);
            }
            return PartialView("_InventoryRow", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var inventory = await _inventoryService.GetByIdAsync(id);
            if (inventory is null)
                return NotFound();

            if (!inventory.IsDeleted)
            {
                inventory.IsDeleted = !inventory.IsDeleted;
                inventory.DeletedOn = DateTime.Now;
                inventory.DeletedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            }
            else
            {
                inventory.IsDeleted = !inventory.IsDeleted;
                inventory.DeletedOn = null;
                inventory.DeletedById = null;
                inventory.DeletedReason = null;
            }

            await _inventoryService.UpdateAsync(inventory);

            return Ok(inventory.UpdatedOn.ToString());
        }

        private async Task<InventoryViewModel> PopulateViewModel(InventoryViewModel? model = null)
        {
            InventoryViewModel viewModel = model is null ? new InventoryViewModel() : model;
            var branches = await _branchService.GetAllActiveAsync();
            var sortedBranches = branches.ToList().OrderBy(s => s.Name).ToList();
            viewModel.Branches = _mapper.Map<IEnumerable<SelectListItem>>(sortedBranches);
            return viewModel;
        }

        public async Task<IActionResult> AllowItem(InventoryViewModel model)
        {
            var inventory = await _inventoryService.GetSingleAsync(b => b.Name.Trim() == model.Name.Trim() && b.BranchId == model.BranchId);
            var isAllowed = inventory is null || inventory.Id.Equals(model.Id);

            return Json(isAllowed);
        }
    }
}

