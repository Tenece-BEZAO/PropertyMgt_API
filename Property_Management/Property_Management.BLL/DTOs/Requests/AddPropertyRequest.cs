namespace Property_Management.BLL.DTOs.Requests
{
    public class AddPropertyRequest
    {
        public string PropertyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? City { get; set; }
        public bool? Status { get; set; }
        public string? Zipcode { get; set; }
        public string NumOfUnits { get; set; }
        public decimal Price { get; set; }
        public string OwnedBy { get; set; }
        public string Image { get; set; }

    }
}
