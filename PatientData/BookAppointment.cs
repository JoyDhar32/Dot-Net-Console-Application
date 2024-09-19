using System;
using System.IO;
using System.Collections.Generic;

namespace Assignment1.PatientData
{
    public class BookAppointment
    {
        private string filePath; // Class-level variable for the file path
        private string patientId; // Store the patient's ID
        private string patientName; // Store the patient's name

        // Constructor with patient's ID and name as parameters
        public BookAppointment(string patientId, string patientName)
        {
            this.patientId = patientId; // Store patient ID
            this.patientName = patientName; // Store patient name

            // Initialize the file path
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
            filePath = Path.Combine(projectDirectory, "data.txt");
        }

        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│              Book Appointment              │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            // Check if patient is registered with a doctor
            string doctorId = ""; // This should come from the patient data

            // Get list of doctors from data.txt
            List<Doctor> doctors = GetDoctorList();
            if (doctors.Count == 0)
            {
                Console.WriteLine("No doctors found. Please add doctors first.");
                return;
            }

            // Display the list of doctors
            Console.WriteLine("You are not registered with any doctor! Please choose which doctor you would like to register with.\n");
            for (int i = 0; i < doctors.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {doctors[i].Name} | {doctors[i].Email} | {doctors[i].Phone} | {doctors[i].Address}");
            }

            // Safely handle input to avoid crashes if user provides incorrect input
            try
            {
                Console.Write("\nPlease choose a doctor: ");
                int selectedDoctorIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                if (selectedDoctorIndex >= 0 && selectedDoctorIndex < doctors.Count)
                {
                    doctorId = doctors[selectedDoctorIndex].Id;
                    string doctorName = doctors[selectedDoctorIndex].Name;

                    Console.WriteLine($"\nYou are booking a new appointment with {doctorName}");

                    // Collect appointment details
                    Console.Write("Enter description of the appointment: ");
                    string appointmentDescription = Console.ReadLine();

                    // Store the appointment in the data.txt file
                    StoreAppointment(patientId, patientName, doctorId, doctorName, appointmentDescription);

                    Console.WriteLine("\nThe appointment has been booked successfully!");
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Invalid choice. Returning to menu...");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Returning to menu...");
            }
        }

        // Method to read doctor list from data.txt
        private List<Doctor> GetDoctorList()
        {
            List<Doctor> doctors = new List<Doctor>();

            // Read all lines from the data.txt file
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                // Loop through each line and extract doctor information
                foreach (string line in lines)
                {
                    if (line.StartsWith("Doctor")) // Filter out doctor lines
                    {
                        // Split the line into components and extract doctor details
                        string[] data = line.Split(',');
                        string id = ExtractField(data[1], "ID");
                        string name = ExtractField(data[2], "Name");
                        string email = ExtractField(data[3], "Email");
                        string phone = ExtractField(data[5], "Phone");
                        string address = string.Join(",", data, 6, data.Length - 6).Trim(); // Full address

                        // Create a new doctor object and add it to the list
                        doctors.Add(new Doctor(id, name, email, phone, address));
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error: data.txt file not found at {filePath}");
            }

            return doctors;
        }

        // Method to extract field values from formatted text (e.g., "ID: 1")
        private string ExtractField(string data, string fieldName)
        {
            var parts = data.Split(':');
            if (parts.Length > 1)
            {
                return parts[1].Trim();
            }
            return string.Empty;
        }

        // Method to store the appointment in data.txt
        private void StoreAppointment(string patientId, string patientName, string doctorId, string doctorName, string description)
        {
            try
            {
                // Format the appointment data
                string appointmentData = $"appointment, PatientId: {patientId}, PatientName: {patientName}, DoctorId: {doctorId}, DoctorName: {doctorName}, Description: {description}, Date: {DateTime.Now}";

                // Append the appointment to the file
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(appointmentData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to data.txt: {ex.Message}");
            }
        }
    }

    // Simulating the Doctor class (you likely already have this in your project)
    public class Doctor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Doctor(string id, string name, string email, string phone, string address)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
        }
    }
}
