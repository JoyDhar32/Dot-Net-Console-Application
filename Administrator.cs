using System;

namespace Assignment1
{
    public class Administrator : User
    {
        public Administrator(string id, string password) : base(id, password)
        {
        }

        public override void Greet()
        {
            Console.WriteLine("\nWelcome to DOTNET Hospital Management System");
            ShowMenu();
        }

        // Method to display the Administrator menu
        public void ShowMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("┌───────────────────────────────────────────┐");
                Console.WriteLine("│      DOTNET Hospital Management System     │");
                Console.WriteLine("├───────────────────────────────────────────┤");
                Console.WriteLine("│             Administrator Menu            │");
                Console.WriteLine("└───────────────────────────────────────────┘\n");

                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. List all doctors");
                Console.WriteLine("2. Check doctor details");
                Console.WriteLine("3. List all patients");
                Console.WriteLine("4. Check patient details");
                Console.WriteLine("5. Add doctor");
                Console.WriteLine("6. Add patient");
                Console.WriteLine("7. Logout");
                Console.WriteLine("8. Exit");
                Console.Write("\nEnter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {

                    case "1":
                        var listDoctors = new AdministratorData.ListAllDoctors();
                        listDoctors.Execute();  // Calls the ListAllDoctors class to display all doctors
                        break;




                    case "2":
                        var checkDoctor = new AdministratorData.CheckDoctorDetails();
                        checkDoctor.Execute();
                        break;
                    case "3":
                        var listPatients = new AdministratorData.AllPatients();
                        listPatients.Execute();
                        break;
                    case "4":
                        var checkPatientDetails = new AdministratorData.CheckPatientDetails();
                        checkPatientDetails.Execute();
                        break;
                    case "5":
                        var addDoctor = new AddDoctor();
                        addDoctor.Execute();
                        break;
                    case "6":
                        var addPatient = new AddPatient();
                        addPatient.Execute();
                        break;
                    case "7":
                        exit = true;
                        Console.WriteLine("Logging out...");
                        Program.StartLoginProcess(); // Redirect to the login process
                        break;
                    case "8":
                        exit = true;
                        Console.WriteLine("Exiting...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a number between 1 and 8.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
