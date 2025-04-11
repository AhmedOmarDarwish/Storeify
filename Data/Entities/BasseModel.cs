namespace Storeify.Data.Entities
{
    public class BasseModel
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Status")]
        public bool IsDeleted { get; set; } = false;
        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
        public string? DeletedReason { get; set; }

    }
}
