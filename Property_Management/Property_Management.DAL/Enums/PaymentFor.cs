namespace Property_Management.DAL.Enums
{
    public enum PaymentFor
    {
        Rent = 1,
        Damage,
        HouseRent
    }

    public static class GetPaymentFor
    {

        public static string GetStringValue(this PaymentFor paymentFor)
        {
            return paymentFor switch
            {
                PaymentFor.Rent => "rent",
                PaymentFor.Damage => "damage",
                PaymentFor.HouseRent => "house rent",
                _ => string.Empty,
            };
        }
    }
}

