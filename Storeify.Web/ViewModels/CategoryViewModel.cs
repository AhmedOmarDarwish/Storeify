namespace Storeify.Web.ViewModels
{
    public class CategoryViewModel:BasseModel
    {
        public int Id { get; set; }

        [MaxLength(length: 100, ErrorMessage = Errors.MaxLength)]
        [Remote("AllowItem", controller: null, AdditionalFields = nameof(Id), ErrorMessage = Errors.Duplicated)]
        public string Name { get; set; } = null!;
        [MaxLength(length: 500, ErrorMessage = Errors.MaxLength)]
        public string? Description { get; set; }
    }
}
