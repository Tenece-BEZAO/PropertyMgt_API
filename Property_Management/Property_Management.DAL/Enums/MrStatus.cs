namespace Property_Management.DAL.Enums
{
    public enum MrStatus
    {
        Fixed = 1,
        Accepted,
        Pending,
    }

    public static class GetMrStatus
    {
        public static string? GetStringValue(this MrStatus status)
        {
            return status switch
            {
                MrStatus.Fixed => "Fixed",
                MrStatus.Accepted => "Accepted",
                MrStatus.Pending => "Pending",
                _ => null
            };
        }
    }
}

