namespace Storeify.Data.Entities
{
    public class Payment : BasseModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = null!;

    }
}

