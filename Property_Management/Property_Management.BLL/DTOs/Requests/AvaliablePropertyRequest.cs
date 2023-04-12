using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management.BLL.DTOs.Requests
{
    public class AvaliablePropertyrequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string? City { get; set; }
        public bool Status { get; set; }
        public string? Zipcode { get; set; }
        public string? NumOfUnits { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
