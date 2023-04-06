namespace Property_Management.BLL.DTOs.Responses
{
    public class PaymentResponse
    {
        public string? Message { get; set; }
        public int? TransactionAmount { get; set; }
        public string? PaymentFor { get; set; }
        public string? PaymentLink { get; set; }
        public string? ReferenceKey { get; set; }
    }
}
