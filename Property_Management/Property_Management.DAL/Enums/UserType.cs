namespace Property_Management.DAL.Enums
{
    public enum UserType
    {
        LandLord = 1,
        Manager,
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
                UserType.Manager => "Manager",
                UserType.Tenant => "Tenant",
                UserType.Staff => "Staff",
                _ => null
            };
        }
    }
}
