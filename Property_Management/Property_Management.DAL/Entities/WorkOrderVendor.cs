using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management.DAL.Entities
{
    public class WorkOrderVendor
    {
        public string WorkOrderVendorId { get; set; }
        public string VendorId { get; set; }
        [Precision(18, 2)]
        public decimal Cost { get; set; }
        public DateTime DateCompleted { get; set; }
        public string WorkOrderId { get; set; }
        public WorkOrder WorkOrders { get; set; }
        public Vendor Vendors { get; set; }
    }
}
