using Microsoft.EntityFrameworkCore;
using Property_Management.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public class LeasePayment
    {
        [Key]
        public string LeasePaymentId { get; set; } 
        public PaymentType PaymentType { get; set; }

        [Precision(18, 2)]
        public decimal PaymentAmount { get; set;}

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }

        [Precision(18, 2)]
        public decimal LateFees { get; set; }
        public Lease? Lease { get; set; }
        public  string LeaseId { get; set; }
        public ApplicationUser? User { get; set; }
        public string? UserId { get; set; }
        public byte[]? Concurrency { get; set; }


    }
}
