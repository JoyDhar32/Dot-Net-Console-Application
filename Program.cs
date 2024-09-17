using System;
using System.IO;

namespace Assignment1
{
    internal class Program
    {
        static void Main(string[] args)
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

                        if (data.Length >= 3) // Ensure there are at least 3 components (role, id, password)
                        {
                            string role = data[0].Trim();
                            string id = data[1].Contains(":") ? data[1].Split(':')[1].Trim() : string.Empty;
                            string password = data[3].Contains(":") ? data[3].Split(':')[1].Trim() : string.Empty;

                            // Check if the entered ID and password match
                            if (id == inputId && password == inputPassword)
                            {
                                isAuthenticated = true;
                                Console.Clear(); // Clear the screen after login
                                if (role == "Administrator")
                                {
                                    Console.WriteLine("\nHello Admin!");
                                }
                                else if (role == "Patient")
                                {
                                    Console.WriteLine("\nHello Patient!");
                                }
                                else if (role == "Doctor")
                                {
                                    Console.WriteLine("\nHello Doctor!");
                                }
                                Console.ReadKey();
                                return; // Exit the application after successful login
                            }
                        }
                    }

                    if (!isAuthenticated)
                    {
                        Console.WriteLine("\nInvalid credentials. Please try again.\n");
                    }
                }
                else
                {
                    Console.WriteLine("Data file not found.");
                    Console.ReadKey();
                    return; // Exit if the file is not found
                }
            }
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

        // Method to display the login menu with a design similar to the provided image
        public static void DisplayLoginMenu()
        {
            Console.Clear(); // Clears the console to display a fresh menu
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│      DOTNET Hospital Management System    │");
            Console.WriteLine("├-------------------------------------------|");
            Console.WriteLine("│                   Login                   │");
            Console.WriteLine("└───────────────────────────────────────────┘");
            Console.WriteLine();
        }
    }
}
