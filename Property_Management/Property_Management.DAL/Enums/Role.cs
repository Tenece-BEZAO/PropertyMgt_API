namespace Property_Management.DAL.Enums
{
    public enum UserRole
    {
        Admin = 1,
        LandLord,
        Manager,
        Tenant,
        User,
    }

    public static class GetUserRole
    {
        public static string? GetStringValue(this UserRole userRole)
        {
            return userRole switch
            {
                UserRole.Admin => "admin",
                UserRole.LandLord => "landlord",
                UserRole.Manager => "manager",
                UserRole.Tenant => "tenant",
                UserRole.User => "user",
                _ => null
            };
        }
    }
}