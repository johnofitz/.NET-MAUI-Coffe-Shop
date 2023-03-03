namespace JohnCoffee;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        // Static registration of route throught Shell
        Routing.RegisterRoute(nameof(ProductPage), typeof(ProductPage));
        Routing.RegisterRoute(nameof(ShoppingCart), typeof(ShoppingCart));
        Routing.RegisterRoute(nameof(ReciptPage), typeof(ReciptPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}
