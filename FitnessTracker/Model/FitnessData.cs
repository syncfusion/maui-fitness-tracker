namespace FitnessTracker
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
        public int TotalCalories { get; set; }
        public string ActivityType { get; set; } = string.Empty;
    }
}

