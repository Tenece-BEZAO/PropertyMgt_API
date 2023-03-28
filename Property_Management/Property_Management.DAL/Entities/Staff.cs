using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Property_Management.DAL.Entities
{
    public class Staff
    {
        public string StaffId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Occupation { get; set; }
        public virtual ICollection<Unit> Units { get; set; }
        public List<MaintenanceRequest> MaintenanceRequests { get; set; }
        public virtual ICollection<InspectionCheck> InspectionChecks { get; set; }

        public List<WorkOrder> WorkOrders { get; set; }
    }
}
