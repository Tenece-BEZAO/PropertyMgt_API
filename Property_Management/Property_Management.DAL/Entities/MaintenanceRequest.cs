using Property_Management.DAL.Enums;

namespace Property_Management.DAL.Entities
{
    public class MaintenanceRequest
    {  
        public string Id { get; set; }
        public string UnitId { get; set; }
        public string Description { get; set; }
        public string ReportedTo { get; set; }
        public string? VendorId { get; set; }
        public Priority Priority { get; set; }
        public string Priority { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string RequestedBy { get; set; }
        public DateTime DateLogged { get; set; }
        public MrStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public Unit Unit { get; set; }
        public Staff Employee { get; set; }
        public Tenant Tenant { get; set; }
        public Vendor Vendor { get; set; }
        public List<WorkOrder> WorkOrder { get; set; }

    }
}
