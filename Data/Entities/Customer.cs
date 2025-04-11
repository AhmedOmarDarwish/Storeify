namespace Storeify.Data.Entities
{
    public class Customer : BasseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Phone]
        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = null!;

        [MaxLength(100)]
        public string? Address { get; set; }

    }
}
