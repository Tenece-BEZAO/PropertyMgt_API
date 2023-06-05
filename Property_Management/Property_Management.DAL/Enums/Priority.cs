namespace Property_Management.DAL.Enums
{
    public enum Priority
    {
        Urgency = 1,
        Piercing,
        Manageable,
    }

    public static class GetPriority
    {
        public static string? GetStringValue(this Priority priority)
        {
            return priority switch
            {
                Priority.Urgency => "Urgent",
                Priority.Piercing => "Piercing",
                Priority.Manageable => "Manageable",
                _ => null
            };
        }
    }
}
