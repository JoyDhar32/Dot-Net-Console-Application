// Patient.cs
using System;

namespace Assignment1
{
    public class Patient : User
    {
        public Patient(string id, string password) : base(id, password)
        {
        }

        public override void Greet()
        {
            Console.WriteLine("Hello Patient!");
        }
    }
}
