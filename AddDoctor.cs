using System;
using System.IO;

namespace Assignment1
{
    public class AddDoctor
    {
        public void Execute()
        {
            Console.Clear(); // Clear the console to display fresh information

            // Display the menu header
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│      DOTNET Hospital Management System     │");
            Console.WriteLine("├───────────────────────────────────────────┤");
            Console.WriteLine("│                Add Doctor                 │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            Console.WriteLine("Registering a new doctor with the DOTNET Hospital Management System");

            //  doctor details
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Phone: ");
            string phone = Console.ReadLine();

            Console.Write("Street Number: ");
            string streetNumber = Console.ReadLine();

            Console.Write("Street: ");
            string street = Console.ReadLine();

            Console.Write("City: ");
            string city = Console.ReadLine();

            Console.Write("State: ");
            string state = Console.ReadLine();

            // doctor password
            string password = ReadPassword();

            // Combine address into one field
            string address = $"{streetNumber} {street}, {city}, {state}";

            // Generate a unique ID for the doctor (starts with 2 and has 5 digits)
            string doctorId = GenerateDoctorId();

            // Create a formatted string for the new doctor entry, including password
            string newDoctorEntry = $"Doctor, ID: {doctorId}, Name: {firstName} {lastName}, Email: {email}, Password: {password}, Phone: {phone}, Address: {address}";

            // Write the new doctor to the data.txt file
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
            string filePath = Path.Combine(projectDirectory, "data.txt");

            try
            {
                File.AppendAllText(filePath, newDoctorEntry + Environment.NewLine);
                Console.WriteLine($"\nDr {firstName} {lastName} added to the system with ID {doctorId}!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }

            // Wait for the user to press any key to return to the menu
            Console.WriteLine("\nPress any key to return to the Administrator menu...");
            Console.ReadKey();
        }

        // Method to generate a unique doctor ID (starts with '2' and followed by 4 random digits)
        private string GenerateDoctorId()
        {
            Random random = new Random();
            // Generate a random number between 0000 and 9999, and prepend "2" to make it 5 digits starting with 2
            string doctorId = "2" + random.Next(0, 10000).ToString("D4");
            return doctorId;
        }

        // Method to read the password with masked input
        private string ReadPassword()
        {
            string password = string.Empty;
            ConsoleKeyInfo key;

            Console.Write("Password: ");

            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }
    }
}
