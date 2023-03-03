namespace JohnCoffee.View;

public partial class ReciptPage : ContentPage
{
	public ReciptPage(ReciptViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}