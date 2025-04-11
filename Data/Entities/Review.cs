namespace Storeify.Data.Entities
{
    public class Review : BasseModel
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public decimal Rating { get; set; }

        [Required]
        [MaxLength(50)]
        public string ReviewTitle { get; set; } = null!;

        [Required]
        [MaxLength(500)] // Adjust length as needed
        public string ReviewBody { get; set; } = null!;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;

        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        public int CustomerID { get; set; }

        // Navigation properties
        public virtual Product Product { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;

    }
}
