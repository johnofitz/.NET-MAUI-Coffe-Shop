namespace JohnCoffee;

public partial class ShoppingCart : ContentPage
{
	public ShoppingCart(ShoppingCartViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;
	}
}