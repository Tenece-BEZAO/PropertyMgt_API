using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public class Lease
    {
        [Key]
        public string LeaseId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LeaseDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LeaseStartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LeaseEndDate { get; set; }
        [Precision(18, 2)]
        public decimal MonthlyRent { get; set; }
        [Precision(18, 2)]
        public decimal SecurityDepositAmount { get; set; }
        public bool LeaseStatus { get; set; }
        public string LandLordOrManagerId { get; set; }
        public ApplicationUser? LandLordOrManager { get; set; }
        public string TenantId { get; set; }
        public ApplicationUser? Tenant { get; set; }
        public string PropertyId { get; set; }
        public Property Property { get; set; }
        public byte[]? Concurrency { get; set; }

    }
}
