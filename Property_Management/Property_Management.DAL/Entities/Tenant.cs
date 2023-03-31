using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Property_Management.DAL.Entities
{
    public class Tenant
    {
        public string TenantId { get; set; }
        public string UserId { get; set; }
        public string UnitId { get; set; }
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
        public string? Address { get; set; }
        public string? SecurityDepositReturnId { get; set; }
        public string? LandLordId { get; set; }
        public string? MaintenanceId { get; set; }
        public string? LeaseId { get; set; }
        public string? SecurityId { get; set; }
        public Unit Units { get; set; }
        public IEnumerable<Payment> Payments { get; set; }
        public ICollection<MaintenanceRequest> MaintenanceRequests { get; set; }
        public LandLord LandLord { get; set; }
        public Property Property { get; set; }
        public ICollection<SecurityDepositReturn> SecurityDepositReturns { get; set; }
        public ApplicationUser User { get; set; }

    }

}

