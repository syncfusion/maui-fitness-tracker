namespace FitnessTracker
{
    public partial class MainPageDesktop : ContentPage
    {
        Border _selectedBorder;

        public MainPageDesktop()
        {
            InitializeComponent();
            selectedtab.BindingContext = new FitnessViewModel(Navigation);
            // Set default selection
            _selectedBorder = HomeBorder;
            SetSelected(HomeBorder);

            // Add Tap Gestures
            AddTapGesture(HomeBorder);
            AddTapGesture(ActivityBorder);
            AddTapGesture(JournalBorder);
            AddTapGesture(GoalBorder);
        }

        void AddTapGesture(Border border)
        {
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) => SetSelected(border);
            border.GestureRecognizers.Add(tapGesture);
        }

        void SetSelected(Border border)
        {
            // Reset previous selection
            if (_selectedBorder != null)
            {
                _selectedBorder.BackgroundColor = Colors.Transparent;

                // Reset color of all spans inside the previous selection
                var prevFormattedText = ((Label)_selectedBorder.Content).FormattedText;
                if (prevFormattedText != null)
                {
                    foreach (var span in prevFormattedText.Spans)
                    {
                        span.TextColor = Color.FromArgb("#313032"); // Default color
                    }
                }
            }

            // Set new selection
            border.BackgroundColor = Color.FromArgb("#7633DA");

            // Update color of all spans inside the newly selected border
            var formattedText = ((Label)border.Content).FormattedText;
            if (formattedText != null)
            {
                foreach (var span in formattedText.Spans)
                {
                    span.TextColor = Colors.White; // Highlight color
                }
            }

            _selectedBorder = border;
        }

        void OnCreateTapped(object sender, TappedEventArgs e)
        {
            createpopup.ShowRelativeToView(createbutton, Syncfusion.Maui.Popup.PopupRelativePosition.AlignBottom);
        }
    }
}