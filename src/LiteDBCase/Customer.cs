namespace LiteDBCase;
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string[] Phones { get; set; } = new string[]{};
    public bool IsActive { get; set; }
}