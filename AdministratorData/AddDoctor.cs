using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Assignment1.AdministratorData
{
    public class AddDoctor
    {
        public void Execute()
        {
            Console.Clear(); // Clear the console to display fresh information

            // Display the menu header
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│      DOTNET Hospital Management System    │");
            Console.WriteLine("├-------------------------------------------┤");
            Console.WriteLine("│                Add Doctor                 │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            Console.WriteLine("Registering a new doctor with the DOTNET Hospital Management System");

            // Validate first name
            string firstName = ReadRequiredField("First Name");

            // Validate last name
            string lastName = ReadRequiredField("Last Name");

            // Validate email
            string email = ReadValidEmail();

            // Validate phone number
            string phone = ReadValidPhoneNumber();

            // Validate street number
            string streetNumber = ReadValidStreetNumber();

            // Validate street
            string street = ReadRequiredField("Street");

            // Validate city
            string city = ReadRequiredField("City");

            // Validate state
            string state = ReadRequiredField("State");

            // Validate password
            string password = ReadPassword();

            // Combine address into one field
            string address = $"{streetNumber} {street}, {city}, {state}";

            // Generate a unique ID for the doctor (starts with 2 and has 5 digits)
            string doctorId = GenerateUniqueDoctorId();

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

        // Method to ensure a field is not left blank
        private string ReadRequiredField(string fieldName)
        {
            string input;
            do
            {
                Console.Write($"{fieldName}: ");
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine($"{fieldName} is required. Please enter a value.");
                }
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }

        // Method to generate a unique doctor ID (starts with '2' and followed by 4 random digits)
        private string GenerateUniqueDoctorId()
        {
            string doctorId;
            Random random = new Random();
            HashSet<string> existingIds = GetExistingDoctorIds();

            do
            {
                // Generate a random number between 0000 and 9999, and prepend "2" to make it 5 digits starting with 2
                doctorId = "2" + random.Next(0, 10000).ToString("D4");
            }
            while (existingIds.Contains(doctorId));

            return doctorId;
        }

        // Method to get all existing doctor IDs from data.txt
        private HashSet<string> GetExistingDoctorIds()
        {
            HashSet<string> ids = new HashSet<string>();
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
            string filePath = Path.Combine(projectDirectory, "data.txt");

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (line.StartsWith("Doctor"))
                    {
                        string[] data = line.Split(',');
                        string id = ExtractField(data[1], "ID");
                        ids.Add(id);
                    }
                }
            }

            return ids;
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

        // Method to validate email format
        private string ReadValidEmail()
        {
            string email;
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; // Simple email pattern

            do
            {
                Console.Write("Email: ");
                email = Console.ReadLine();

                if (!Regex.IsMatch(email, emailPattern))
                {
                    Console.WriteLine("Invalid email format. Please enter a valid email.");
                }

            } while (!Regex.IsMatch(email, emailPattern));

            return email;
        }

        // Method to validate phone number (only digits and '+')
        private string ReadValidPhoneNumber()
        {
            string phone;
            string phonePattern = @"^[0-9+]+$"; // Phone number should contain only digits and '+'

            do
            {
                Console.Write("Phone (only numbers and '+'): ");
                phone = Console.ReadLine();

                if (!Regex.IsMatch(phone, phonePattern))
                {
                    Console.WriteLine("Invalid phone number. Please enter a valid phone number.");
                }

            } while (!Regex.IsMatch(phone, phonePattern));

            return phone;
        }

        // Method to validate street number (only digits)
        private string ReadValidStreetNumber()
        {
            string streetNumber;
            string numberPattern = @"^\d+$"; // Street number should contain only digits

            do
            {
                Console.Write("Street Number (only numbers): ");
                streetNumber = Console.ReadLine();

                if (!Regex.IsMatch(streetNumber, numberPattern))
                {
                    Console.WriteLine("Invalid street number. Please enter a valid number.");
                }

            } while (!Regex.IsMatch(streetNumber, numberPattern));

            return streetNumber;
        }

        // Helper method to extract field value based on format (e.g., "ID: 1")
        private string ExtractField(string data, string fieldName)
        {
            var parts = data.Split(':');
            if (parts.Length > 1)
            {
                return parts[1].Trim();
            }
            return string.Empty;
        }
    }
}
