namespace Storeify.Data.Entities
{
    [Index(nameof(Name), nameof(BranchId), IsUnique = true)]

    public class Inventory :BasseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Branch))]
        public int BranchId { get; set; }

        // Navigation property 
        public virtual Branch Branch { get; set; } = null!;
        public virtual ICollection<InventoryProduct> InventoryProducts { get; set; } = new HashSet<InventoryProduct>();
    }
}
