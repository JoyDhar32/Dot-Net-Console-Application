using System;

namespace Assignment1.PatientData
{
    public class MyDetails
    {
        private Patient _patient;

        public MyDetails(Patient patient)
        {
            _patient = patient;
        }

        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("┌───────────────────────────────────────────┐");
            Console.WriteLine("│          Patient Details                  │");
            Console.WriteLine("└───────────────────────────────────────────┘\n");

            Console.WriteLine($"ID: {_patient.Id}");
            Console.WriteLine($"Name: {_patient.Name}");
            Console.WriteLine($"Email: {_patient.Email}");
            Console.WriteLine($"Phone: {_patient.Phone}");
            Console.WriteLine($"Address: {_patient.Address}");

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }
    }
}
