namespace Property_Management.BLL.DTOs.Responses
{
    public class MrResponse
    {
        public string Description { get; set; }
        public StaffMrResponse Staff { get; set; }
        public string Priority { get; set; }
        public TenantMrResponse Tenant { get; set; }
        public string DateLogged { get; set; }
        public string Status { get; set; }
        public string DueDate { get; set; }

    }
   public class StaffMrResponse
    {
        public string StaffName { get; set; }
        public string Occupation { get; set; }
        public bool Available { get; set; }
    }

    public class TenantMrResponse
    {
        public string TenantName { get; set; }
        public PropertyResponse Property { get; set; }
    }
}
