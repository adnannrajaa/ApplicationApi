using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationApi.Models.DataModels
{
    public class EmployeePerformance
    {
        [Key]
        public int PerformanceId { get; set; }
        public string UserId { get; set; }
        public string Month { get; set; }
        public Nullable<float> TotalTarget { get; set; }
        public Nullable<float> TargetAchieved { get; set; }
        public Nullable<float> Completion { get; set; }
        public Nullable<bool> IsMonthClosed { get; set; }
    }
}
