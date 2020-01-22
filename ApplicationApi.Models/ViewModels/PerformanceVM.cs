using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationApi.Models.ViewModels
{
    public class PerformanceVM
    {
        public int id { get; set; }
        public string EmployeeName { get; set; }
        public string Month { get; set; }
        public Nullable<float> TotalTarget { get; set; }
        public Nullable<float> TargetAchieved { get; set; }
        public Nullable<float> Completion { get; set; }
        public bool IsMonthClosed { get; set; }

    }
}
