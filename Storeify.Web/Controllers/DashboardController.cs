using Microsoft.AspNetCore.Mvc;
using Storeify.Web.Core.ViewModels;

namespace Storeify.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DashboardController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var numberOfProduct = _context.Products.Count(c => !c.IsDeleted);

            numberOfProduct = numberOfProduct <= 10 ? numberOfProduct : numberOfProduct / 10 * 10;

            var numberOfOrder = _context.Orders.Count(c => !c.IsDeleted);
            var lastAddedProduct = _context.Products
                                .Include(b => b.Category)
                                .Where(b => !b.IsDeleted)
                                .OrderByDescending(b => b.Id)
                                .Take(8)
                                .ToList();

            //var topProduct = _context.PurchaseOrderDetails
            //    .Include(c => c.BookCopy)
            //    .ThenInclude(c => c!.Book)
            //    .ThenInclude(b => b!.Author)
            //    .GroupBy(c => new
            //    {
            //        c.BookCopy!.BookId,
            //        c.BookCopy!.Book!.Title,
            //        c.BookCopy!.Book!.ImageThumbnailUrl,
            //        AuthorName = c.BookCopy!.Book!.Author!.Name
            //    })
            //    .Select(b => new
            //    {
            //        b.Key.BookId,
            //        b.Key.Title,
            //        b.Key.ImageThumbnailUrl,
            //        b.Key.AuthorName,
            //        Count = b.Count()
            //    })
            //    .OrderByDescending(b => b.Count)
            //    .Take(6)
            //    .Select(b => new BookViewModel
            //    {
            //        Id = b.BookId,
            //        Title = b.Title,
            //        ImageThumbnailUrl = b.ImageThumbnailUrl,
            //        Author = b.AuthorName
            //    })
            //    .ToList();

            var viewModel = new DashboardViewModel
            {
                NumberOfProducts = numberOfProduct,
                NumberOfOrders = numberOfProduct,
                LastAddedProducts = _mapper.Map<IEnumerable<ProductViewModel>>(lastAddedProduct),
            };

            return View(viewModel);
        }
    }
}
