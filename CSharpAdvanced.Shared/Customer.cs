namespace CSharpAdvanced.Shared;

public class Customer : BaseEntity
{
    public Customer(int id, string name)
    {
        Id = id;
        Name = name;
        CreatedAt = DateTime.Now;
    }

    public Customer(string name)
    {
        Name = name;
        CreatedAt = DateTime.Now;
    }

    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}