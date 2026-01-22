using DDD.Domain.Exceptions;

namespace DDD.Domain.Entities;

public class Client
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime RegistrationDate { get; private set; }
    public bool IsActive { get; private set; }

    private Client() { } // EF Core

    public Client(string name, string email)
    {
        Validate(name, email);

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        RegistrationDate = DateTime.UtcNow;
        IsActive = true;
    }

    public void Update(string name, string email)
    {
        Validate(name, email);

        Name = name;
        Email = email;
    }

    private static void Validate(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("El nombre del cliente es obligatorio");

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("El email del cliente es obligatorio");

        if (!email.Contains('@'))
            throw new DomainException("El email no tiene un formato válido");
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new DomainException("El cliente ya se encuentra desactivado");

        IsActive = false;
    }
}
