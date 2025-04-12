namespace Storeify.Web.ViewModels
{
    public class StoreViewModel : BasseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = Errors.MaxLength)]
        public string Name { get; set; } = null!;

        [Phone]
        [MaxLength(15, ErrorMessage = Errors.MaxLength)]
        [RegularExpression(@"^\(?\d{3}\)?[\s\-]?\d{4}[\s\-]?\d{4}$", ErrorMessage = Errors.PhoneNumber)]
        public string? Phone { get; set; }

        [MaxLength(300, ErrorMessage = Errors.MaxLength)]
        public string? LogoUrl { get; set; }

        [Display(Name = "Store Logo")]
        public IFormFile? Image { get; set; }
    }
}
