using System;
using System.IO;
using System.Collections.Generic;

namespace Assignment1.AdministratorData
{
    public class CheckPatientDetails
    {
        private string filePath;

        public CheckPatientDetails()
        {
            // Initialize the file path for data.txt
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
            filePath = Path.Combine(projectDirectory, "data.txt");
        }

        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│               Patient Details             │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            Console.WriteLine("Please enter the ID of the patient whose details you are checking. Or press 'n' to return to the menu:");
            string input = Console.ReadLine();

            if (input.ToLower() == "n")
            {
                // Return to the Administrator menu
                return;
            }

            // Try to find the patient with the input ID
            bool patientFound = false;

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                // Loop through each line in the file to find the patient
                foreach (string line in lines)
                {
                    if (line.StartsWith("Patient"))
                    {
                        string[] data = line.Split(',');

                        string patientId = ExtractField(data[1], "ID");

                        if (patientId == input)
                        {
                            patientFound = true;
                            string name = ExtractField(data[2], "Name");
                            string email = ExtractField(data[3], "Email");
                            string phone = ExtractField(data[5], "Phone");
                            string address = string.Join(",", data, 6, data.Length - 6).Trim(); // Full address

                            // Display the patient's details
                            Console.WriteLine($"\nDetails for {name}:");
                            Console.WriteLine("─────────────────────────────────────────────────────────────────────────────");
                            Console.WriteLine($"Name                 | Email Address             | Phone       | Address");
                            Console.WriteLine("------------------------------------------------------------------------------");
                            Console.WriteLine($"{name.PadRight(20)} | {email.PadRight(25)} | {phone.PadRight(12)} | {address}");
                            Console.WriteLine("─────────────────────────────────────────────────────────────────────────────");

                            break;
                        }
                    }
                }

                if (!patientFound)
                {
                    Console.WriteLine($"\nNo patient found with ID: {input}");
                }
            }
            else
            {
                Console.WriteLine("\nData file not found.");
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
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
