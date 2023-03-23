using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management.DAL.Enums
{
    public enum UserType
    {
        LandLord = 1,
        Manager,
        Tenant
    }

    public static partial class GetStringValue
    {
        public static string? UserTypeEnum(this UserType userType)
        {
            return userType switch
            {
                UserType.LandLord => "Landlord",
                UserType.Manager => "Manager",
                UserType.Tenant => "Tenant",
                _ => null
            };
        }
    }
}
