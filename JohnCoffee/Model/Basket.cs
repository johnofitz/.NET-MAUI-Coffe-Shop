namespace JohnCoffee.Model;

public class Basket
{
    public string First { get; set; }
    public string Last { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public int Id { get; set; }
    public List<Order> Orders { get; set; } = new List<Order>();
    public double Total { get; set; }

    public string MyTime { get; set; }

}
