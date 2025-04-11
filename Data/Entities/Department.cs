namespace Storeify.Data.Entities
{
    public class Department : BasseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(length: 100)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        // Foreign Key
        [Required]
        [ForeignKey(nameof(Branch))]
        public int BranchId { get; set; }

        // Navigation Property
        public virtual Branch Branch { get; set; } = null!;
    }
}
