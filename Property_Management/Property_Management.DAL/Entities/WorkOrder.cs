using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public class WorkOrder
    {
        [Key]
        public string WorkOrderId { get; set; }
        public string? MaintenanceRequestId { get; set; }
        public string? StaffId { get; set; }
        public bool IsDeleted { get; set; } = false;

        public DateTime DateCreated { get; set; }
        public DateTime? DateCompleted { get; set; }
        [Precision(18, 2)]
        public decimal Cost { get; set; }

        public MaintenanceRequest MaintenanceRequest { get; set; }
        public Staff Employees { get; set; }
        public List<WorkOrderVendor> WorkOrderVendors { get; set; }
    }
}
