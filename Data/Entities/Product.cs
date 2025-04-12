namespace Storeify.Data.Entities
{
    [Index(nameof(Barcode),  IsUnique = true)]
    public class Product : BasseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Barcode { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [MaxLength(length: 300)]
        public string? ImageUrl { get; set; }

        public int? StockQuantity { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        // Navigation property
        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<InventoryProduct> InventoryProducts { get; set; } = new HashSet<InventoryProduct>();
        public virtual ICollection<DiscountProduct> DiscountProducts { get; set; } = new HashSet<DiscountProduct>();

    }
}
