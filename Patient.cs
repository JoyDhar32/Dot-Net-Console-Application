using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class Patient : User
    {
        public Patient(string id, string email, string password) : base(id, email, password) { }

        public override void ShowMenu()
        {
            Console.WriteLine("Welcome to Patient Menu!");
            // Add patient-specific functionalities here
        }
    }
}
