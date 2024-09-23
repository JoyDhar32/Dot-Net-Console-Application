using System;
using System.IO;
using System.Linq;

namespace Assignment1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StartLoginProcess();
        }

        // Move the login process to a separate method
        public static void StartLoginProcess()
        {
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
            string filePath = Path.Combine(projectDirectory, "data.txt");

            bool isAuthenticated = false;

            while (!isAuthenticated) // Loop until authenticated
            {
                // Display the login menu
                DisplayLoginMenu();

                // Ask for login details
                Console.Write("Enter ID: ");
                string inputId = Console.ReadLine();

                Console.Write("Enter Password: ");
                string inputPassword = ReadPassword();  // Hides the password while typing

                // Read the data.txt file and check for login credentials
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        string[] data = line.Split(',');

                        if (data.Length >= 6) // Ensure there are at least 6 components (role, id, name, email, password, phone, address)
                        {
                            string role = data[0].Trim(); // Role: Administrator, Doctor, Patient
                            string id = ExtractField(data[1], "ID"); // Extract ID
                            string name = ExtractField(data[2], "Name"); // Extract Name
                            string email = ExtractField(data[3], "Email"); // Extract Email
                            string password = ExtractField(data[4], "Password"); // Extract Password
                            string phone = ExtractField(data[5], "Phone"); // Extract Phone

                            string address = "";
                            if (data.Length > 6) // Check if address field exists
                            {
                                 address = string.Join(",", data, 6, data.Length - 6).Split(':')[1].Trim();
                            }

                            // Check if the entered ID and password match
                            if (id == inputId && password == inputPassword)
                            {
                                isAuthenticated = true;
                                Console.Clear(); // Clear the screen after login

                                User user = null;

                                // Instantiate the correct user type
                                if (role == "Administrator")
                                {
                                    user = new Administrator(id, password);
                                }
                                else if (role == "Patient")
                                {
                                    user = new Patient(id, password, name, email, phone, address);
                                }
                                else if (role == "Doctor")
                                {
                                    user = new Doctor(id, password, name, email, phone, address);
                                }

                                // Greet the user
                                if (user != null)
                                {
                                    user.Greet();
                                }

                                Console.ReadKey();
                                return; // Exit the application after successful login
                            }
                        }
                        else
                        {
                            // Skip invalid or incomplete records (like appointments) Which less 6
                          //  Console.WriteLine("Invalid record found, skipping...");
                        }

                    }

                    if (!isAuthenticated)
                    {
                        Console.WriteLine("\nInvalid credentials. Please try again.\n");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Data file not found.");
                    Console.ReadKey();
                    return;
                }
            }
        }

        // Helper method to extract field values
        private static string ExtractField(string data, string fieldName)
        {
            var parts = data.Split(':');
            if (parts.Length > 1)
            {
                return parts[1].Trim();
            }
            return string.Empty;
        }

        // Method to display the login menu with a design similar to the provided image
        public static void DisplayLoginMenu()
        {
            Console.Clear(); // Clears the console to display a fresh menu
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│      DOTNET Hospital Management System    │");
            Console.WriteLine("├───────────────────────────────────────────┤");
            Console.WriteLine("│                   Login                   │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");
        }

        // Method to hide password while typing
        public static string ReadPassword()
        {
            string password = string.Empty;
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
            return password;
        }
    }
}
