namespace DDD.Application.Orders.Commands
{
    public class UpdateOrderCommand
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
