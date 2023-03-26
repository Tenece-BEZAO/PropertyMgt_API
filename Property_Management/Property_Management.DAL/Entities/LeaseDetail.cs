using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public class Lease
    {
        [Key]
        public string LeaseId { get; set; }
        public string TenantId { get; set; }
        public string PropertyId { get; set; }
        public string Tenant_Property_Number { get; set; }
        public string Tenant_Unit_Number { get; set; }
        public string Upcoming_Tenant { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Lease_Date { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Lease_Start_Date { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Lease_End_Date { get; set; }
        [Precision(18, 2)]
        public decimal Monthly_Rent { get; set; }
        [Precision(18, 2)]
        public decimal Security_Deposit_Amount { get; set; }
        public bool Lease_Status { get; set; }
        public Property? Property { get; set; }
        public byte[]? Concurrency { get; set; }

    }
}
