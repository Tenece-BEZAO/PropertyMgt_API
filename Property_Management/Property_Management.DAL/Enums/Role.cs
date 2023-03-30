namespace Property_Management.DAL.Enums
{
    public enum UserRole
    {
        Admin = 1,
        User,
    }

    public static class GetUserRole
    {
        public static string? GetStringValue(this UserRole userRole)
        {
            return userRole switch
            {
                UserRole.Admin => "Admin",
                UserRole.User => "User",
                _ => null
            };
        }
    }
}