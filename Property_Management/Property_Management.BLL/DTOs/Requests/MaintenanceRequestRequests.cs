using Property_Management.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management.BLL.DTOs.Requests
{
    public class MaintenanceRequestRequests
    {
        public DateTime Date { get; set; }

        public string MaintenanceRequestId { get; set; }
        public string UnitId { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
        public string ReportedTo { get; set; }
        public string Priority { get; set; }
        public String propertyId { get;} 
        public string? Status { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
    
    }
}
