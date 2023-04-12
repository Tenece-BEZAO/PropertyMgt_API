using Microsoft.EntityFrameworkCore;
using Property_Management.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public class Payment
    {
       
        public string Id { get; set; }
        public PaymentType PaymentType { get; set; }
        public string TenantId { get; set; }
        public string LeaseId { get; set; }
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public Lease Lease { get; set; }
        public Tenant? Tenant { get; set; }

    }
}
