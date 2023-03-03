
namespace JohnCoffee.ViewModel;
/// <summary>
/// Productviewmodel class controlls the items added to the cart and calculates the price of each
/// </summary>
[QueryProperty("Menu", "Menu")]
public partial class ProductViewModel : ParentViewModel
{
    // instantiate object basket from class Basket
    public Basket basket = new();
   
    public ProductViewModel() { }

   [ObservableProperty]
    Menu menu;

    [ObservableProperty]
    int counter;

    [ObservableProperty]
    string cost;

    [RelayCommand]
    async Task GoToCheckout()
    {
    
        await Shell.Current.GoToAsync(nameof(ShoppingCart));

       
        
    }
    /// <summary>
    /// Add To cart command, adds the current item viewed to object from class basket
    /// </summary>
    /// <returns></returns>

    [RelayCommand]
    async Task AddToCart()
    {
        if (IsBusy)
            return;

        try
        { 
            // For another order clear the last total
            if (basket.Orders.Count <= 0)
            {
                basket.Total *= 0;
            }
            // Condition to check if the count is legitimate
            if (counter== 0)
            {
                await Shell.Current.DisplayAlert("Error!", "Select Quantity", "OK");
                return;
            }
            // Add to object basket
            basket.Total += (double)Menu.Price * Counter;
            cost = basket.Total.ToString("0.00");
            basket.Orders.Add(new Order { OrderName = "X" + Counter.ToString() + " " + Menu.Name + " : € " + Menu.Price.ToString() });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get menu: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }

        // set busy to false after event and return counter to 0
        finally
        {
            IsBusy = false;
            Counter = 0;
        }
    }
    /// <summary>
    /// Increment counter command
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    async Task<int> IncrementCounter()
    {
        await Task.Delay(50);
        if (Counter >= 50) return Counter;

        Counter++;
        return Counter;
    }
    /// <summary>
    /// Decrement method
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    async Task<int> DecrementCounter()
    {
        await Task.Delay(50);
        if (Counter <= 0) return Counter;

        Counter--;
        return Counter;
    }

    /// <summary>
    /// Add items from product page to a list of object basket 
    /// used on the next page
    /// </summary>
    /// <returns></returns>
    public List<Basket> GetItems()
    {
        List<Basket> cart = new()
             {
                 new Basket()
                 {
                     Total= Math.Round(basket.Total,3),
                     Orders = basket.Orders
                 }
             };
        return cart;
    }


}
