using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public class WorkOrder
    {
        public string WorkOrderId { get; set; }
        public string MaintenanceRequestId { get; set; }
        public string StaffId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateCompleted { get; set; }
        [Precision(18, 2)]
        public decimal Cost { get; set; }

        public MaintenanceRequest MaintenanceRequest { get; set; }
        public Staff Employees { get; set; }
        public List<WorkOrderVendor> WorkOrderVendors { get; set; }
    }
}
