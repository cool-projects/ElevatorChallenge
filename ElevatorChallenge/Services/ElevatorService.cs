using ElevatorChallenge.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Services
{
    public class ElevatorService : IElevatorService
    {
        private readonly Elevator _elevator;
        private readonly Queue<int> _requests;

        public ElevatorService(Elevator elevator)
        {
            _elevator = elevator;
            _requests = new Queue<int>();
        }

        public int CurrentFloor => _elevator.CurrentFloor;

        public bool IsIdle => _elevator.State == ElevatorState.Idle;

        public int DistanceToFloor(int floor)
        {
            return Math.Abs(_elevator.CurrentFloor - floor);
        }

        public bool CanStopAt(int floor)
        {
            if (IsIdle) return true;
            if (_elevator.State == ElevatorState.MovingUp && floor > _elevator.CurrentFloor) return true;
            if (_elevator.State == ElevatorState.MovingDown && floor < _elevator.CurrentFloor) return true;
            return false;
        }

        public void AddFloorRequest(int floor)
        {
            if (!_requests.Contains(floor))
            {
                _requests.Enqueue(floor);
            }
        }

        public async Task ProcessRequestsAsync()
        {
            while (_requests.Count > 0)
            {
                int nextFloor = _requests.Dequeue();
                await MoveToFloorAsync(nextFloor);
                UnloadPassengers();
            }
        }

        private async Task MoveToFloorAsync(int targetFloor)
        {
            _elevator.State = targetFloor > _elevator.CurrentFloor ? ElevatorState.MovingUp : ElevatorState.MovingDown;
            Console.WriteLine($"Elevator {_elevator.Id} is moving {_elevator.State} to floor {targetFloor}.");

            // Simulate delay for movement
            await Task.Delay(1000 * Math.Abs(targetFloor - _elevator.CurrentFloor));

            _elevator.CurrentFloor = targetFloor;
            _elevator.State = ElevatorState.Idle;
            Console.WriteLine($"Elevator {_elevator.Id} has arrived at floor {_elevator.CurrentFloor}.");
        }

        public bool LoadPassengers(int passengers)
        {
            if (_elevator.CurrentLoad + passengers > _elevator.Capacity)
            {
                Console.WriteLine($"Elevator {_elevator.Id} cannot take {passengers} passengers due to weight limit.");
                return false;
            }

            _elevator.CurrentLoad += passengers;
            Console.WriteLine($"Elevator {_elevator.Id} now has {_elevator.CurrentLoad}/{_elevator.Capacity} passengers.");
            return true;
        }

        private void UnloadPassengers()
        {
            Console.WriteLine($"Elevator {_elevator.Id} has unloaded {_elevator.CurrentLoad} passengers.");
            _elevator.CurrentLoad = 0;
        }

        public void ShowStatus()
        {
            Console.WriteLine($"Elevator {_elevator.Id} - Floor: {_elevator.CurrentFloor}, State: {_elevator.State}, Load: {_elevator.CurrentLoad}/{_elevator.Capacity}");
        }
    }
}
