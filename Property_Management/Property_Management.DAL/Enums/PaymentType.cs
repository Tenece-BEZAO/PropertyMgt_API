namespace Property_Management.DAL.Enums
{
    public enum PaymentType
    {
        Cash = 1,
        Paypal,
        CreditCard,
        DebitCard,
        EFT,
        MobilePayment,
        OnlinePay,
        Check,
        MoneyOrder,
        WireTransfer,
    }

    public static partial class GetStringValue
    {

        public static string GetPaymentType(this PaymentType paymentType)
        {
            return paymentType switch
            {
                PaymentType.Cash => "Cash",
                PaymentType.Paypal => "Paypal",
                PaymentType.CreditCard => "Credit Card",
                PaymentType.DebitCard => "Debit Card",
                PaymentType.EFT => "Electronic Fund Transfer",
                PaymentType.MobilePayment => "Mobile payment",
                PaymentType.OnlinePay => "Online payment",
                PaymentType.Check => "Check",
                PaymentType.MoneyOrder => "Money Order",
                PaymentType.WireTransfer => "Wire Transfer",
                _ => string.Empty,
            };
        }
    }
}

