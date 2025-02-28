using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Models
{
    public class DataPoint
    {
        public string Label { get; set; } = string.Empty;
        public double Value { get; set; }
    }

    public class FAQ
    {
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public bool IsExpanded { get; set; } = false;
    }

    public class WeeklyStepData
    {
        public string WeekRange { get; set; } = string.Empty;
        public int TotalSteps { get; set; }

    }
}

