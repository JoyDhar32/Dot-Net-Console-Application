using System;

namespace Assignment1.DoctorData
{
    public class MyDetails
    {
        private Doctor _loggedInDoctor;

        // Constructor to initialize with the logged-in doctor's details
        public MyDetails(Doctor loggedInDoctor)
        {
            _loggedInDoctor = loggedInDoctor;
        }

        // Method to display the doctor details
        public void Execute()
        {
            Console.Clear(); // Clear the console

            // Display the header
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│      DOTNET Hospital Management System     │");
            Console.WriteLine("├───────────────────────────────────────────┤");
            Console.WriteLine("│                 My Details                │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            // Display the logged-in doctor's details
            Console.WriteLine("Name                 | Email Address             | Phone       | Address");
            Console.WriteLine("─────────────────────────────────────────────────────────────────────────────");
            Console.WriteLine($"{_loggedInDoctor.Name.PadRight(20)} | {_loggedInDoctor.Email.PadRight(25)} | {_loggedInDoctor.Phone.PadRight(12)} | {_loggedInDoctor.Address}");

            // Wait for user input before returning to the menu
            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }
    }
}
