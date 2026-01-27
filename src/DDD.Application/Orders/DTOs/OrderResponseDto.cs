namespace DDD.Application.Orders.DTOs

{
    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public Guid IdCliente { get; set; }
        public string? NombreCliente { get; set; }
        public string? EmailCliente { get; set; }
        public decimal Total { get; set; }
    }
}
