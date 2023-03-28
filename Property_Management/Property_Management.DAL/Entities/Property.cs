using Microsoft.EntityFrameworkCore;

namespace Property_Management.DAL.Entities
{
    


    public class Property
    {

        public string PropertyId { get; set; }
        public string Name { get; set; }
        public string LeaseId { get; set; }
        public string? LandLordId { get; set; }
        public string Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zipcode { get; set; }
        public string NumOfUnits { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }

        public ICollection<Unit> Units { get; set; }
        public LandLord LandLords { get; set; }

        public string OwnedBy { get; set; }
        public string Image { get; set; }
        public Lease Leases { get; set; }

    }
}
