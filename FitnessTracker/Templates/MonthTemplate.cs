﻿using Syncfusion.Maui.Calendar;

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
            if (ViewModel != null && ViewModel.dailySteps.TryGetValue(monthCellDetails!.Date, out int steps))
            {
                switch (steps)
                {
                    case > 1500:
                        return IntenseStepCountTemplate;
                    case > 1000:
                        return HighStepCountTemplate;
                    case > 500:
                        return MediumStepCountTemplate;
                    case > 200:
                        return LowStepCountTemplate;
                }
            }

            return DefaultStepCountTemplate;
        }
    }
}
