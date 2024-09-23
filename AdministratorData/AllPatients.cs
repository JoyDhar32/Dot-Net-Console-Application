using System;
using System.IO;
using System.Collections.Generic;

namespace Assignment1.AdministratorData
{
    public class AllPatients
    {
        private string filePath;

        public AllPatients()
        {
            // Initialize file path to data.txt
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
            filePath = Path.Combine(projectDirectory, "data.txt");
        }

        public void Execute()
        {
            Console.Clear();
            // Display the menu header
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│      DOTNET Hospital Management System    │");
            Console.WriteLine("├-------------------------------------------┤");
            Console.WriteLine("│                All Patients               │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            Console.WriteLine("All patients registered to the DOTNET Hospital Management System");
            Console.WriteLine("Name                 | Email Address             | Phone       | Address");
            Console.WriteLine("------------------------------------------------------------------------------");

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                List<Patient> patients = new List<Patient>();

                // Parse each line and create a Patient object if the role is "Patient"
                foreach (string line in lines)
                {
                    if (line.StartsWith("Patient"))
                    {
                        string[] data = line.Split(',');

                        if (data.Length >= 6) // Ensure there are enough fields
                        {
                            string id = ExtractField(data[1], "ID");
                            string name = ExtractField(data[2], "Name");
                            string email = ExtractField(data[3], "Email");
                            string phone = ExtractField(data[5], "Phone");
                            string address = string.Join(",", data, 6, data.Length - 6).Trim(); // Full address

                            // Create a new Patient object and add it to the list
                            patients.Add(new Patient(id, name, email, phone, address));
                        }
                    }
                }

                // Display each patient in the list
                foreach (var patient in patients)
                {
                    Console.WriteLine(patient.ToString());
                }

                Console.WriteLine("\nPress any key to return to the Administrator menu...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Data file not found.");
                Console.ReadKey();
            }
        }

        // Method Overloading: Find patient by ID
        public Patient FindPatient(string id)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (line.StartsWith("Patient"))
                    {
                        string[] data = line.Split(',');
                        string patientId = ExtractField(data[1], "ID");

                        if (patientId == id)
                        {
                            string name = ExtractField(data[2], "Name");
                            string email = ExtractField(data[3], "Email");
                            string phone = ExtractField(data[5], "Phone");
                            string address = string.Join(",", data, 6, data.Length - 6).Trim();
                            return new Patient(patientId, name, email, phone, address);
                        }
                    }
                }
            }
            return null; // If no patient is found
        }

        // Method Overloading: Find patient by name
        public Patient FindPatientByName(string name)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (line.StartsWith("Patient"))
                    {
                        string[] data = line.Split(',');
                        string patientName = ExtractField(data[2], "Name");

                        if (patientName.ToLower() == name.ToLower())
                        {
                            string id = ExtractField(data[1], "ID");
                            string email = ExtractField(data[3], "Email");
                            string phone = ExtractField(data[5], "Phone");
                            string address = string.Join(",", data, 6, data.Length - 6).Trim();
                            return new Patient(id, patientName, email, phone, address);
                        }
                    }
                }
            }
            return null; // If no patient is found
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

    // Patient class to structure the patient information
    public class Patient
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Patient(string id, string name, string email, string phone, string address)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
        }

        // Override the ToString method to return formatted string for patient details
        public override string ToString()
        {
            return $"{Name.PadRight(20)} | {Email.PadRight(25)} | {Phone.PadRight(12)} | {Address}";
        }
    }
}
