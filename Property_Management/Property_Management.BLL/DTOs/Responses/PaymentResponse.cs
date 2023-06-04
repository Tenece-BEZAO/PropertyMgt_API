namespace Property_Management.BLL.DTOs.Responses
{
    public class PaymentResponse
    {
        public string? Id { get; set; }
        public string? Message { get; set; }
        public decimal? TransactionAmount { get; set; }
        public string? PaymentFor { get; set; }
        public string? PaymentLink { get; set; }
        public string? ReferenceKey { get; set; }
    }
}
