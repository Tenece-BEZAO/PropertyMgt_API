using Property_Management.DAL.Enums;

namespace Property_Management.BLL.DTOs.Requests
{
    public class NewUnitRequest
    {
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string PropertyId { get; set; }
        public string UnitDescription { get; set; }
        public UnitType UnitType { get; set; }
        public decimal UnitPrice { get; set; }
        public int NumOfBedRooms { get; set; }
    }
}
