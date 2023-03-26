using Microsoft.AspNetCore.Identity;
using Property_Management.DAL.Enums;

namespace Property_Management.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? PropertyId { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public UserType UserTypeId { get; set; }
        public UserRole UserRole { get; set; }
        public DateTime BirthDay { get; set; }
        public string? Address { get; set; }
        public string? Occupation { get; set; }
        public Property? Property { get; set; }
        public byte[]? Concurrency { get; set; }
    }
}
