using System;
using System.IO;
using System.Collections.Generic;

namespace Assignment1
{
    public class CheckDoctorDetails
    {
        public void Execute()
        {
            while (true)
            {
                Console.Clear(); // Clear the console to display fresh information

                // Display the menu header
                Console.WriteLine("┌───────────────────────────────────────────┐");
                Console.WriteLine("│      DOTNET Hospital Management System    │");
                Console.WriteLine("├-------------------------------------------|");
                Console.WriteLine("│                Doctor Details             │");
                Console.WriteLine("└───────────────────────────────────────────┘\n");

                Console.WriteLine("Please enter the ID of the doctor whose details you are checking.");
                Console.WriteLine("Or press 'n' to return to menu.");
                Console.Write("Enter ID: ");
                string inputId = Console.ReadLine();

                if (inputId.ToLower() == "n")
                {
                    return; // Go back to the previous menu
                }

                // Read the doctor details from the file
                string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
                string filePath = Path.Combine(projectDirectory, "data.txt");

                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    bool doctorFound = false;

                    // Parse each line and check if the ID matches a doctor
                    foreach (string line in lines)
                    {
                        if (line.StartsWith("Doctor"))
                        {
                            string[] data = line.Split(',');
                            string id = data[1].Split(':')[1].Trim();

                            if (id == inputId)
                            {
                                string name = data[2].Split(':')[1].Trim();
                                string email = data[3].Split(':')[1].Trim();
                                string phone = data[5].Split(':')[1].Trim();
                                string address = string.Join(",", data, 6, data.Length - 6).Split(':')[1].Trim(); 
   //This will join all parts of the array after the 6th element (which contains the address) into a single string, ensuring that the full address is captured even if it contains commas.

                                // Display the doctor's details in the same console
                                Console.WriteLine($"\nDetails for {name}");
                                //Console.WriteLine("─────────────────────────────────────────────────────────────────────────────");
                                Console.WriteLine("Name                 | Email Address             | Phone       | Address");
                                Console.WriteLine("----------------------------------------------------------------------------------------------------");
                                Console.WriteLine($"{name,-20} | {email,-25} | {phone,-12} | {address}");

                                doctorFound = true;
                                break;
                            }
                        }
                    }

                    if (!doctorFound)
                    {
                        // If the doctor with the provided ID is not found, show an error message
                        Console.WriteLine("\nNo doctor found with that ID. Please try again.");
                    }

                    // Ask the user to press any key to continue or 'n' to return to the menu
                    Console.WriteLine("\nPress any key to check another doctor or 'n' to return to the menu.");
                    string choice = Console.ReadLine();

                    if (choice.ToLower() == "n")
                    {
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Data file not found.");
                    Console.ReadKey();
                    return;
                }
            }
        }
    }
}
