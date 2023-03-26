using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public  class Tenant
    {
        [Key]
        public  string TenantId { get; set; }
        public string PropertyId { get; set; }
        public  string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? HomePhone { get; set; }
        public string? MobilePhone { get; set; }
        public Property? Property { get; set; }
        public byte[]? Concurrency { get; set; }

    }
}
