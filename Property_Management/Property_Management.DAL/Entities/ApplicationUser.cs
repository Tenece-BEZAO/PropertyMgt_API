using Microsoft.AspNetCore.Identity;
using Property_Management.DAL.Enums;

namespace Property_Management.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public override string Email { get; set; }
        public string ProfileImage { get; set; }
        public bool Active { get; set; }
        public UserType UserTypeId { get; set; }
        public UserRole UserRole { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
