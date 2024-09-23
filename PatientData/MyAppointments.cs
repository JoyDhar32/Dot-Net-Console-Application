using System;
using System.IO;
using System.Collections.Generic;

namespace Assignment1.PatientData
{
    public class MyAppointments
    {
        private string filePath;
        private string patientId;

        // Constructor to initialize file path and patient's ID
        public MyAppointments(string patientId)
        {
            this.patientId = patientId;
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
            filePath = Path.Combine(projectDirectory, "data.txt");
        }

        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│              My Appointments              │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            // Fetch and display appointments for the logged-in patient
            List<AppointmentDetails> appointments = GetAppointmentsForPatient(patientId);
            if (appointments.Count == 0)
            {
                Console.WriteLine($"No appointment found for patient ID: {patientId}.");
            }
            else
            {
                Console.WriteLine($"Appointments for Patient ID {patientId}:\n");
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

        // Method to read appointments from data.txt and filter by patient ID
        private List<AppointmentDetails> GetAppointmentsForPatient(string patientId)
        {
            List<AppointmentDetails> appointments = new List<AppointmentDetails>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                // Loop through each line and extract appointment details
                foreach (string line in lines)
                {
                    if (line.StartsWith("appointment")) // Filter appointment lines
                    {
                        string[] data = line.Split(',');

                        string patient = ExtractField(data[1], "PatientId"); // Extract PatientId
                        if (patient.Equals(patientId, StringComparison.OrdinalIgnoreCase))
                        {
                            // Appointment found for the patient, add to the list
                            string patientName = ExtractField(data[2], "PatientName");
                            string doctorName = ExtractField(data[4], "DoctorName");
                            string description = ExtractField(data[5], "Description");

                            appointments.Add(new AppointmentDetails(doctorName, patientName, description));
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

        // Method to extract field values from formatted text (e.g., "PatientId: 12345")
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

    // Class to hold appointment details
    public class AppointmentDetails
    {
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string Description { get; set; }

        public AppointmentDetails(string doctorName, string patientName, string description)
        {
            DoctorName = doctorName;
            PatientName = patientName;
            Description = description;
        }
    }
}
