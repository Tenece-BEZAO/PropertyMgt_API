using Microsoft.EntityFrameworkCore;

namespace Property_Management.DAL.Entities
{
    public class Lease
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string TenantId { get; set; }
        public string PaymentId { get; set; }
        public string PropertyId { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; } = DateTime.UtcNow.AddDays(1);
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public bool Status { get; set; }

        [Precision(18, 2)]
        public decimal Rent { get; set; }
        [Precision(18, 2)]
        public decimal SecurityDeposit { get; set; } 
        public string? UpcomingTenant { get; set; }
        public Unit Unit { get; set; }  
        public Tenant Tenant { get; set; }
        public IEnumerable<Payment> Payment { get; set; }
        public IEnumerable<Property> Property { get; set; }
        public SecurityDepositReturn SecurityDepositReturns { get; set; }
    }
}
