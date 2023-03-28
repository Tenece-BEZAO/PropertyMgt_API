using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public class SecurityDepositReturn
    {
        [Key]
        public string? SecurityDepositReturnId { get; set; }
        public string PropertyId { get; set; } = string.Empty;
        public string UnitId { get; set; } 
        public string LeaseId { get; set; } = string.Empty;

        [Precision(18, 2)]
        public decimal SecurityDepositAmount { get; set; }
        [Precision(18, 2)]
        public decimal AmountReturnedAfterLease { get; set; }
        public string? LeavingTenant { get; set; }
        public string LeavingTenantPropertyNo { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReturnDate { get; set; }

        public Tenant? Tenants { get; set; }
        public Lease? Leases { get; set; }
        public Property? Properties { get; set; }
        public Unit? Units { get; set; }



    }
}