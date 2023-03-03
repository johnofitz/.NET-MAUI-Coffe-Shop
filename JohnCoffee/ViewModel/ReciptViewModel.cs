namespace JohnCoffee.ViewModel;
/// <summary>
/// Class reciptViewmodel Combines all information and write to a 
/// json file stored in appData called orderhistory.json, also clears all items from all other pages,
/// so a new instance can be completed
/// </summary>
public partial class ReciptViewModel: ParentViewModel
{
    // Instantiate object menuDetails from class MenuDetailsViewModel
    private readonly ShoppingCartViewModel reciptDetails;

    MenuUtility utility;

    public ObservableCollection<Basket> CurrentOrder { get; } = new();
    /// <summary>
    /// Constructor accepts object from class Shoppingcart and menuUtility to read files
    /// this method also excutes async task GetRecipt on entry of page
    /// </summary>
    /// <param name="reciptDetails"></param>
    /// <param name="utility"></param>
    public ReciptViewModel(ShoppingCartViewModel reciptDetails, MenuUtility utility)
    {
        this.reciptDetails= reciptDetails;
        this.utility=utility;
        GetReciptCommand.Execute(this);
    }

    /// <summary>
    /// Method used to write data to  a json file 
    /// this method reads data stored in a temp json file created in the shopping cart page
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    async Task GetRecipt()
    {
        try
        {
            // Call temp.json file
            var item = await utility.GetHistory("temp.json");

            // condition to clear menu for erroneous behaviour
            if (CurrentOrder.Count != 0)
                CurrentOrder.Clear();

            item.ForEach(CurrentOrder.Add);

            Basket history = new();
            // Add all items to histrory object
            foreach (var it in item)
            {

                history.Id = it.Id;
                history.First = it.First;
                history.Last = it.Last;
                history.Phone = it.Phone;
                history.FullName = it.First + " " + it.Last;
                history.Total = Math.Round(it.Total, 3, MidpointRounding.AwayFromZero);
                history.Orders = it.Orders;
                history.MyTime = it.MyTime;
        
            }

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "orderhistory.json");


            string json;
            // Create list of basket class
            List<Basket> logging = new();
            // check if file dosent exist and create one
            if (!File.Exists(targetFile))
            {
                logging.Add(history);
                json = JsonSerializer.Serialize(logging);
                File.WriteAllText(targetFile, json);

               
            }
            // if not read old file and add new items
            else
            {
                json = File.ReadAllText(targetFile);
                logging = JsonSerializer.Deserialize<List<Basket>>(json);
                logging.Add(history);
                string newJson = JsonSerializer.Serialize(logging);
                File.WriteAllText(targetFile, newJson);
            }

        }
        catch (Exception ex)
        {
            // Displays errors on console and pop up
            Debug.WriteLine($"Unable to get order: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }
    /// <summary>
    /// Method that clears previous pages and returns to home
    /// </summary>
    /// <returns></returns>

    [RelayCommand]
    async Task GetHistoryRecipt()
    {
        try
        {
            await Shell.Current.DisplayAlert("Success", "Order Complete", "OK");
        }
        catch (Exception ex)
        {
            // Displays errors on console and pop up
            Debug.WriteLine($"Unable to get menu: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            // Clear all entry data
            reciptDetails.FirstN = string.Empty;
            reciptDetails.LastN = string.Empty; 
            reciptDetails.Num = string.Empty;
            reciptDetails.Cart.Clear();

            foreach (var item in reciptDetails.Basket)
            {
                item.Orders.Clear();
            }
          
       
            // Move back to home page
            await Shell.Current.GoToAsync("..//..//..");
        }
    }

}
