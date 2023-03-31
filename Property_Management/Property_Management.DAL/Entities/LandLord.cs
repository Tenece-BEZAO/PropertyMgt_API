﻿using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public  class LandLord
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessage = "LastName cannot be empty"), 
        RegularExpression(@"^[\w ]*[a-zA-Z]+(([', -][a-zA-Z])?[a-zA-Z]*)\s*$", ErrorMessage = "Invalid Firstname !"), MaxLength(25), MinLength(2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name cannot be empty"), 
        RegularExpression(@"^[\w ]*[a-zA-Z]+(([', -][a-zA-Z])?[a-zA-Z]*)\s*$", ErrorMessage = "Invalid Lastname !"), MaxLength(25), MinLength(2)]
        public string? FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Occupation { get; set; }
        public string? Address { get; set; }
       
        public string PropertyId { get; set; }
        public string? TenantId { get; set; }

        public ICollection<Tenant> Tenant { get; set; }
        public ICollection<Property> Property { get; set; }
        public ApplicationUser User { get; set; }
    }
}
