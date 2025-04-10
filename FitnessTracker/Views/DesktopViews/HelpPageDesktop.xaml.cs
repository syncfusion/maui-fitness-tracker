namespace FitnessTracker;

public partial class HelpPageDesktop : ContentView
{
	public HelpPageDesktop()
	{
		InitializeComponent();
        BindingContext = new FitnessViewModel(Navigation);
    }
    void faqListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
    {
        if (e.DataItem is FAQ tappedItem)
        {
            tappedItem.IsExpanded = !tappedItem.IsExpanded;
            faqListView.ItemsSource = null;
            var viewModel = (FitnessViewModel)this.BindingContext;
            var faqs = viewModel.FAQs;
            faqListView.ItemsSource = faqs;
        }
    }

    void aiassistbutton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AIAssistViewPage());
    }

    void BackButton_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PopAsync();
    }
}