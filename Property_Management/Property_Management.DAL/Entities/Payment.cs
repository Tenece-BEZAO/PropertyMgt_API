using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Property_Management.DAL.Entities
{
    public class Payment
    {
       
        public string PaymentId { get; set; }
        public string PaymentMethod { get; set; }
        public string LeaseId { get; set; }
        public string PaidBy { get; set; }

        [Precision(18, 2)]
        public decimal Amount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }
        public Lease Lease { get; set; }
        public Tenant? Tenants { get; set; }

    }
}
