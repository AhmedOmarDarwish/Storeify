namespace Storeify.Data.Entities
{
    public class Cart : BasseModel
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        public int CustomerID { get; set; }

        [ForeignKey(nameof(Order))]
        public int? OrderID { get; set; }

        // Navigation
        public virtual Customer Customer { get; set; } = null!;
        public virtual Order? Order { get; set; }
    }
}
