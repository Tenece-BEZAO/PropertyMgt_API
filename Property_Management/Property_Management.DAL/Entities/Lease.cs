using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public class Lease
    {

        public string LeaseId { get; set; }
        public string TenantUnitId { get; set; }

        [Precision(18, 2)]
        public decimal Security_Deposit_Amount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        public string TenantPropertyId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        [Precision(18, 2)]
        public decimal Rent { get; set; }
        public string Status { get; set; }
        public string Upcoming_Tenant { get; set; }

        public Unit Unit { get; set; }
        public virtual Tenant Tenant { get; set; }
        public List<Payment> Payments { get; set; }
        public ICollection<Property> Properties { get; set; }
        public SecurityDepositReturn SecurityDepositReturns { get; set; }
    }
}
