namespace JohnCoffee.ViewModel;

/// <summary>
/// Class MainViewmodel used to display all product items stored locally
/// </summary>
public partial class MainViewModel: ParentViewModel
{
    // Create object menu service from class MenuService
    MenuUtility menuUtility;

    public ObservableCollection<Menu> Product { get; } = new();

    private readonly string jsonFile = "menu.json";
    public MainViewModel(MenuUtility menuUtility)
    {
        Heading = "Home";
        this.menuUtility = menuUtility;
        GetMenuAsync(jsonFile);
    }
    /// <summary>
    /// Method which uses shell to eneter product page
    /// this method turns object menu to a dictionary item, which is accessed by key values
    /// </summary>
    /// <param name="menu"></param>
    /// <returns></returns>

    [RelayCommand]
    async Task GoToProduct(Menu menu)
    {
        if (menu.Name == null) return;
        
        await Shell.Current.GoToAsync(nameof(ProductPage), true, new Dictionary<string, object>
        {
            {"Menu", menu }
        });
    }
    /// <summary>
    /// Method used to add menu items stored in resource folder to observable collection
    /// </summary>
    /// <param name="product"></param>

    public async void GetMenuAsync(string product)
    {
        try
        {
            var item = await menuUtility.GetMenu(product);

            // condition to clear menu for erroneous behaviour
            if (Product.Count != 0)
                Product.Clear();

            // loop through object menu and add to collection
            item.ForEach(Product.Add);

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get menu: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }

}
