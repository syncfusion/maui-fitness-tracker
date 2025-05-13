namespace FitnessTracker
{
    /// <summary>
    /// Represents a data point with a label and a numerical value, typically used for charting or statistics.
    /// </summary>
    public class DataPoint
    {
        /// <summary>
        /// Gets or sets the label for the data point.
        /// </summary>
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the numerical value for the data point.
        /// </summary>
        public double Value { get; set; }
    }

    /// <summary>
    /// Represents a frequently asked question with an answer and a flag indicating whether it is expanded.
    /// </summary>
    public class FAQ
    {
        /// <summary>
        /// Gets or sets the question.
        /// </summary>
        public string? Question { get; set; }

        /// <summary>
        /// Gets or sets the answer to the question.
        /// </summary>
        public string? Answer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the FAQ is expanded to show the answer.
        /// </summary>
        public bool IsExpanded { get; set; } = false;
    }

/// <summary>
    /// Represents weekly step data including a week range, total steps and calories, and the type of activity.
    /// </summary>
    public class WeeklyStepData
    {
        /// <summary>
        /// Gets or sets the range of the week (e.g., 'June 1 - June 7').
        /// </summary>
        public string WeekRange { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total number of steps taken during the week.
        /// </summary>
        public int TotalSteps { get; set; }

        /// <summary>
        /// Gets or sets the total number of calories burned during the week.
        /// </summary>
        public int TotalCalories { get; set; }

        /// <summary>
        /// Gets or sets the type of activity performed during the week.
        /// </summary>
        public string ActivityType { get; set; } = string.Empty;
    }
}

