namespace Property_Management.DAL.Enums
{
    public enum UserType
    {
        LandLord = 1,
        Manager,
        Tenant
    }

    public static class GetUserType
    {
        public static string? GetStringValue(this UserType userType)
        {
            return userType switch
            {
                UserType.LandLord => "Landlord",
                UserType.Manager => "Manager",
                UserType.Tenant => "Tenant",
                _ => null
            };
        }
    }
}
