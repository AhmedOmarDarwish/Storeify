namespace Storeify.Data.Entities
{
    public class Inventory :BasseModel
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Branch))]
        public int BranchID { get; set; }

        // Navigation property 
        public virtual Branch Branch { get; set; } = null!;
        public virtual ICollection<InventoryProduct> InventoryProducts { get; set; } = new HashSet<InventoryProduct>();
    }
}
