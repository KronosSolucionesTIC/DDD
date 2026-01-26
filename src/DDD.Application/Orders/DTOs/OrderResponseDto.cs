namespace DDD.Application.Orders.DTOs

{
    public class OrderResponseDto
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }


}
