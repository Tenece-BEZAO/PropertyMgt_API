using Microsoft.EntityFrameworkCore;
using Property_Management.DAL.Enums;

namespace Property_Management.DAL.Entities
{
    public class Payment
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public PaymentType PaymentType { get; set; }
        public string TenantId {get; set;}
        public string UserId { get; set; }
        public PaymentFor PaymentFor { get; set; }
        public bool IsDeleted { get; set; }
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public ApplicationUser? User { get; set; }

    }
}
