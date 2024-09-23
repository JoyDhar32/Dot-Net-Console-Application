using System;
using System.IO;
using System.Collections.Generic;

namespace Assignment1.DoctorData
{
    public class MyPatients
    {
        private string filePath;

        // Constructor to initialize file path
        public MyPatients()
        {
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
            filePath = Path.Combine(projectDirectory, "data.txt");
        }

        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│               My Patients                 │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            // Fetch and display patient list
            List<PatientDetails> patients = GetPatients();
            if (patients.Count == 0)
            {
                Console.WriteLine("No patients found.");
            }
            else
            {
                Console.WriteLine($"{"Patient Name",-20} | {"Email Address",-25} | {"Phone",-12} | {"Address",-30}");
                Console.WriteLine(new string('-', 90)); // Separator line

                // Loop through and display each patient
                foreach (var patient in patients)
                {
                    Console.WriteLine($"{patient.Name,-20} | {patient.Email,-25} | {patient.Phone,-12} | {patient.Address,-30}");
                }
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }

        // Method to read patients from data.txt
        private List<PatientDetails> GetPatients()
        {
            List<PatientDetails> patients = new List<PatientDetails>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                // Loop through each line and extract patient details
                foreach (string line in lines)
                {
                    if (line.StartsWith("Patient")) // Filter patient lines
                    {
                        string[] data = line.Split(',');

                        string name = ExtractField(data[2], "Name");
                        string email = ExtractField(data[3], "Email");
                        string phone = ExtractField(data[5], "Phone");
                        string address = ExtractField(data[6], "Address");

                        // Add the patient details to the list
                        patients.Add(new PatientDetails(name, email, phone, address));
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error: data.txt file not found at {filePath}");
            }

            return patients;
        }

        // Method to extract field values from formatted text (e.g., "Name: John Doe")
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

    // Class to hold patient details
    public class PatientDetails
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public PatientDetails(string name, string email, string phone, string address)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
        }
    }
}
