using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management.BLL.Models
{
    public class TenantWithLease
    {
        
            public int TenantId { get; set; }
            public string Name { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string PropertyAddress { get; set; }
            public string UnitNumber { get; set; }
        }

    }

