namespace Property_Management.DAL.Enums
{
    public enum UserRole
    {
        Admin = 1,
        User,
        Customer,
    }

    public static class GetUserRole
    {
        public static string? GetStringValue(this UserRole userRole)
        {
            return userRole switch
            {
                UserRole.Admin => "Admin",
                UserRole.User => "User",
                UserRole.Customer => "Customer",
                _ => null
            };
        }
    }
}