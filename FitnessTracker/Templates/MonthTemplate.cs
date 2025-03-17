using Syncfusion.Maui.Calendar;

namespace FitnessTracker.Templates
{
    public class MonthCellTemplateSelector : DataTemplateSelector
    {
        public FitnessViewModel ViewModel { get; set; }
        public DataTemplate IntenseStepCountTemplate { get; set; }
        public DataTemplate HighStepCountTemplate { get; set; }
        public DataTemplate MediumStepCountTemplate { get; set; }
        public DataTemplate LowStepCountTemplate { get; set; }
        public DataTemplate DefaultStepCountTemplate { get; set; }

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
