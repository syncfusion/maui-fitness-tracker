using FitnessTracker.Models;
using Microsoft.Maui.Platform;
namespace FitnessTracker.Views;

public partial class HelpPage : ContentPage
{
	public HelpPage()
	{
		InitializeComponent();
	}

    private void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item != null)
        {
            var tappedItem = e.Item as FAQ;
            tappedItem.IsExpanded = !tappedItem.IsExpanded;
            faqListView.ItemsSource = null;
            var viewModel = (FitnessViewModel)this.BindingContext;
            var faqs = viewModel.FAQs;
            faqListView.ItemsSource = faqs;
        }
    }

    private void aiassistbutton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AIAssistview());
    }

}