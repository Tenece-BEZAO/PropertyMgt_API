namespace Property_Management.DAL.Enums
{
    public enum Gender
    {
        Male = 1,
        Female,
        Shemale,
        PreferNotToSay,
    }

   public static partial class GetStringValue
    {
        public static string? GenderType(this Gender gender)
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
