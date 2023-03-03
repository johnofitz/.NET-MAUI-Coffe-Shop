namespace JohnCoffee.Model;

/// <summary>
/// Class Menu accepts Json data storeed in Resources folder
/// data is deserialized using Class Menu services and object
/// are passed.
/// </summary>
public class Menu
{
    public string Title { get; set; }
    public string Name { get; set; }
    public double? Price { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }

}

