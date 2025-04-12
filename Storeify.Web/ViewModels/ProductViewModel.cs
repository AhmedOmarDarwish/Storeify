using System.ComponentModel.DataAnnotations.Schema;

namespace Storeify.Web.ViewModels
{
    public class ProductViewModel:BasseModel
    {
        public int Id { get; set; }
        [Remote("AllowItem", null!, AdditionalFields = "Id,Barcode", ErrorMessage = Errors.DuplicatedBarcode)]
        [MaxLength(50, ErrorMessage = Errors.MaxLength)]
        public string Barcode { get; set; } = null!;

        [Display(Name = "Product")]
        //[Remote("AllowItem", null!, AdditionalFields = "Id,Barcode", ErrorMessage = Errors.DuplicatedProduct)]
        [MaxLength(50, ErrorMessage = Errors.MaxLength)]
        public string Name { get; set; } = null!;

        [MaxLength(1000, ErrorMessage = Errors.MaxLength)]
        public string? Description { get; set; }

        [MaxLength(length: 300, ErrorMessage = Errors.MaxLength)]
        public string? ImageUrl { get; set; }

        [Display(Name = "Product Image")]
        public IFormFile? Image { get; set; }

        [Display(Name = "Stock Q")]
        public int? StockQuantity { get; set; } = 0;

        [Column(TypeName = "decimal(10,2)")]
        [Range(0, 99999999.99, ErrorMessage = "Price must be between 0 and 99,999,999.99")]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category? Category { get; set; } = null!;

        public IEnumerable<SelectListItem>? Categories { get; set; }

    }
}
