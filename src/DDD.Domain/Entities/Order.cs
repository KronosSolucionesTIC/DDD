using DDD.Domain.Exceptions;

namespace DDD.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid ClientId { get; private set; }
        public DateTime Date { get; private set; }
        public decimal TotalAmount { get; private set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; private set; }

        private Order() { }

        public Order(Guid clientId, decimal totalAmount)
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
            TotalAmount = totalAmount;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;

            Validate();
        }

        private void Validate()
        {
            if (ClientId == Guid.Empty)
                throw new DomainException("ClientId es obligatorio");

            if (TotalAmount <= 0)
                throw new DomainException("El total debe ser mayor a 0");
        }

        public void UpdateClient(Guid clientId)
        {
            if (clientId == Guid.Empty)
                throw new DomainException("ClientId inválido");

            ClientId = clientId;
        }

        public void UpdateDate(DateTime date)
        {
            if (date == default)
                throw new DomainException("Fecha inválida");

            Date = date;
        }

        public void UpdateTotalAmount(decimal totalAmount)
        {
            if (totalAmount <= 0)
                throw new DomainException("El total debe ser mayor a 0");
            TotalAmount = totalAmount;
        }

        public void Deactivate()
        {
            if (!IsActive)
                throw new DomainException("La orden ya se encuentra desactivado");

            IsActive = false;
        }
    }

}
