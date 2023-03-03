namespace JohnCoffee.ViewModel;
/// <summary>
/// ShoppingCartViewModel accepts the Entry info from xmal using binding
/// also adds the current date and time
/// </summary>
public partial class ShoppingCartViewModel: ParentViewModel
{


    // Instantiate object menuDetails from class MenuDetailsViewModel
    [ObservableProperty]
    ProductViewModel menuDetails;

    //List<Basket> processOrder = new();
    /// <summary>
    /// Constructor of Class BasketViewModel. Excutes GetCurrentBasket()
    /// and sets Title Name
    /// </summary>
    /// <param name="menuDetails"></param>
    
    public ShoppingCartViewModel(ProductViewModel menuDetails)
    {
        this.menuDetails = menuDetails;
        GetCurrentBasket();
        
        Heading = "Basket";
    }

    // Dynamic collection of Objects in Basket
   
    public ObservableCollection<Basket> Basket { get; } = new();

    [ObservableProperty]
    List<Basket> cart = new();

    /// <summary>
    /// Method GetCurrentBasket Access Method from MenuDetailsViewModel
    /// which contains the current instance of recipt items. This is 
    /// passed to  an Observable collection for xmal and called within 
    /// the constructor.
    /// </summary>
    public void GetCurrentBasket()
    {
        try
        {
            
            // Access to GetRecipts method
            var cart = menuDetails.GetItems();

            // Clear current observadle collection as not to duplicate
            if (Basket.Count != 0)
            {
                Basket.Clear();
            }
            // Add objects to Observable collection
            cart.ForEach(Basket.Add);

        }
        catch (Exception ex)
        {
            // Display error message on console and pop up Shell
            Debug.WriteLine($"Unable to get Basket: {ex.Message}");
            Shell.Current.DisplayAlert("Empty Basket", ex.Message, "OK");
        }
    }
    
    /// <summary>
    /// Method place oreder accepts all Input information
    /// Uses DateTime class to get the curent date and time 
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    public async Task GetPlaceOrder()
    {
        if (string.IsNullOrEmpty(FirstN))
        {
            await Shell.Current.DisplayAlert("Error!", "Name is blank", "OK");
            return;
            
        }
        if (string.IsNullOrEmpty(LastN))
        {
            await Shell.Current.DisplayAlert("Error!", "Surname is blank", "OK");
            return;
            
        }
        if (string.IsNullOrEmpty(Num))
        {
            await Shell.Current.DisplayAlert("Error!", "Number is blank", "OK");
            return;
            
        }
        else
        {
            DateTime localDate = DateTime.Now;
            var currentTime = localDate.ToString();

            Random random = new();
            int rand = random.Next(1, 1000);

            var placeOrder = menuDetails.GetItems();

            foreach (var it in placeOrder)
            {
                cart.Add(new Basket
                {
                    Id = rand,
                    First = FirstN,
                    Last = LastN,
                    Phone = Num,
                    FullName = FirstN + " " + LastN,
                    Total = it.Total,
                    Orders = it.Orders,
                    MyTime = currentTime
                });
            }
            // Write info to temp json file as this needs to be transient
            string json = JsonSerializer.Serialize(cart);
            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "temp.json");
            File.WriteAllText(targetFile, json);

            await Shell.Current.GoToAsync(nameof(ReciptPage), true);
            
        }
   
    }

}
