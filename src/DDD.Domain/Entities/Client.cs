namespace DDD.Domain.Entities;

public class Client
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime RegistrationDate { get; private set; }

    private Client() { }

    public Client(string name, string email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        RegistrationDate = DateTime.UtcNow;
    }
}
