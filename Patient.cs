using System;

namespace Assignment1
{
    public class Patient : User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        // Constructor to initialize patient details and call the base User constructor
        public Patient(string id, string password, string name, string email, string phone, string address)
            : base(id, password) // Pass id and password to the base User class constructor
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
        }

        // Override the Greet method to provide a patient-specific greeting
        public override void Greet()
        {
            Console.WriteLine($"Hello {Name}, welcome to the Patient Dashboard!");
            ShowPatientMenu();
        }

        // Method to display the Patient Menu and connect it to the corresponding classes
        public void ShowPatientMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("┌───────────────────────────────────────────┐");
                Console.WriteLine("│      DOTNET Hospital Management System     │");
                Console.WriteLine("├───────────────────────────────────────────┤");
                Console.WriteLine("│                 Patient Menu              │");
                Console.WriteLine("└───────────────────────────────────────────┘\n");
                Console.WriteLine($"Welcome to DOTNET Hospital Management System {Name}\n");

                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. List patient details");
                Console.WriteLine("2. List my doctor details");
                Console.WriteLine("3. List all appointments");
                Console.WriteLine("4. Book appointment");
                Console.WriteLine("5. Exit to login");
                Console.WriteLine("6. Exit System");
                Console.Write("\nEnter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Calls the MyDetails class to list the patient's details
                        new PatientData.MyDetails(this).Execute();
                        break;
                    case "2":
                        // Calls the MyDoctors class to list the patient's doctor details
                        new PatientData.MyDoctors(this.Id).Execute();
                        break;
                    case "3":
                        // Calls the MyAppointments class to list all patient appointments
                        new PatientData.MyAppointments(this.Id).Execute();
                        break;
                    case "4":
                        new PatientData.BookAppointment(this.Id, this.Name).Execute(); // Book an appointment
                        break;
                    case "5":
                        exit = true;
                        Program.StartLoginProcess(); // Exit to login
                        break;
                    case "6":
                        exit = true;
                        Console.WriteLine("Exiting the system...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a number between 1 and 6.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
