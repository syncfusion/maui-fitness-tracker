namespace FitnessTracker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SfTabView_TabItemTapped(object sender, Syncfusion.Maui.Toolkit.TabView.TabItemTappedEventArgs e)
        {
            if(e.TabItem!.Header != "Home")
            {
                headerLabel.Text = e.TabItem!.Header;
            }
            else
            {
                headerLabel.Text = "";
            } 
        }

        private void Hamburger_Tapped(object sender, TappedEventArgs e)
        {

        }

        private void Notification_Tapped(object sender, TappedEventArgs e)
        {

        }

        private void ProfilePhoto_Tapped(object sender, TappedEventArgs e)
        {

        }

        private void AddIcon_Clicked(object sender, EventArgs e)
        {
            addButton.IsVisible = false;
            overlayGrid.IsVisible = floatingButtonGrid.IsVisible = true;
        }

        private void CloseIcon_Clicked(object sender, EventArgs e)
        {
            addButton.IsVisible = true;
            overlayGrid.IsVisible = floatingButtonGrid.IsVisible = false;
        }

        private void OverlayGrid_Tapped(object sender, TappedEventArgs e)
        {

        }
    }

}
