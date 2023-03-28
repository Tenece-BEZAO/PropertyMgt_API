using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Property_Management.DAL.Entities
{
    public class Tenant
    {
        public string TenantId { get; set; }
        public string UnitId { get; set; }
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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime MoveInDate { get; set; }
        public string? PropertytId { get; set; }
        public string? Occupation { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime MoveOutDate { get; set; }

        public string? SecurityDepositReturnId { get; set; }
        public string? LandLordId { get; set; }
        public Unit Units { get; set; }
        public virtual ICollection<Lease> Leases { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<MaintenanceRequest> MaintenanceRequests { get; set; }
        public LandLord LandLords { get; set; }
        public Property Properties { get; set; }
        public ICollection<SecurityDepositReturn> SecurityDepositReturns { get; set; }

    }

}

