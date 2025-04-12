namespace Storeify.Data.Entities
{
    public class InventoryProduct:BasseModel
    {

        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; } = null!;


        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }

    }
}
