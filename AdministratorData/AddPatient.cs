using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Assignment1.AdministratorData
{
    public class AddPatient : IDisposable
    {
        private bool disposed = false;

        public void Execute()
        {
            Console.Clear(); // Clear the console to display fresh information

            // Display the menu header
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│      DOTNET Hospital Management System    │");
            Console.WriteLine("├───────────────────────────────────────────┤");
            Console.WriteLine("│                Add Patient                │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            Console.WriteLine("Registering a new patient with the DOTNET Hospital Management System");

            // Prompt for patient details
            string firstName = GetValidatedInput("First Name: ");
            string lastName = GetValidatedInput("Last Name: ");
            string email = GetValidatedEmail("Email: ");
            string password = ReadPassword();
            string phone = GetValidatedPhone("Phone: ");
            string streetNumber = GetValidatedInput("Street Number: ");
            string street = GetValidatedInput("Street: ");
            string city = GetValidatedInput("City: ");
            string state = GetValidatedInput("State: ");

            // Combine address into one field
            string address = $"{streetNumber} {street}, {city}, {state}";

            // Generate a unique ID for the patient
            string patientId = GeneratePatientId();

            // Create a formatted string for the new patient entry
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

        // Generate a unique patient ID (Starts with '3')
        private string GeneratePatientId()
        {
            Random random = new Random();
            string patientId = "3" + random.Next(0, 10000).ToString("D4");

            while (IdExists(patientId)) // Ensure ID is unique
            {
                patientId = "3" + random.Next(0, 10000).ToString("D4");
            }

            return patientId;
        }

        // Check if the ID already exists in the data.txt
        private bool IdExists(string id)
        {
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
            string filePath = Path.Combine(projectDirectory, "data.txt");

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (line.Contains($"ID: {id}"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Validate email using a regex pattern
        private string GetValidatedEmail(string prompt)
        {
            string email;
            do
            {
                Console.Write(prompt);
                email = Console.ReadLine();
            } while (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"));
            return email;
        }

        // Validate that the input is non-empty and not null
        private string GetValidatedInput(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
            } while (string.IsNullOrEmpty(input));
            return input;
        }

        // Validate phone number to accept only numbers or + (international format)
        private string GetValidatedPhone(string prompt)
        {
            string phone;
            do
            {
                Console.Write(prompt);
                phone = Console.ReadLine();
            } while (!Regex.IsMatch(phone, @"^\+?[0-9]+$"));
            return phone;
        }

        // Read password with minimum 3 characters
        private string ReadPassword()
        {
            string password;
            do
            {
                Console.Write("Password (min 3 characters): ");
                password = string.Empty;
                ConsoleKeyInfo key;

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
            } while (password.Length < 3);
            return password;
        }

        // IDisposable Implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Cleanup managed resources
                }
                disposed = true;
            }
        }
    }

    // Extension Method for Patient
    public static class PatientExtensions
    {
        public static string FormatPatientDetails(this string patient)
        {
            return $"Patient Details: {patient}";
        }
    }
}
