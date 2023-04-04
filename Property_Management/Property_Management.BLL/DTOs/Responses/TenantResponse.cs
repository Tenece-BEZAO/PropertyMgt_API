using Property_Management.DAL.Entities;

namespace Property_Management.BLL.DTOs.Responses
{
    public class PaymentInfoResponse
    {
        
        public decimal SecurityDeposit { get; set; }
        public decimal Rent { get; set; }
        public TenantPaymentInfoResponse TenantResponse { get; set; }
    }

    public class TenantPaymentInfoResponse
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
