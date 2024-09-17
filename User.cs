// User.cs
namespace Assignment1
{
    public abstract class User
    {
        public string Id { get; set; }
        public string Password { get; set; }

        public User(string id, string password)
        {
            Id = id;
            Password = password;
        }

        public abstract void Greet();
    }
}
