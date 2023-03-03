namespace JohnCoffee.Utility;

public class MenuUtility
{
    // Pass class Menu as list
    List<Menu> menu = new();

    List<Basket> history = new();

    public async Task<List<Menu>> GetMenu(string file)
    {
        // Condition to check if menu is already loaded and not null
        if (menu?.Count > 0)
            return menu;

        // OpenAppPackageFileAsync as file is on application
        using var stream = await FileSystem.OpenAppPackageFileAsync(file);
        using var reader = new StreamReader(stream);
        var items = await reader.ReadToEndAsync();
        menu = JsonSerializer.Deserialize<List<Menu>>(items);

        return menu;
    }

    public async Task<List<Basket>> GetHistory(string file)
    {
        // Condition to check if menu is already loaded and not null
        if (history?.Count > 0)
            return history;

        // OpenAppPackageFileAsync as file is on application
        string stream = Path.Combine(FileSystem.Current.AppDataDirectory, file);
        using var reader = new StreamReader(stream);
        var items = await reader.ReadToEndAsync();
        history = JsonSerializer.Deserialize<List<Basket>>(items);

        return history;
    }

}
