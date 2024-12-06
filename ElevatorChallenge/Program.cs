using ElevatorChallenge.Models;
using ElevatorChallenge.Services;
using System;
using System.Linq;

namespace ElevatorChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("The Leonardo Building - Elevator System");
                Console.Write("Enter the number of floors in the building: ");
                int floors = int.Parse(Console.ReadLine());

                Console.Write("Enter the number of elevators: ");
                int numberOfElevators = int.Parse(Console.ReadLine());

                Console.Write("Enter the capacity of each elevator: ");
                int capacity = int.Parse(Console.ReadLine());

                // Initialize building and services
                var elevators = Enumerable.Range(1, numberOfElevators)
                                          .Select(id => new Elevator(id, capacity))
                                          .ToList();

                var building = new Building(floors, elevators);
                IBuildingService buildingService = new BuildingService(building);

                while (true)
                {
                    Console.WriteLine("\n1. Call Elevator");
                    Console.WriteLine("2. Show Elevator Status");
                    Console.WriteLine("3. Exit");
                    Console.Write("Choose an option: ");
                    int option = int.Parse(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            Console.Write("Enter the floor to call an elevator: ");
                            int floor = int.Parse(Console.ReadLine());
                            Console.Write("Enter the number of passengers waiting: ");
                            int passengers = int.Parse(Console.ReadLine());
                            buildingService.CallElevator(floor, passengers);
                            break;

                        case 2:
                            buildingService.ShowElevatorStatus();
                            break;

                        case 3:
                            Console.WriteLine("Exiting the Elevator Simulator. Goodbye!");
                            return;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
