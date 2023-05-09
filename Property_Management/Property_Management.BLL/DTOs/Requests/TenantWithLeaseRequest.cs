namespace Property_Management.BLL.DTOs.Requests
{
    public class TenantWithLeaseRequest
    {

        public int TenantId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PropertyAddress { get; set; }
        public string UnitNumber { get; set; }
    }

}

