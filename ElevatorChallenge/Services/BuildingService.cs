using ElevatorChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly Building _building;
        private readonly List<IElevatorService> _elevatorServices;

        public BuildingService(Building building)
        {
            _building = building;

            // Initialize ElevatorService for each elevator
            _elevatorServices = _building.Elevators
                .Select(elevator => new ElevatorService(elevator))
                .ToList<IElevatorService>();
        }

        public void CallElevator(int floor, int passengers)
        {
            if (floor < 1 || floor > _building.Floors)
            {
                Console.WriteLine($"Invalid floor: {floor}. Please choose a floor between 1 and {_building.Floors}.");
                return;
            }

            var nearestElevatorService = _elevatorServices
                .Where(e => e.IsIdle || e.CanStopAt(floor))
                .OrderBy(e => e.DistanceToFloor(floor))
                .ThenBy(e => e.CurrentFloor)
                .FirstOrDefault();

            if (nearestElevatorService == null)
            {
                Console.WriteLine($"No available elevators to respond to floor {floor}.");
                return;
            }

            nearestElevatorService.AddFloorRequest(floor);
            if (nearestElevatorService.LoadPassengers(passengers))
            {
                Task.Run(() => nearestElevatorService.ProcessRequestsAsync());
            }
        }

        public void ShowElevatorStatus()
        {
            Console.WriteLine("\nElevator Status:");
            foreach (var elevatorService in _elevatorServices)
            {
                elevatorService.ShowStatus();
            }
        }
    }
}
