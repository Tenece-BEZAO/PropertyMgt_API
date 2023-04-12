using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Property_Management.DAL.Entities
{
    public class Tenant
    {
        public string TenantId { get; set; }
        public string UserId { get; set; }
        public string UnitId { get; set; }
        [Required(ErrorMessage = "LastName cannot be empty"), RegularExpression(@"^[\w ]*[a-zA-Z]+(([', -][a-zA-Z])?[a-zA-Z]*)\s*$",
         ErrorMessage = "Invalid Firstname !"), MaxLength(25), MinLength(2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name cannot be empty"), RegularExpression(@"^[\w ]*[a-zA-Z]+(([', -][a-zA-Z])?[a-zA-Z]*)\s*$",
             ErrorMessage = "Invalid Lastname !"), MaxLength(25), MinLength(2)]
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime MoveInDate { get; set; } = DateTime.UtcNow;
        public string NormalizedMoveInDate { get; set; }
        public string? PropertyId { get; set; }
        public string? Occupation { get; set; }

        public DateTime? MoveOutDate { get; set; } = DateTime.UtcNow;
        public string? NormalizedMoveOutDate { get; set; }
        [AllowNull]
        public string Address { get; set; }
        public string? SecurityDepositReturnId { get; set; }
        public string? LandLordId { get; set; }
        public string? MaintenanceId { get; set; }
        public string? LeaseId { get; set; }
        public string? SecurityId { get; set; }
        public IEnumerable<Lease> Lease {get; set;}
        public Unit Units { get; set; }
        public  virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<MaintenanceRequest> MaintenanceRequests { get; set; }
        public LandLord LandLord { get; set; }
        public Property Property { get; set; }
        public  virtual ICollection<SecurityDepositReturn> SecurityDepositReturns { get; set; }
        public ApplicationUser User { get; set; }

    }

}

