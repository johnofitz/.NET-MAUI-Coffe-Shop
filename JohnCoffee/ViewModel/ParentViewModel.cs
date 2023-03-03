namespace JohnCoffee.ViewModel;
/// <summary>
/// Interface implemented ObservableObject for checking 
/// allows interfaces for all basic class methods and generates
/// Source code.
/// Microsoft definied ICommand interface to provide a commanding
/// abstraction for the XMAL framework
/// </summary>
public partial class ParentViewModel: ObservableObject 
{

    // Source gennerators will  automatically complete through partial class generation 
    // This generates basic getters and setters
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    public bool isBusy;

    [ObservableProperty]
    public string heading;

    // Lambda function to check if not busy
    public bool IsNotBusy => !IsBusy;

    [ObservableProperty]
    public string firstN;

    [ObservableProperty]
    public string lastN;

    [ObservableProperty]
    public string num;

    [ObservableProperty]
    public string oldFirstN;

    [ObservableProperty]
    public string oldLastN;

    [ObservableProperty]
    public string oldNum;

}
