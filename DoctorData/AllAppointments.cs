using System;
using System.IO;
using System.Collections.Generic;

namespace Assignment1.DoctorData
{
    public class AllAppointments
    {
        private string filePath;
        private string doctorId;

        // Constructor to initialize file path and doctor's ID
        public AllAppointments(string doctorId)
        {
            this.doctorId = doctorId;
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
            filePath = Path.Combine(projectDirectory, "data.txt");
        }

        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│                All Appointments           │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            // Fetch and display appointments for the logged-in doctor
            List<DoctorAppointment> appointments = GetAppointmentsForDoctor(doctorId);
            if (appointments.Count == 0)
            {
                Console.WriteLine($"No appointments found for doctor with ID: {doctorId}.");
            }
            else
            {
                Console.WriteLine($"Appointments:\n");
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

        // Method to read appointments from data.txt and filter by doctor ID
        private List<DoctorAppointment> GetAppointmentsForDoctor(string doctorId)
        {
            List<DoctorAppointment> appointments = new List<DoctorAppointment>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                // Loop through each line and extract appointment details
                foreach (string line in lines)
                {
                    if (line.StartsWith("appointment")) // Filter appointment lines
                    {
                        string[] data = line.Split(',');

                        string docId = ExtractField(data[3], "DoctorId"); // Extract DoctorId
                        if (docId.Equals(doctorId, StringComparison.OrdinalIgnoreCase))
                        {
                            // Appointment found for the doctor, add to the list
                            string doctorName = ExtractField(data[4], "DoctorName");
                            string patientName = ExtractField(data[2], "PatientName");
                            string description = ExtractField(data[5], "Description");

                            appointments.Add(new DoctorAppointment(doctorName, patientName, description));
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

        // Method to extract field values from formatted text (e.g., "DoctorId: 28250")
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

    // Class to represent the appointments for a doctor
    public class DoctorAppointment
    {
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string Description { get; set; }

        public DoctorAppointment(string doctorName, string patientName, string description)
        {
            DoctorName = doctorName;
            PatientName = patientName;
            Description = description;
        }
    }
}
