using System;
using System.IO;
using System.Collections.Generic;

namespace Assignment1
{
    public class ListAllDoctors
    {
        public void Execute()
        {
            Console.Clear(); // Clear the console

            // Display the menu header
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│      DOTNET Hospital Management System     │");
            Console.WriteLine("├───────────────────────────────────────────┤");
            Console.WriteLine("│                 All Doctors               │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            Console.WriteLine("All doctors registered to the DOTNET Hospital Management System");
            Console.WriteLine("Name                 | Email Address             | Phone       | Address");
            Console.WriteLine("─────────────────────────────────────────────────────────────────────────────");

            // Read the doctor details from the file
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
                        string[] data = line.Split(',');
                        string id = data[1].Split(':')[1].Trim();
                        string name = data[2].Split(':')[1].Trim();
                        string email = data[3].Split(':')[1].Trim();
                        string phone = data[5].Split(':')[1].Trim();
                        string address = string.Join(",", data, 6, data.Length - 6).Trim();

                        // Create a new Doctor object and add it to the list
                        doctors.Add(new Doctor(id, "", name, email, phone, address));
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
    }
}
