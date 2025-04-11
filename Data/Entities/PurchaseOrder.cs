namespace Storeify.Data.Entities
{
    public class PurchaseOrder : BasseModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [ForeignKey(nameof(Supplier))]
        public int SupplierID { get; set; }

        // Navigation property
        public virtual Supplier Supplier { get; set; } = null!;
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new HashSet<PurchaseOrderDetail>();

    }
}
