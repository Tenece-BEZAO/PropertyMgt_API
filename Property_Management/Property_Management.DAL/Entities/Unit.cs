using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
      public class Unit
      {
        
        public string UnitId { get; set; }
        public string PropertyId { get; set; }
        public int NumOfBedRooms { get; set; }
        public int UnitType { get; set; }
        public string TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Precision(18, 2)]
        public decimal Rent { get; set; }
        public string StaffId { get; set; }
        public virtual Staff Staff { get; set; }
        public Property Property { get; set; }
        public ICollection<Tenant> Tenants { get; set; }
        public ICollection<Lease> Leases { get; set; }
        public ICollection<MaintenanceRequest> MaintenanceRequests { get; set; }
        public  ICollection<SecurityDepositReturn>  SecurityDepositReturns { get; set; }
        public ICollection<InspectionCheck> inspectionChecks { get; set; }
        public ICollection<WorkOrder> WorkOrders { get; set; }
    }
}
