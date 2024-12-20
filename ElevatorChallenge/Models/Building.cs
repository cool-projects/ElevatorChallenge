﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorChallenge.Models
{
    public class Building
    {
        public int Floors { get; set; }
        public List<Elevator> Elevators { get; set; }

        public Building(int floors, List<Elevator> elevators)
        {
            Floors = floors;
            Elevators = elevators;
        }
    }
}
