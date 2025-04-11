namespace Storeify.Data.Entities
{
    public class Discount : BasseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = null!;

        [MaxLength(150)]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal DiscountPercentage { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool IsActive { get; set; }


        [ForeignKey(nameof(Branch))]
        public int? BranchId { get; set; }
        public virtual Branch? Branch { get; set; }

        public virtual ICollection<DiscountProduct> DiscountProducts { get; set; } = new HashSet<DiscountProduct>(); 
    }
}

