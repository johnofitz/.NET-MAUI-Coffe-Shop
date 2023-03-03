namespace JohnCoffee;

public partial class OrderHistory : ContentPage
{
	public OrderHistory(OrderHistoryViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;
	}
}