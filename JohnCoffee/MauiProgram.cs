using Microsoft.Extensions.Logging;

namespace JohnCoffee;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});



#if DEBUG
        builder.Logging.AddDebug();

#endif
        
        
        builder.Services.AddSingleton<ProductViewModel>();
        builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddTransient<ShoppingCartViewModel>();
        builder.Services.AddTransient<OrderHistoryViewModel>();
        builder.Services.AddTransient<ReciptViewModel>();
        builder.Services.AddTransient<OrderHistory>();
        builder.Services.AddTransient<ShoppingCart>();
        builder.Services.AddTransient<MenuUtility>();
        builder.Services.AddTransient<ProductPage>();
        builder.Services.AddTransient<ReciptPage>();
        builder.Services.AddTransient<MainPage>();
     
     
        return builder.Build();


    }
}
