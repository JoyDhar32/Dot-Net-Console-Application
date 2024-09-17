using System;

namespace Assignment1
{
    public class Doctor : User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Doctor(string id, string password, string name, string email, string phone, string address)
            : base(id, password)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
        }

        // Override the ToString method to display doctor details in shorthand format
        public override string ToString()
        {
            return $"{Name,-20} | {Email,-25} | {Phone,-12} | {Address}";
        }

        public override void Greet()
        {
            Console.WriteLine("\nHello Doctor!");
        }
    }
}
