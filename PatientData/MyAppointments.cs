using System;
using System.IO;
using System.Collections.Generic;

namespace Assignment1.PatientData
{
    public class MyAppointments
    {
        private string filePath;
        private string patientName;

        // Constructor to initialize file path and patient's name
        public MyAppointments(string patientName)
        {
            this.patientName = patientName;
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
            filePath = Path.Combine(projectDirectory, "data.txt");
        }

        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│                My Appointments            │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            // Fetch and display appointments for the logged-in patient
            List<Appointment> appointments = GetAppointmentsForPatient(patientName);
            if (appointments.Count == 0)
            {
                Console.WriteLine($"No appointments found for {patientName} Book From Menu");
            }
            else
            {
                Console.WriteLine($"Appointments for {patientName}:\n");
                Console.WriteLine($"{"Doctor",-20} | {"Patient",-20} | {"Description",-30}");
                Console.WriteLine(new string('-', 70)); // Separator line

                // Loop through and display each appointment
                foreach (var appointment in appointments)
                {
                    Console.WriteLine($"{appointment.DoctorName,-20} | {appointment.PatientName,-20} | {appointment.Description,-30}");
                }
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }

        // Method to read appointments from data.txt and filter by patient name
        private List<Appointment> GetAppointmentsForPatient(string patientName)
        {
            List<Appointment> appointments = new List<Appointment>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                // Loop through each line and extract appointment details
                foreach (string line in lines)
                {
                    if (line.StartsWith("appointment")) // Filter appointment lines
                    {
                        string[] data = line.Split(',');

                        string patient = ExtractField(data[1], "PatientName");
                        if (patient.Equals(patientName, StringComparison.OrdinalIgnoreCase))
                        {
                            string doctor = ExtractField(data[2], "DoctorName");
                            string description = ExtractField(data[3], "Description");

                            appointments.Add(new Appointment(patient, doctor, description));
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error: data.txt file not found at {filePath}");
            }

            return appointments;
        }

        // Method to extract field values from formatted text (e.g., "PatientName: John Doe")
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

    // Appointment class to hold the appointment details
    public class Appointment
    {
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string Description { get; set; }

        public Appointment(string patientName, string doctorName, string description)
        {
            PatientName = patientName;
            DoctorName = doctorName;
            Description = description;
        }
    }
}
