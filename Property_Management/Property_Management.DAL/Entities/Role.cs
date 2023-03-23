using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public class Role
    {
        [Key]
        public string RoleId { get; set; } 
        public string RoleName { get; set; }
        public byte[]? Concurrency { get; set; }

    }
}
