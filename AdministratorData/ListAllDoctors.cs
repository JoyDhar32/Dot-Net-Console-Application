using System;
using System.IO;
using System.Collections.Generic;

namespace Assignment1.AdministratorData
{
    public class ListAllDoctors
    {
        public void Execute()
        {
            Console.Clear(); // Clear the console

            // Display the menu header
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│      DOTNET Hospital Management System    │");
            Console.WriteLine("├-------------------------------------------┤");
            Console.WriteLine("│                 All Doctors               │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            Console.WriteLine("All doctors registered to the DOTNET Hospital Management System");
            Console.WriteLine("Name                 | Email Address             | Phone       | Address");
            Console.WriteLine("-------------------------------------------------------------------------------------");

            // Get the file path
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
            string filePath = Path.Combine(projectDirectory, "data.txt");

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                List<Doctor> doctors = new List<Doctor>();

                // Parse each line and create a Doctor object if the role is "Doctor"
                foreach (string line in lines)
                {
                    if (line.StartsWith("Doctor"))
                    {
                        // Use a different parsing strategy that handles the commas in the address
                        try
                        {
                            string[] parts = line.Split(new[] { "Doctor, ID:", "Name:", "Email:", "Password:", "Phone:", "Address:" }, StringSplitOptions.None);

                            if (parts.Length == 7)
                            {
                                string id = parts[1].Trim();
                                string name = parts[2].Trim();
                                string email = parts[3].Trim();
                                string phone = parts[5].Trim();
                                string address = parts[6].Trim();  // Capture the full address

                                // Create a new Doctor object and add it to the list
                                doctors.Add(new Doctor(id, "", name, email, phone, address));
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error parsing doctor data: {ex.Message}");
                        }
                    }
                }

                // Display each doctor in the list
                foreach (var doctor in doctors)
                {
                    Console.WriteLine(doctor.ToString());
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

    // Doctor class to structure the doctor information
    public class Doctor : User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Doctor(string id, string password, string name, string email, string phone, string address)
            : base(id, password)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
        }

        // Override the ToString method to return formatted string for doctor details
        public override string ToString()
        {
            return $"{Name.PadRight(20)} | {Email.PadRight(25)} | {Phone.PadRight(12)} | {Address}";
        }

        public override void Greet()
        {
            Console.WriteLine($"Welcome, Dr. {Name}!");
        }
    }
}
