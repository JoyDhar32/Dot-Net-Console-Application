using System;

namespace Assignment1
{
    public class Doctor : User
    {
        // Define properties for the Doctor class
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        // Constructor to initialize doctor details and call the base User constructor
        public Doctor(string id, string password, string name, string email, string phone, string address)
            : base(id, password)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
        }

        // Override the ToString() method to return formatted string for doctor details
        public override string ToString()
        {
            return $"{Name.PadRight(20)} | {Email.PadRight(25)} | {Phone.PadRight(12)} | {Address}";
        }

        // Override the Greet() method to provide a doctor-specific greeting
        public override void Greet()
        {
            Console.WriteLine($"Welcome, Dr. {Name}!");
            ShowMenu(); 
        }

        // Show the doctor menu
        public void ShowMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("┌───────────────────────────────────────────┐");
                Console.WriteLine("│      DOTNET Hospital Management System     │");
                Console.WriteLine("├───────────────────────────────────────────┤");
                Console.WriteLine("│                 Doctor Menu               │");
                Console.WriteLine("└───────────────────────────────────────────┘\n");
                Console.WriteLine($"Welcome to DOTNET Hospital Management System Dr. {Name}\n");

                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. List doctor details");
                Console.WriteLine("2. List patients");
                Console.WriteLine("3. List appointments");
                Console.WriteLine("4. Check particular patient");
                Console.WriteLine("5. List appointments with patient");
                Console.WriteLine("6. Logout");
                Console.WriteLine("7. Exit");
                Console.Write("\nEnter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        new DoctorData.MyDetails(this).Execute(); // Pass the current doctor object to MyDetails
                        break;
                    case "2":
                        new DoctorData.MyPatients(this).Execute();  // List patients
                        break;
                    case "3":
                        new DoctorData.AllAppointments(this).Execute(); // List all appointments for the logged-in doctor
                        break;
                    case "4":
                        new DoctorData.ParticularPatient().Execute(); // Check particular patient
                        break;
                    case "5":
                        new DoctorData.ListAppointmentWithPatient(this).Execute(); // List appointments with patient
                        break;
                    case "6":
                        exit = true;
                        Program.StartLoginProcess(); // Exit to login
                        break;
                    case "7":
                        exit = true;
                        Console.WriteLine("Exiting the system...");
                        Environment.Exit(0); // Exit the application
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a number between 1 and 7.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
