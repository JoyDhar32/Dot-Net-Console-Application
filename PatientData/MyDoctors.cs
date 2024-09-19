using System;

namespace Assignment1.PatientData
{
    public class MyDoctors
    {
        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│          My Doctor Details                │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            // Simulate fetching doctor's details
            Console.WriteLine("Doctor Name: Dr. John Doe");
            Console.WriteLine("Specialty: General Practitioner");
            Console.WriteLine("Contact: 123-456-7890");
            Console.WriteLine("Email: john.doe@hospital.com");

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }
    }
}
