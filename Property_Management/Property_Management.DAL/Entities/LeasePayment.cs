using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public class LeasePayment
    {
        [Key]
        public string LeasePaymentId { get; set; } 
        public string PaymentType { get; set; } = string.Empty;

        [Precision(18, 2)]
        public decimal PaymentAmount { get; set;}

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }
        public  string LeaseId { get; set; }

        [Precision(18, 2)]
        public decimal LateFees { get; set; }
        public string? PaidBy { get; set; }
        public Tenant? TenantDetails { get; set; }
        public Lease? LeaseDetails { get; set; }
        public byte[]? Concurrency { get; set; }


    }
}
