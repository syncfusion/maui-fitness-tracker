using Syncfusion.Maui.Calendar;

#nullable disable
namespace FitnessTracker
{
    /// <summary>
    /// Selects a data template for calendar cells based on the activity type and level of activity data.
    /// </summary>
    public class MonthCellTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Gets or sets the view model containing activity data used for template selection.
        /// </summary>
        public FitnessViewModel ViewModel { get; set; }

        /// <summary>
        /// Gets or sets the template for cells with intense step counts or calorie usage.
        /// </summary>
        public DataTemplate IntenseStepCountTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for cells with high step counts or calorie usage.
        /// </summary>
        public DataTemplate HighStepCountTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for cells with medium step counts or calorie usage.
        /// </summary>
        public DataTemplate MediumStepCountTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for cells with low step counts or calorie usage.
        /// </summary>
        public DataTemplate LowStepCountTemplate { get; set; }

        /// <summary>
        /// Gets or sets the default template for cells with minimal or no activity data.
        /// </summary>
        public DataTemplate DefaultStepCountTemplate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonthCellTemplateSelector"/> class.
        /// </summary>
        public MonthCellTemplateSelector()
        {
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var monthCellDetails = item as CalendarCellDetails;
            if (ViewModel != null && ViewModel.DailySteps.TryGetValue(monthCellDetails!.Date, out var data))
            {
                switch(ViewModel.SelectedActivityType)
                {
                    case "Walking":
                    case "Running":
                        {
                            switch (data.Steps)
                            {
                                case > 5000:
                                    return IntenseStepCountTemplate;
                                case > 3000:
                                    return HighStepCountTemplate;
                                case > 2000:
                                    return MediumStepCountTemplate;
                                case > 1000:
                                    return LowStepCountTemplate;
                            }
                        }
                        break;
                    case "Cycling":
                    case "Swimming":
                        {
                            switch (data.Calories)
                            {
                                case > 1200:
                                    return IntenseStepCountTemplate;
                                case > 900:
                                    return HighStepCountTemplate;
                                case > 600:
                                    return MediumStepCountTemplate;
                                case > 300:
                                    return LowStepCountTemplate;
                            }
                        }
                        break;
                    case "Yoga":
                    case "Sleeping":
                        {
                            switch (data.Calories)
                            {
                                case > 400:
                                    return IntenseStepCountTemplate;
                                case > 300:
                                    return HighStepCountTemplate;
                                case > 200:
                                    return MediumStepCountTemplate;
                                case > 100:
                                    return LowStepCountTemplate;
                            }
                        }
                        break;
                }
            }

            return DefaultStepCountTemplate;
        }
    }
}
