namespace Storeify.Data.Entities
{
    public class DiscountProduct : BasseModel
    {
        [Required]
        [ForeignKey(nameof(Discount))]
        public int DiscountId { get; set; }
        public Discount Discount { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
