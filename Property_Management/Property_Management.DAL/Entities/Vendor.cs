using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Property_Management.DAL.Entities
{
    public class Vendor
    {
        public string VendorId { get; set; }
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
        public List<WorkOrderVendor> WorkOrderVendors { get; set; } 
    }
}
