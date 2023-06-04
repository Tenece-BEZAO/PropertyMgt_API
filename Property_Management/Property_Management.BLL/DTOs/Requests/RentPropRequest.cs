using Property_Management.DAL.Enums;

namespace Property_Management.BLL.DTOs.Requests
{
    public class RentPropRequest
    {
        public string UserId { get; set; }
        public decimal Price { get; set; }
        public string UnitId { get; set; }
        public string PropertyId { get; set; }
        public string PaymentId { get; set; }
        public PaymentType paymentType { get; set; }
        public string LeaseId { get; set; }
    }
}
