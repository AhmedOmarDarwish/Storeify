
namespace Storeify.Web.Controllers
{
    public class StoresController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IService<Store> _storeService;
        private readonly IMapper _mapper;
        private List<String> _allowedExtensions = new() { ".jpg", ".jpeg", ".png" };
        private int _maxAllowedSize = 2097152;

        public StoresController(IService<Store> storeService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _storeService = storeService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;

        }

        // GET: Stores
        public async Task<IActionResult> Index()
        {
            var store = await _storeService.GetSingleAsync();
            StoreViewModel viewModel;

            if (store == null)
            {
                viewModel = new StoreViewModel();
            }
            else
            {
                viewModel = _mapper.Map<StoreViewModel>(store);
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(StoreViewModel viewModel)
        {
            var store = _mapper.Map<Store>(viewModel);
            var storeData = await _storeService.GetSingleAsync();

            if (storeData is not null)
            {
                store.UpdatedBy = 1;
                store.UpdatedDate = DateTime.Now;

                if (viewModel.Image is not null)
                {

                    var (isValid, errorMessage, imageName) = await TrySaveImageAsync(viewModel.Image);
                    if (!isValid)
                    {
                        ModelState.AddModelError(nameof(viewModel.Image), errorMessage);
                        viewModel.LogoUrl = storeData.LogoUrl;
                        return View(nameof(Index), viewModel);
                    }

                    DeleteOldImage(storeData.LogoUrl);
                    store.LogoUrl = imageName;
                }
                else
                {
                    store.LogoUrl = storeData.LogoUrl;
                }

                await _storeService.UpdateAsync(store);
            }
            else
            {
                store.CreatedBy = 1;
                store.CreatedDate = DateTime.Now;

                if (viewModel.Image is not null)
                {
                    var (isValid, errorMessage, imageName) = await TrySaveImageAsync(viewModel.Image);
                    if (!isValid)
                    {
                        ModelState.AddModelError(nameof(viewModel.Image), errorMessage);
                        return View(nameof(Index), viewModel);
                    }

                    store.LogoUrl = imageName;
                }

                await _storeService.CreateAsync(store);
            }

            return RedirectToAction(nameof(Index));
        }
        private void DeleteOldImage(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
                return;

            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", imageName);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }
        private async Task<(bool IsValid, string ErrorMessage, string ImageName)> TrySaveImageAsync(IFormFile image)
        {
            var extension = Path.GetExtension(image.FileName);

            if (!_allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError(nameof(image), Errors.NotAllowedExtension);
                return (false, Errors.NotAllowedExtension, null);
            }

            if (image.Length > _maxAllowedSize)
            {
                ModelState.AddModelError(nameof(image), Errors.MaxSize);
            return (false, Errors.MaxSize, null);
            }

            var imageName = $"StoreLogo{Guid.NewGuid()}{extension}";
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", imageName);

            using var stream = System.IO.File.Create(path);
            await image.CopyToAsync(stream);

            return (true, null, imageName);
        }

    }

}

