using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public class MaintenanceRequest
    {
        [Key]
        public string RequestId { get; set; }
        public string LoggedBy { get; set; }
        public string MRNotes {get;set;}
        public string Priority { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime  DateLogged { get; set; }    
        public string? Status { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
        public TenantDetail? TenantDetails { get; set; }
        public byte[]? Concurrency { get; set; }

    }
}
