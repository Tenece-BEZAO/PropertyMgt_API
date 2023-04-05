using Property_Management.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management.BLL.DTOs.Responses
{
    public class TenantDTO
    {


       /* public string TenantId { get; set; }*/
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
        [AllowNull]
        public string Address { get; set; }
        public string PropertyId { get; set; }



        public static TenantDTO FromTenant(Tenant tenant)
        {
            return new TenantDTO
            {
                UserId = tenant.UserId,
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
}
