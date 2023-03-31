using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management.BLL.DTOs.Requests
{
    public class UpdatePropertyRequests
    {
        public string PropertyId { get; set; }
        public string Name { get; set; }
        public string? LeaseId { get; set; }
        public string Address { get; set; }
        public string? City { get; set; }
        public bool? Status { get; set; }
        public string? Zipcode { get; set; }
        public string NumOfUnits { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }

        public ICollection<Unit> Units { get; set; }
        public LandLord LandLord { get; set; }
        public string LandLordId { get; set; }
        public string Image { get; set; }
        public Lease Leases { get; set; }
    }
}
