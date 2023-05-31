namespace Property_Management.DAL.Enums
{
    public enum UnitType
    {
        TwoBedRoomFlat = 1,
        Duplex,
        Estate,
        Bungallow,
        OneRoomApartment
    }

    public static class GetUnitType
    {
        public static string? GetStringValue(this UnitType unitType)
        {
            return unitType switch
            {
                UnitType.TwoBedRoomFlat => "Two bed room flat",
                UnitType.Duplex => "Duplex",
                UnitType.Estate => "Estate",
                UnitType.Bungallow => "Bunggallow",
                UnitType.OneRoomApartment => "One Room apartment",
                _ => null
            };
        }
    }
}
