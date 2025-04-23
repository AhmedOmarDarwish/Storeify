namespace Storeify.Web.Core.ViewModels
{
    public class DashboardViewModel
    {
        public int NumberOfProducts { get; set; }
        public int NumberOfOrders { get; set; }
        public IEnumerable<ProductViewModel> LastAddedProducts { get; set; } = new List<ProductViewModel>();
        //public IEnumerable<ProductViewModel> TopProduct { get; set; } = new List<ProductViewModel>();
        //public IEnumerable<BranchService> TopBranches { get; set; } = new List<BranchService>();
    }
}