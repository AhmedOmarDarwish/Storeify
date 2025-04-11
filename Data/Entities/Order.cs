namespace Storeify.Data.Entities
{
    public class Order : BasseModel
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [MaxLength(50)]
        public string OrderType { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Customer))]
        public int CustomerID { get; set; }

        [Required]
        [ForeignKey(nameof(Payment))]
        public int PaymentID { get; set; }

        [Required]
        [ForeignKey(nameof(Delivery))]
        public int DeliveryID { get; set; }

        // Navigation
        public virtual Customer Customer { get; set; } = null!;
        public virtual Payment Payment { get; set; } = null!;
        public virtual Delivery Delivery { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();
    }
}
