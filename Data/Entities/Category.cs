namespace Storeify.Data.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category : BasseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(length: 100)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
