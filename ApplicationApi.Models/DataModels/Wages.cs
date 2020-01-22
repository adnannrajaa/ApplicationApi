using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationApi.Models.DataModels
{
    public class Wages
    {
        [Key]
        public int WagesId { get; set; }
        public int UserId { get; set; }
        public string CurrentSalary { get; set; }
        public string SalaryPaid { get; set; }
        public string Incentives { get; set; }
        public string otherBenifits { get; set; }
        public string Description { get; set; }
        public string WagesMonth { get; set; }
        public string WagesYear { get; set; }
        public string OutStanding { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }

    }
}
