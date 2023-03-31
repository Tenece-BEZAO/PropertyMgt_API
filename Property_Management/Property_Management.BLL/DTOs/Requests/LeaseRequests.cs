using System.ComponentModel.DataAnnotations;

namespace Property_Management.BLL.DTOs.Requests
{
    public class CreateLeaseRequest
    {
        public string PropertyId { get; set; }
        public string TenantId { get; set; }
        public decimal Rent { get; set; }
        public DateTime StartDate { get; set; }
        public bool Status { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }


    public class AcceptLeaseRequest
    {
        public string LeaseId { get; set; }
        public string PropertyId { get; set; }
    }
}
