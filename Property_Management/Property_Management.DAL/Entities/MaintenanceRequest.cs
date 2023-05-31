using Property_Management.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public class MaintenanceRequest
    {  
        public DateTime Date { get; set; }
       
        public string Id { get; set; }
        public string UnitId { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
        public string ReportedTo { get; set; }
        public Priority Priority { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string LoggedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateLogged { get; set; }
        public MrStatus Status { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
        public Unit Unit { get; set; }
        public Staff Employee { get; set; }
        public Tenant? Tenant { get; set; }
        public Vendor Vendor { get; set; }


        public List<WorkOrder> WorkOrder { get; set; }

    }
}
