using System;
using System.IO;

namespace Assignment1
{
    public class AddPatient
    {
        public void Execute()
        {
            Console.Clear(); // Clear the console to display fresh information

            // Display the menu header
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│      DOTNET Hospital Management System     │");
            Console.WriteLine("├───────────────────────────────────────────┤");
            Console.WriteLine("│                Add Patient                │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            Console.WriteLine("Registering a new patient with the DOTNET Hospital Management System");

            // Prompt for patient details
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            // Prompt for patient password
            string password = ReadPassword();

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

            // Combine address into one field
            string address = $"{streetNumber} {street}, {city}, {state}";

            // Generate a unique ID for the patient (starts with 3 and has 5 digits)
            string patientId = GeneratePatientId();

            // Create a formatted string for the new patient entry, with the correct format
            string newPatientEntry = $"Patient, ID: {patientId}, Name: {firstName} {lastName}, Email: {email}, Password: {password}, Phone: {phone}, Address: {address}";


            // Write the new patient to the data.txt file
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
            string filePath = Path.Combine(projectDirectory, "data.txt");

            try
            {
                File.AppendAllText(filePath, newPatientEntry + Environment.NewLine);
                Console.WriteLine($"\nPatient {firstName} {lastName} added to the system with ID {patientId}!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }

            // Wait for the user to press any key to return to the menu
            Console.WriteLine("\nPress any key to return to the Administrator menu...");
            Console.ReadKey();
        }

        // Method to generate a unique patient ID (starts with '3' and followed by 4 random digits)
        private string GeneratePatientId()
        {
            Random random = new Random();
            // Generate a random number between 0000 and 9999, and prepend "3" to make it 5 digits starting with 3
            string patientId = "3" + random.Next(0, 10000).ToString("D4");
            return patientId;
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
