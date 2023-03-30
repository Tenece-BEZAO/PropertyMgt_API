using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management.BLL.Models
{
    public class CreateTenantVM
    {
        public string TenantId { get; set; }
        
        public string? LastName { get; set; }
       
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
    }
}
