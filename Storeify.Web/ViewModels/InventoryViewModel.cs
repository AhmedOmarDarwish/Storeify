using System.ComponentModel.DataAnnotations.Schema;

namespace Storeify.Web.ViewModels
{
    public class InventoryViewModel:BasseModel
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = Errors.MaxLength)]
        [Remote("AllowItem", null!, AdditionalFields = "Id,StoreId", ErrorMessage = Errors.DuplicatedInventory)]
        public string Name { get; set; } = null!;

        [Display(Name = "Branch Name")]
        [Remote("AllowItem", null!, AdditionalFields = "Id,Name", ErrorMessage = Errors.DuplicatedInventory)]
        public int BranchId { get; set; }

        public Branch? Branch { get; set; }

        public IEnumerable<SelectListItem>? Branches { get; set; }

    }
}
