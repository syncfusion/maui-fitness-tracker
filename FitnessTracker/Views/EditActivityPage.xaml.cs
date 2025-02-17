
using Syncfusion.Maui.Picker;

namespace FitnessTracker.Views
{
	public partial class EditActivityPage : ContentPage
	{
        List<string> activityList = new List<string> { "Walking", "Running", "Cycling", "Swimming", "Hiking", "Aerobics", "Elliptical Training", "Strength Training", "Stair Climbing", "Yoga", "Dancing", "Martial Arts", "Pilates", "Meditation", "Rowing", "CrossFit" };
        public EditActivityPage ()
		{
			InitializeComponent ();
            activityBox.ItemsSource = activityList;
            activityBox.SelectedIndex = 0;
		}

        private void datePickerEntry_Focused(object sender, FocusEventArgs e)
        {
            datePicker.IsOpen = true;
        }

        private void DatePicker_Tapped(object sender, TappedEventArgs e)
        {
            datePicker.IsOpen = true;
        }

        private void datePicker_SelectionChanged(object sender, Syncfusion.Maui.Picker.DatePickerSelectionChangedEventArgs e)
        {
            if (datePicker.SelectedDate != null)
            {
                datePickerEntry.Text = datePicker.SelectedDate.Value.ToString("MM/dd/yyyy");
            }
        }

        private void startTimePickerEntry_Focused(object sender, FocusEventArgs e)
        {
            startTimePicker.IsOpen = true;
        }

        private void StartTimePicker_Tapped(object sender, TappedEventArgs e)
        {
            startTimePicker.IsOpen = true;
        }

        private void startTimePicker_SelectionChanged(object sender, TimePickerSelectionChangedEventArgs e)
        {
            if (startTimePicker.SelectedTime != null)
            {
                startTimePickerEntry.Text = DateTime.Today.Add(startTimePicker.SelectedTime.Value).ToString("hh:mm tt");
            }
        }

        private void endTimePickerEntry_Focused(object sender, FocusEventArgs e)
        {
            endTimePicker.IsOpen = true;
        }

        private void EndTimePicker_Tapped(object sender, TappedEventArgs e)
        {
            endTimePicker.IsOpen = true;
        }

        private void endTimePicker_SelectionChanged(object sender, TimePickerSelectionChangedEventArgs e)
        {
            if (endTimePicker.SelectedTime != null)
            {
                endTimePickerEntry.Text = DateTime.Today.Add(endTimePicker.SelectedTime.Value).ToString("hh:mm tt");
            }
        }
    }
}