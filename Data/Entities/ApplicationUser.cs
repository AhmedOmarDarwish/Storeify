namespace Storeify.Data.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(UserName), IsUnique = true)]
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;

        public string? CreatedById { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string? UpdatedById { get; set; }

        public DateTime? UpdatedOn { get; set; }

    }
}
