using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class Administrator : User
    {
        public Administrator(string id, string email, string password) : base(id, email, password) { }

        public override void ShowMenu()
        {
            Console.WriteLine("Welcome to Administrator Menu!");
            // Add administrator-specific functionalities here
        }
    }
}
