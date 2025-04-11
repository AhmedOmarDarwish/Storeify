namespace Storeify.Data.Entities
{
    public class User : BasseModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Username { get; set; } = null!;

        [MaxLength( 100)]
        public string Password { get; set; } = null!;

        [EmailAddress]
        [MaxLength( 100)]
        public string Email { get; set; } = null !;

    }
}
