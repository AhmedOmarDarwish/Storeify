namespace Storeify.Data.Entities
{
    public class BasseModel
    {
        public string? CreatedById { get; set; }
        public ApplicationUser? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string? UpdatedById { get; set; }
        public ApplicationUser? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        [Display(Name = "Status")]
        public bool IsDeleted { get; set; } = false;
        public string? DeletedById { get; set; }
        public ApplicationUser? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedReason { get; set; }



    }
}
