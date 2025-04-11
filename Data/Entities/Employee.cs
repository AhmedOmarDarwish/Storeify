namespace Storeify.Data.Entities
{
    public class Employee : BasseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(14)]
        [MinLength(14)]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "National ID must be exactly 14 digits.")]
        public string NationalID { get; set; } = null!;

        [Required]
        [MaxLength(1)]
        public string Gender { get; set; } = null!;

        [Phone]
        [MaxLength(20)]
        public string? Phone { get; set; }

        [EmailAddress]
        [MaxLength(50)]
        public string? Email { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal Salary { get; set; }


        // Foreign Keys
        [ForeignKey(nameof(Department))]
        public int DepartmentID { get; set; }
        // Navigation property
        public virtual Department Department { get; set; } = null!;

     
    }
}
