using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Property_Management.DAL.Entities
{
    public class Vendor
    {
        public string VendorId { get; set; }
        [Required(ErrorMessage = "LastName cannot be empty"), RegularExpression(@"^[\w ]*[a-zA-Z]+(([', -][a-zA-Z])?[a-zA-Z]*)\s*$",
  ErrorMessage = "Invalid Firstname !"), MaxLength(25), MinLength(2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name cannot be empty"), RegularExpression(@"^[\w ]*[a-zA-Z]+(([', -][a-zA-Z])?[a-zA-Z]*)\s*$",
             ErrorMessage = "Invalid Lastname !"), MaxLength(25), MinLength(2)]
        public string? FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<WorkOrderVendor> WorkOrderVendors { get; set; } 
    }
}
