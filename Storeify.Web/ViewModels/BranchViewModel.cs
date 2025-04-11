
namespace Storeify.Web.ViewModels
{
    public class BranchViewModel:BasseModel
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = Errors.MaxLength)]
        [Remote("AllowItem", null!, AdditionalFields = "Id,StoreId", ErrorMessage = Errors.DuplicatedBranch)]
        public string Name { get; set; } = null!;

        [DataType(DataType.PhoneNumber)]
        [MaxLength(15)]
        [RegularExpression(@"^\(?\d{3}\)?[\s\-]?\d{4}[\s\-]?\d{4}$", ErrorMessage = Errors.PhoneNumber)]
        public string? Phone { get; set; }

        [Display(Name = "Store")]
        [Remote("AllowItem", null!, AdditionalFields = "Id,Name", ErrorMessage = Errors.DuplicatedBranch)]
        public int StoreId { get; set; }

        public Store? Store { get; set; }

        public IEnumerable<SelectListItem>? Stores { get; set; }

    }
}
