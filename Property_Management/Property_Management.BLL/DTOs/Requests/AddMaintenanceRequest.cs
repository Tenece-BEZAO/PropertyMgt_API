using Property_Management.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace Property_Management.BLL.DTOs.Requests
{
    public class AddMaintenanceRequest
    {
        public virtual string? Id { get; set; }
        public string? UnitId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string? ReportedTo { get; set; }
        public Priority? Priority { get; set; } = DAL.Enums.Priority.Manageable;
        public string? RequestedBy { get; set; }
        public DateTime? DateLogged { get; set; } = DateTime.UtcNow;
        public MrStatus? Status { get; set; }
        public DateTime? DueDate { get; set; } = DateTime.UtcNow;
    }

    public class UpdateMaintenanceRequest : AddMaintenanceRequest
    {
        [Required]
        public override string? Id { get; set;}
    }
}
