using System;
using System.IO;
using System.Collections.Generic;

namespace Assignment1.DoctorData
{
    public class AllAppointments
    {
        private Doctor _loggedInDoctor; // Reference to the currently logged-in doctor

        public AllAppointments(Doctor loggedInDoctor)
        {
            _loggedInDoctor = loggedInDoctor; // Set the logged-in doctor
        }

        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│              List Appointments            │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            Console.WriteLine($"Appointments for Dr. {_loggedInDoctor.Name}:\n");

            // Headers for displaying appointment data
            Console.WriteLine("Doctor                | Patient               | Description");
            Console.WriteLine("───────────────────────────────────────────────────────────────────────────────");

            // Read and process the data.txt file
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
            string filePath = Path.Combine(projectDirectory, "data.txt");

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                List<Appointment> appointments = new List<Appointment>();

                // Loop through lines and find matching appointments for the logged-in doctor
                foreach (string line in lines)
                {
                    if (line.StartsWith("appointment"))
                    {
                        string[] data = line.Split(',');

                        string doctorName = data[2].Split(':')[1].Trim(); // Extract doctor name
                        if (doctorName == _loggedInDoctor.Name) // Check if the doctor matches the current logged-in doctor
                        {
                            string patientName = data[1].Split(':')[1].Trim(); // Extract patient name
                            string description = data[3].Split(':')[1].Trim(); // Extract appointment description

                            // Add the appointment to the list
                            appointments.Add(new Appointment(doctorName, patientName, description));
                        }
                    }
                }

                // Display all appointments for the logged-in doctor
                foreach (var appointment in appointments)
                {
                    Console.WriteLine(appointment.ToString());
                }

                if (appointments.Count == 0)
                {
                    Console.WriteLine("No appointments found for this doctor.");
                }
            }
            else
            {
                Console.WriteLine("Data file not found.");
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }
    }

    // Appointment class to store appointment details
    public class Appointment
    {
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string Description { get; set; }

        public Appointment(string doctorName, string patientName, string description)
        {
            DoctorName = doctorName;
            PatientName = patientName;
            Description = description;
        }

        // Override ToString() to format appointment details
        public override string ToString()
        {
            return $"{DoctorName.PadRight(20)} | {PatientName.PadRight(20)} | {Description}";
        }
    }
}
