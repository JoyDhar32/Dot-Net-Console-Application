using System;
using System.IO;
using System.Collections.Generic;

namespace Assignment1.DoctorData
{
    public class MyPatients
    {
        private Doctor _loggedInDoctor; // Reference to the currently logged-in doctor

        public MyPatients(Doctor loggedInDoctor)
        {
            _loggedInDoctor = loggedInDoctor; // Set the logged-in doctor
        }

        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│              List Patients                │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            Console.WriteLine($"Patients assigned to Dr. {_loggedInDoctor.Name}:\n");

            // Headers
            Console.WriteLine("Patient              | Doctor                | Email Address         | Phone       | Address");
            Console.WriteLine("───────────────────────────────────────────────────────────────────────────────────────────");

            // Read and process the data.txt file
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
            string filePath = Path.Combine(projectDirectory, "data.txt");

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                List<Patient> patients = new List<Patient>();

                foreach (string line in lines)
                {
                    if (line.StartsWith("appointment"))
                    {
                        string[] data = line.Split(',');

                        string doctorName = data[2].Split(':')[1].Trim(); // Extract doctor name
                        if (doctorName == _loggedInDoctor.Name) // Check if the doctor matches the current logged-in doctor
                        {
                            string patientName = data[1].Split(':')[1].Trim(); // Extract patient name

                            // Find patient details (name, email, phone, address)
                            Patient patient = GetPatientDetails(patientName, doctorName, lines);
                            if (patient != null)
                            {
                                patients.Add(patient);
                            }
                        }
                    }
                }

                // Display patient information
                foreach (var patient in patients)
                {
                    Console.WriteLine(patient.ToString());
                }

                if (patients.Count == 0)
                {
                    Console.WriteLine("No patients found for this doctor.");
                }
            }
            else
            {
                Console.WriteLine("Data file not found.");
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }

        // Helper method to find patient details in the data.txt file
        private Patient GetPatientDetails(string patientName, string doctorName, string[] lines)
        {
            foreach (string line in lines)
            {
                if (line.StartsWith("Patient") && line.Contains(patientName))
                {
                    string[] data = line.Split(',');
                    string name = data[2].Split(':')[1].Trim();
                    string email = data[3].Split(':')[1].Trim();
                    string phone = data[5].Split(':')[1].Trim();
                    string address = string.Join(",", data, 6, data.Length - 6).Trim();

                    return new Patient(name, doctorName, email, phone, address);
                }
            }

            return null;
        }
    }

    // Patient class to store patient details
    public class Patient
    {
        public string Name { get; set; }
        public string DoctorName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Patient(string name, string doctorName, string email, string phone, string address)
        {
            Name = name;
            DoctorName = doctorName;
            Email = email;
            Phone = phone;
            Address = address;
        }

        // Override ToString() to format patient details
        public override string ToString()
        {
            return $"{Name.PadRight(20)} | {DoctorName.PadRight(20)} | {Email.PadRight(20)} | {Phone.PadRight(12)} | {Address}";
        }
    }
}
