using Property_Management.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management.BLL.DTOs.Requests
{
    public class MaintenanceRequestrequests
    {
        public DateTime Date { get; set; }

        public string PropertyId { get; set; }
        public string MaintenanceRequestId { get; set; }
        public string UnitId { get; set; }
        public string Description { get; set; }
        public string ReportedTo { get; set; }
        public string Priority { get; set; }
        public string LoggedBy { get; set; }
        public Unit Unit { get; set; }
        public Vendor Vendor { get; set; }

    }
}
