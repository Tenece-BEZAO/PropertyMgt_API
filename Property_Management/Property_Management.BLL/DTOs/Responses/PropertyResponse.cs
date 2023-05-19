namespace Property_Management.BLL.DTOs.Responses
{
    public class PropertyResponse
    {
        public string PropertyId { get; set; }
        public string LandLordId { get; set; }
        public LeaseResponse Lease { get; set; }
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
}
