using System;

namespace Assignment1
{
    // Assuming a base User class
    public class User
    {
        public string Id { get; set; }
        public string Password { get; set; }

        // Constructor for User class
        public User(string id, string password)
        {
            Id = id;
            Password = password;
        }

        // Greet method to be overridden in derived classes like Patient
        public virtual void Greet()
        {
            Console.WriteLine("Hello, user!");
        }
    }
}
