namespace DDD.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid ClientId { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Total { get; private set; }
        public string Status { get; private set; }

        private Order() { }

        public Order(Guid clientId, decimal total, string status)
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
            Date = DateTime.UtcNow;
            Total = total;
            Status = status;
        }
    }

}
