
namespace FitnessTracker.Views
{
    public partial class AddActivityPage : ContentPage
    {
        List<string> activityList = new List<string> { "Walking", "Running", "Cycling", "Swimming", "Hiking", "Aerobics", "Elliptical Training", "Strength Training", "Stair Climbing", "Yoga", "Dancing", "Martial Arts", "Pilates", "Meditation", "Rowing", "CrossFit" };
        public AddActivityPage()
        {
            InitializeComponent();
            activityBox.ItemsSource = activityList;
        }
    }
}