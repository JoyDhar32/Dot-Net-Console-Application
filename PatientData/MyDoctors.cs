using System;
using System.IO;
using System.Collections.Generic;

namespace Assignment1.PatientData
{
    public class MyDoctors
    {
        private string filePath;
        private string patientId;

        // Constructor to initialize file path and patient's ID
        public MyDoctors(string patientId)
        {
            this.patientId = patientId;
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
            filePath = Path.Combine(projectDirectory, "data.txt");
        }

        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│                My Doctor                  │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            // Fetch appointments for the logged-in patient
            List<PatientAppointment> appointments = GetAppointmentsForPatient(patientId);
            if (appointments.Count == 0)
            {
                Console.WriteLine($"No doctor found for patient ID: {patientId}. Please book an appointment.");
            }
            else
            {
                Console.WriteLine($"Your doctor:\n");
                Console.WriteLine($"{"Name",-20} | {"Email Address",-25} | {"Phone",-12} | {"Address",-30}");
                Console.WriteLine(new string('-', 100)); // Separator line

                foreach (var appointment in appointments)
                {
                    // For each appointment, find the doctor's details and display them
                    AssignedDoctor doctor = GetDoctorById(appointment.DoctorId);
                    if (doctor != null)
                    {
                        Console.WriteLine($"{doctor.Name,-20} | {doctor.Email,-25} | {doctor.Phone,-12} | {doctor.Address,-30}");
                    }
                }
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }

        // Method to read appointments from data.txt and filter by patient ID
        private List<PatientAppointment> GetAppointmentsForPatient(string patientId)
        {
            List<PatientAppointment> appointments = new List<PatientAppointment>();

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
                            string doctorId = ExtractField(data[3], "DoctorId");
                            appointments.Add(new PatientAppointment(patient, doctorId));
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

        // Method to find the doctor by ID from data.txt
        private AssignedDoctor GetDoctorById(string doctorId)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                // Loop through each line and extract doctor details
                foreach (string line in lines)
                {
                    if (line.StartsWith("Doctor"))
                    {
                        string[] data = line.Split(',');

                        string id = ExtractField(data[1], "ID");
                        if (id == doctorId)
                        {
                            string name = ExtractField(data[2], "Name");
                            string email = ExtractField(data[3], "Email");
                            string phone = ExtractField(data[5], "Phone");
                            string address = ExtractField(data[6], "Address");

                            return new AssignedDoctor(name, email, phone, address);
                        }
                    }
                }
            }

            return null;
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

    // Renamed Appointment class to avoid conflict
    public class PatientAppointment
    {
        public string PatientId { get; set; }
        public string DoctorId { get; set; }

        public PatientAppointment(string patientId, string doctorId)
        {
            PatientId = patientId;
            DoctorId = doctorId;
        }
    }

    // Doctor class renamed to AssignedDoctor
    public class AssignedDoctor
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public AssignedDoctor(string name, string email, string phone, string address)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
        }
    }
}
