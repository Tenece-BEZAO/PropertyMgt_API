using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management.BLL.Models
{
    public class AddOrUpdateMaintenanceVM
    {
        public string TenantId { get; set; }
        public string MaintenanceRequestId { get; set; }
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
    }
}
