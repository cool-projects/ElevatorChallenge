using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorChallenge.Services
{
    public interface IBuildingService
    {
        void CallElevator(int floor, int passengers);
        void ShowElevatorStatus();
    }
}
