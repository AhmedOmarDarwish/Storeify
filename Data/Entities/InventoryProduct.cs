namespace Storeify.Data.Entities
{
    public class InventoryProduct:BasseModel
    {
        [Required]
        [ForeignKey(nameof(Inventory))]
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }

    }
}
