namespace Storeify.Data.Entities
{
    [Table("Store")]
    public class Store : BasseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Phone]
        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(length: 300)]
        public string? LogoUrl { get; set; }

        public virtual ICollection<Branch> Branches { get; set; } = new HashSet<Branch>();
    }
}
