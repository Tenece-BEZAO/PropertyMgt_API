using System.ComponentModel.DataAnnotations;

namespace Property_Management.BLL.DTOs.Responses
{
    public class TenantDTO
    {


        public string TenantId { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
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
        public string? Occupation { get; set; }

        public DateTime? MoveOutDate { get; set; } = DateTime.UtcNow;
        public string? NormalizedMoveOutDate { get; set; }
        public string Address { get; set; }
        public IEnumerable<LeaseDto> Leases { get; set; }



        public static TenantDTO FromTenant(TenantDTO tenant)
        {
            return new TenantDTO
            {
                FirstName = tenant.FirstName,
                LastName = tenant.LastName,
                Address = tenant.Address,
                Email = tenant.Email,
                Occupation = tenant.Occupation,
                PhoneNumber = tenant.PhoneNumber,
                MoveInDate = tenant.MoveInDate,
                MoveOutDate = tenant.MoveOutDate,

            };
        }

    }


        public class LeaseDto
        {
         public string LeaseId { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
        public DateTime EndDate { get; set; } = DateTime.UtcNow.AddDays(1);
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public UnitDto Unit { get; set; }
        }

        public class UnitDto
        {
        public string UnitId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        
    }

    }

    
