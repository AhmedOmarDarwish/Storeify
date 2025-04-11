namespace Storeify.Data.Entities
{
    public class Branch : BasseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Location { get; set; } = null!;

        [Phone]
        [MaxLength(20)]
        public string? Phone { get; set; }

        // Foreign Key
        [Required]
        [ForeignKey(nameof(Store))]
        public int StoreId { get; set; }

        // Navigation Property
        public virtual Store Store { get; set; } = null!;
        public virtual ICollection<Department> Departments { get; set; } = new HashSet<Department>();
        public virtual Inventory Inventory { get; set; } = null!;
        public virtual ICollection<Discount> Discounts { get; set; } = new HashSet<Discount>();



    }
}
