namespace JohnCoffee;

public partial class MainPage : ContentPage
{

	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		//Dependancy injection 2-way
		BindingContext= viewModel;
	}

}

