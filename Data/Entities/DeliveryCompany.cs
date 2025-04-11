namespace Storeify.Data.Entities
{
    public class DeliveryCompany: BasseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; } = null!;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(100), EmailAddress]
        public string? EmailAddress { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }
    }
}
