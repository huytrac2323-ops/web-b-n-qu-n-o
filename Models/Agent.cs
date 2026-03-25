namespace Demo.Models;

public class Agent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Company { get; set; }
    public string Status { get; set; }
    public DateTime? RunDate { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal Performance { get; set; }
}
