using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public class MaintenanceRequest
    {  
        public DateTime Date { get; set; }
       
        public string MaintenanceRequestId { get; set; }
        public string UnitId { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
        public string ReportedTo { get; set; }
        public string Priority { get; set; }
        public string LoggedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateLogged { get; set; }
        public string? Status { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
        public Unit Unit { get; set; }
        public Staff Employees { get; set; }
        public Tenant? Tenants { get; set; }
        public Vendor Vendor { get; set; }


        public List<WorkOrder> WorkOrder { get; set; }

    }
}
