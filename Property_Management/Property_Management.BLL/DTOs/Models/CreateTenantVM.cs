using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Property_Management.BLL.Models
{
    /* public class CreateTenantVM
     {
         public string TenantId { get; set; }

         public string? LastName { get; set; }

         public string? FirstName { get; set; }
         public string Email { get; set; }
         public string PhoneNumber { get; set; }

         [DataType(DataType.Date)]
         [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
         public DateTime MoveInDate { get; set; }
         public string? PropertytId { get; set; }
         public string? Occupation { get; set; }
         [DataType(DataType.Date)]
         [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
         public DateTime MoveOutDate { get; set; }
     }*/


    public class TenantDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public LeaseDto Leases { get; set; }
        public UnitDto Unit { get; set; }
        public List<MaintenanceRequestDto> MaintenanceRequestDtos { get; set; }
    }
    public class LeaseDto
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public PropertyDto Property { get; set; }
        public string TenantId { get; set; }
        public TenantDto Tenant { get; set; }

        public string UnitId { get; set; }
        public UnitDto Unit { get; set; }
        public PaymentDto PaymentsDtos { get; set; }

    }
    public class UnitDto
    {
        public string Id { get; set; }
        public PropertyDto Property { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
    public class PropertyDto
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }

    }
    public class MaintenanceRequestDto
    {
        public string Id { get; set; }
        public string Description { get; set; }

    }

    public class PaymentDto
    {
        public string Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }

        public string LeaseId { get; set; }
        public LeaseDto Lease { get; set; }
    }
}