namespace DDD.Application.Orders.DTOs
{
    public class UpdateOrderRequest
    {
        public Guid ClientId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
    }
}
