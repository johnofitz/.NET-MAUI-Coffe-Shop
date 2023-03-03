namespace JohnCoffee.View;

public partial class ProductPage : ContentPage
{
	public ProductPage(ProductViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;
	}
}