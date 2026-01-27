namespace DDD.Application.Users.Dtos
{
    public class LoginResponseDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = default!;
        public bool CanAccessMenu { get; set; }
        public string Token { get; set; } = default!;
    }
}
