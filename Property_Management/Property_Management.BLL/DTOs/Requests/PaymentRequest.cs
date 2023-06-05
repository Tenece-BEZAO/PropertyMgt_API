using Property_Management.DAL.Enums;

namespace Property_Management.BLL.DTOs.Requests
{
    public class PaymentRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public decimal Amount { get; set; }
        public PaymentType PaymentType { get; set; }
        public PaymentFor PaymentFor { get; set; }
    }
}
