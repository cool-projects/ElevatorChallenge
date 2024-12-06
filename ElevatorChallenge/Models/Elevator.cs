using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorChallenge.Models
{
    public class Elevator
    {
        public int Id { get; set; }
        public int CurrentFloor { get; set; }
        public ElevatorState State { get; set; }
        public int Capacity { get; set; }
        public int CurrentLoad { get; set; }

        public Elevator(int id, int capacity)
        {
            Id = id;
            Capacity = capacity;
            CurrentFloor = 1; // Start on ground floor
            State = ElevatorState.Idle;
            CurrentLoad = 0;
        }
    }
}
