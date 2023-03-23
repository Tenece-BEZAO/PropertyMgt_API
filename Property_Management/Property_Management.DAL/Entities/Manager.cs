using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public class Manager
    {
        [Key]
        public string ManagerId { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Occupation { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public byte[]? Concurrency { get; set; }
        public ICollection<InspectionCheck> InspectionChecks { get; set; }

    }
}
