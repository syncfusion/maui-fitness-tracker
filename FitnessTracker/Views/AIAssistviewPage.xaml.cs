using Syncfusion.Maui.Core;
namespace FitnessTracker;

public partial class AIAssistViewPage : ContentPage
{
	public AIAssistViewPage()
	{
		InitializeComponent();
	}

    void CloseButton_Tapped(object sender, TappedEventArgs e)
    {
		Navigation.PopAsync();
    }

    void OnChipAdded(object sender, ElementEventArgs e)
    {
        if (e.Element is Grid grid)
        {
            foreach (var child in grid.Children)
            {
                if (child is SfChip chip)
                {
                    chip.Command = viewModel.ChipCommand;
                    chip.CommandParameter = chip.Text; // or chip.BindingContext if needed
                }
            }
        }
    }
}