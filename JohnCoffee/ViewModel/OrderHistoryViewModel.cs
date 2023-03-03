namespace JohnCoffee.ViewModel;
/// <summary>
/// Class orderHistoryViewmodel reads jsonfile stored
/// in app data and displays output data
/// </summary>
public class OrderHistoryViewModel: ParentViewModel
{
    // Instantiate object menuDetails from class MenuDetailsViewModel
   
    MenuUtility menuUtility;

    public ObservableCollection<Basket> History { get; } = new();

    private readonly string jsonFile = "orderhistory.json";
    /// <summary>
    /// Constructor which accepts menuutility as a parameter
    /// excutetes Histroydata on start up
    /// </summary>
    /// <param name="menuUtility"></param>
    public OrderHistoryViewModel(MenuUtility menuUtility)
    {
        Heading = "Home";
        this.menuUtility = menuUtility;
        GetHistoryAsync(jsonFile);
    }
    /// <summary>
    /// Read oder history file and add data to observable collection
    /// </summary>
    /// <param name="product"></param>
    public async void GetHistoryAsync(string product)
    {
        try
        {
            var item = await menuUtility.GetHistory(product);

            // condition to clear menu for erroneous behaviour
            if (History.Count != 0)
                History.Clear();

            item.ForEach(History.Add);

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"No File History Found: {ex.Message}");
            await Shell.Current.DisplayAlert("Error! No File History Found", ex.Message, "OK");
        }
    }
}
