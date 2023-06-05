namespace Property_Management.BLL.DTOs.Responses
{
    public class PropertyResponse
    {
        public string PropertyId { get; set; }
        public string LandLordId { get; set; }
        public LeaseResponse Lease { get; set; }
        public UnitResponse Unit { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public string Image { get; set; }
        public bool IsDeleted {get; set;} 
    }

    public class LeaseResponse
    {
        public string LeaseId { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; } 
        public DateTime StartDate { get; set; }
    }

    public class UnitResponse
    {
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public decimal UnitPrice { get; set; }
        public string UnitDescription { get; set; }
        public string UnitType { get; set; }
        public int NumberOfRooms { get; set; }
    }
}
