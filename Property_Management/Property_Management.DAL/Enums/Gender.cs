namespace Property_Management.DAL.Enums
{
    public enum Gender
    {
        Male = 1,
        Female,
        Shemale,
        PreferNotToSay,
    }

   public static class GetGenderType
    {
        public static string? GetStringValue(this Gender gender)
        {
            return gender switch
            {
                Gender.Male => "Male",
                Gender.Female => "Female",
                Gender.Shemale => "Shemale",
                Gender.PreferNotToSay => "Not Specified",
                _ => null
            };
        }
    }
}
