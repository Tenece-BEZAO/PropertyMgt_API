using Microsoft.EntityFrameworkCore;

namespace Property_Management.DAL.Entities
{
    public class Transaction
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        public string TransactionRefereal { get; set; } = "None";
        public DateTime MadeAt { get; set; } = DateTime.UtcNow;
    }
}
