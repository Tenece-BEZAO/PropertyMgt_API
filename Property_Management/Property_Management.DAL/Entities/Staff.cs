using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public class Staff
    {
        public string StaffId { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessage = "LastName cannot be empty"), RegularExpression(@"^[\w ]*[a-zA-Z]+(([', -][a-zA-Z])?[a-zA-Z]*)\s*$",
  ErrorMessage = "Invalid Firstname !"), MaxLength(25), MinLength(2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name cannot be empty"), RegularExpression(@"^[\w ]*[a-zA-Z]+(([', -][a-zA-Z])?[a-zA-Z]*)\s*$",
             ErrorMessage = "Invalid Lastname !"), MaxLength(25), MinLength(2)]
        public string? FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Unit> Units { get; set; }
        public List<MaintenanceRequest> MaintenanceRequests { get; set; }
        public virtual ICollection<InspectionCheck> InspectionChecks { get; set; }

        public List<WorkOrder> WorkOrders { get; set; }
        public ApplicationUser User { get; set; }
    }
}
