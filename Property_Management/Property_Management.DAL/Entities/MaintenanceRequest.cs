using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public class MaintenanceRequest
    {
        [Key]
        public string RequestId { get; set; }
        public string UserId { get; set; }
        public string MRNotes {get;set;}
        public string Priority { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime  DateLogged { get; set; }    
        public bool Status { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
        public ApplicationUser? User { get; set; }
        public byte[]? Concurrency { get; set; }

    }
}
