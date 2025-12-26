namespace DDD.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public string PassWordHash { get; private set; }

        protected User() { }

        public User(Guid id, string username, string passwordHash)
        {
            Id = id;
            UserName = username;
            PassWordHash = passwordHash;
        }

        public bool ValidatePassword(string passwordHash)
        {
            return PassWordHash == passwordHash;
        }
    }
}
