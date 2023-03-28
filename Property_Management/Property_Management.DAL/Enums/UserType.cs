namespace Property_Management.DAL.Enums
{
    public enum UserType
    {
        LandLord = 1,
        Tenant,
        Staff
    }

    public static class GetUserType
    {
        public static string? GetStringValue(this UserType userType)
        {
            return userType switch
            {
                UserType.LandLord => "Landlord",
                UserType.Tenant => "Tenant",
                UserType.Staff => "Staff",
                _ => null
            };
        }
    }
}
