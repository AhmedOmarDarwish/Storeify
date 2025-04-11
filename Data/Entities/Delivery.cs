namespace Storeify.Data.Entities
{
    public enum DeliveryAgentType
    {
        Cashier,
        Employee,
        DeliveryCompany
    }

    public class Delivery : BasseModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string DeliveryStatus { get; set; } = null!;

        [MaxLength(100)]
        public string DeliveryAddress { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DeliveryAmount { get; set; } = 0;


        // Discriminates between Cashier, Employee, DeliveryCompany
        public DeliveryAgentType DeliveryAgentType { get; set; }

        [ForeignKey(nameof(Employee))]
        public int? EmployeeID { get; set; }

        [ForeignKey(nameof(DeliveryCompany))]
        public int? CompanyID { get; set; }


        // Navigation
        public virtual Employee? Employee { get; set; }
        public virtual DeliveryCompany? DeliveryCompany { get; set; }
    }
}
