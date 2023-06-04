using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public class WorkOrderVendor
    {
        [Key]
        public string WorkOrderVendorId { get; set; }
        public string? VendorId { get; set; }
        [Precision(18, 2)]
        public decimal Cost { get; set; }
        public DateTime DateCompleted { get; set; }
        public string? WorkOrderId { get; set; }
        public WorkOrder WorkOrders { get; set; }
        public Vendor Vendor { get; set; }
        public bool IsDeleted { get; set; }
    }
}
