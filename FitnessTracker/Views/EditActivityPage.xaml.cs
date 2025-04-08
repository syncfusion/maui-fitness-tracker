using Syncfusion.Maui.Picker;

namespace FitnessTracker
{
	public partial class EditActivityPage : ContentPage
	{
        List<string> activityList = new List<string> { "Walking", "Running", "Cycling", "Swimming", "Yoga", "Sleeping" };
        public EditActivityPage (FitnessActivity activity)
		{
			InitializeComponent ();
            activityBox.ItemsSource = activityList;
            activityBox.SelectedItem = activity.ActivityType;
            BindingContext = activity;
		}

        void datePickerEntry_Focused(object sender, FocusEventArgs e)
        {
            datePicker.IsOpen = true;
        }

        void DatePicker_Tapped(object sender, TappedEventArgs e)
        {
            datePicker.IsOpen = true;
        }

        void datePicker_SelectionChanged(object sender, Syncfusion.Maui.Picker.DatePickerSelectionChangedEventArgs e)
        {
            if (datePicker.SelectedDate != null)
            {
                datePickerEntry.Text = datePicker.SelectedDate.Value.ToString("MM/dd/yyyy");
            }
        }

        void startTimePickerEntry_Focused(object sender, FocusEventArgs e)
        {
            startTimePicker.IsOpen = true;
        }

        void StartTimePicker_Tapped(object sender, TappedEventArgs e)
        {
            startTimePicker.IsOpen = true;
        }

        void startTimePicker_SelectionChanged(object sender, TimePickerSelectionChangedEventArgs e)
        {
            if (startTimePicker.SelectedTime != null)
            {
                startTimePickerEntry.Text = DateTime.Today.Add(startTimePicker.SelectedTime.Value).ToString("hh:mm tt");
            }
        }

        void endTimePickerEntry_Focused(object sender, FocusEventArgs e)
        {
            endTimePicker.IsOpen = true;
        }

        void EndTimePicker_Tapped(object sender, TappedEventArgs e)
        {
            endTimePicker.IsOpen = true;
        }

        void endTimePicker_SelectionChanged(object sender, TimePickerSelectionChangedEventArgs e)
        {
            if (endTimePicker.SelectedTime != null)
            {
                endTimePickerEntry.Text = DateTime.Today.Add(endTimePicker.SelectedTime.Value).ToString("hh:mm tt");
            }
        }

        void CloseIcon_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PopAsync();
        }

        void OnSaveTapped(object sender, TappedEventArgs e)
        {
            if(BindingContext is FitnessActivity activity)
            {
                double energy = double.TryParse(energyExpended.Text, out var value) ? value : 0;
                activity.CaloriesBurned = energy;
                activity.ActivityType = (string)activityBox.SelectedItem!;
                activity.Title = activityTitle.Text;
                activity.Remarks = remarks.Text;
                if(datePicker.SelectedDate is not null)
                {
                    DateTime selectedDate = (DateTime)datePicker.SelectedDate;
                    if (startTimePicker.SelectedTime is TimeSpan startTime)
                    {
                        activity.StartTime = selectedDate.Date + startTime;
                    }

                    if (endTimePicker.SelectedTime is TimeSpan endTime)
                    {
                        activity.EndTime = selectedDate.Date + endTime;
                    }
                }
            }

            Navigation.PopAsync();
        }
    }
}