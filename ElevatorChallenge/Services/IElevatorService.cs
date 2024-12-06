using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Services
{
    public interface IElevatorService
    {
        int CurrentFloor { get; }
        bool IsIdle { get; }
        int DistanceToFloor(int floor);
        bool CanStopAt(int floor);
        void AddFloorRequest(int floor);
        Task ProcessRequestsAsync();
        bool LoadPassengers(int passengers);
        void ShowStatus();
    }
}
