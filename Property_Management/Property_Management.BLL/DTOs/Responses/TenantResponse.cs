using Property_Management.DAL.Entities;

namespace Property_Management.BLL.DTOs.Responses
{
    public class TenantResponse
    {
        public decimal SecurityDeposit { get; set; }
        public decimal Rent { get; set; }
        public IEnumerable<Tenant> Tenants { get; set; }
    }
}
