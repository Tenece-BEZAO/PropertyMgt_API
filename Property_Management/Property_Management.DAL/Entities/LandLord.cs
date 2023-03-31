using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public  class LandLord
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
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
