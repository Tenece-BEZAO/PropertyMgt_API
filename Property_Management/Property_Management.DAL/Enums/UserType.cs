namespace Property_Management.DAL.Enums
{
    public enum UserType
    {
        Admin = 1,
        LandLord,
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
                UserType.Admin => "Admin",
                UserType.LandLord => "Landlord",
                UserType.Manager => "manager",
                UserType.Tenant => "Tenant",
                UserType.Staff => "Staff",
                _ => null
            };
        }
    }
}
