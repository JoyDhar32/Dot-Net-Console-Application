using System;
using System.IO;
using System.Linq;

namespace Assignment1.DoctorData
{
    public class ListAppointmentWithPatient
    {
        private Doctor _loggedInDoctor;

        // Constructor to pass in the logged-in doctor
        public ListAppointmentWithPatient(Doctor doctor)
        {
            _loggedInDoctor = doctor;
        }

        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│        Appointments With Patient           │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            // Prompt the user to enter the patient ID
            Console.Write("Enter the ID of the patient you would like to view appointments for: ");
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
            bool appointmentsFound = false;

            // Search for the appointments based on the entered patient ID and logged-in doctor
            foreach (string line in lines)
            {
                if (line.StartsWith("appointment"))
                {
                    string[] data = line.Split(',');

                    string patientId = ExtractField(data[1], "PatientId");
                    string patientName = ExtractField(data[2], "PatientName");
                    string doctorId = ExtractField(data[3], "DoctorId");
                    string doctorName = ExtractField(data[4], "DoctorName");
                    string description = ExtractField(data[5], "Description");

                    // If the patient ID and doctor ID match, display the appointment details
                    if (patientId == patientIdInput && doctorId == _loggedInDoctor.Id)
                    {
                        if (!appointmentsFound)
                        {
                            Console.WriteLine("\nAppointments for the patient:");
                            Console.WriteLine("────────────────────────────────────────────────────────────────────");
                            Console.WriteLine($"Doctor              | Patient             | Description");
                            Console.WriteLine("────────────────────────────────────────────────────────────────────");
                        }

                        Console.WriteLine($"{doctorName.PadRight(18)} | {patientName.PadRight(18)} | {description}");
                        appointmentsFound = true;
                    }
                }
            }

            // If no appointments were found, display an error message
            if (!appointmentsFound)
            {
                Console.WriteLine("\nNo appointments found for the entered patient ID.");
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
