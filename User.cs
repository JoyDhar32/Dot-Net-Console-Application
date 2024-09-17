using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(string id, string email, string password)
        {
            Id = id;
            Email = email;
            Password = password;
        }

        public virtual void ShowMenu()
        {
            Console.WriteLine("Welcome to the system. Please select an option.");
        }
    }
}
