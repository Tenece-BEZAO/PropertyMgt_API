using System.ComponentModel.DataAnnotations;

namespace Property_Management.DAL.Entities
{
    public class InspectionCheck
    {
        [Key]
        public string InspectionId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateLogged { get; set; }

        public string UserId { get; set; }
        public int NoOfUnits { get; set; }
        public int UnitIdNumber { get; set; }
        public int NoOfDevicesDamaged { get; set; }

        public ApplicationUser? User { get; set; }
        public byte[]? Concurrency { get; set; }
    }
}
