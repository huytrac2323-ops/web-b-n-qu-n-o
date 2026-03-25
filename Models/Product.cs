namespace Demo.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Image { get; set; }
    public string Category { get; set; } = "Fashion";
    public double Rating { get; set; } = 5.0;
    public int Reviews { get; set; } = 0;
    public string Description { get; set; } = "";
}