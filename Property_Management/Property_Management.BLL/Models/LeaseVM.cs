using Microsoft.EntityFrameworkCore;
using Property_Management.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management.BLL.Models
{
    public class LeaseVM
    {
        public string LeaseId { get; set; }

        [Precision(18, 2)]
        public decimal Security_Deposit_Amount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        [Precision(18, 2)]
        public decimal Rent { get; set; }
        public string Status { get; set; }  
    }
}
