
namespace Storeify.Web.ViewModels
{
    public class BranchFormViewModels:BasseModel
    {
        public int Id { get; set; }

        [MaxLength(200, ErrorMessage = Errors.MaxLength)]
        [Remote("AllowItem", null!, AdditionalFields = "Id,StoreId", ErrorMessage = Errors.DuplicatedBranch)]
        public string Location { get; set; } = null!;

        [DataType(DataType.PhoneNumber)]
        [MaxLength(15)]
        [RegularExpression(@"^\(?\d{3}\)?[\s\-]?\d{4}[\s\-]?\d{4}$", ErrorMessage = Errors.PhoneNumber)]
        public string? Phone { get; set; }

        [Display(Name = "Store")]
        [Remote("AllowItem", null!, AdditionalFields = "Id,Location", ErrorMessage = Errors.DuplicatedBranch)]
        public int StoreId { get; set; }

        public Store? Store { get; set; }

        public IEnumerable<SelectListItem>? Stores { get; set; }

    }
}
