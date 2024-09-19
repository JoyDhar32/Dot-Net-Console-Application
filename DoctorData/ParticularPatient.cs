using System;
using System.IO;
using System.Linq;

namespace Assignment1.DoctorData
{
    public class ParticularPatient
    {
        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│            Check Particular Patient        │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            // Prompt the user to enter the patient ID
            Console.Write("Enter the ID of the patient to check: ");
            string patientIdInput = Console.ReadLine();

            // Path to the data.txt file
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
            string filePath = Path.Combine(projectDirectory, "data.txt");

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Data file not found.");
                Console.ReadKey();
                return;
            }

            // Read all lines from the data.txt file
            string[] lines = File.ReadAllLines(filePath);
            bool patientFound = false;

            // Search for the patient based on the entered ID
            foreach (string line in lines)
            {
                if (line.StartsWith("appointment"))
                {
                    string[] data = line.Split(',');

                    string patientId = ExtractField(data[1], "PatientId");
                    string patientName = ExtractField(data[2], "PatientName");
                    string doctorName = ExtractField(data[4], "DoctorName");
                    string email = "david@david.com"; // For simplicity, replace this with real data when available
                    string phone = "0412341234"; // For simplicity, replace this with real data when available
                    string address = "19 Real Street, Sydney, NSW"; // For simplicity, replace this with real data when available

                    // If the patient ID matches, display the details
                    if (patientId == patientIdInput)
                    {
                        patientFound = true;

                        // Display the patient's details
                        Console.WriteLine("\nPatient Details:");
                        Console.WriteLine("─────────────────────────────────────────────────────────────────────────────");
                        Console.WriteLine($"Patient      | Doctor            | Email              | Phone       | Address");
                        Console.WriteLine("─────────────────────────────────────────────────────────────────────────────");
                        Console.WriteLine($"{patientName.PadRight(12)} | {doctorName.PadRight(17)} | {email.PadRight(18)} | {phone.PadRight(12)} | {address}");
                        Console.WriteLine("─────────────────────────────────────────────────────────────────────────────");

                        break;
                    }
                }
            }

            // If the patient was not found, display an error message
            if (!patientFound)
            {
                Console.WriteLine("\nError: No patient found with that ID.");
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }

        // Method to extract field values from formatted text (e.g., "PatientId: 3")
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
