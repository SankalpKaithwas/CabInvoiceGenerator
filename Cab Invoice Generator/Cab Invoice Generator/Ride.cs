using System;
using System.Collections.Generic;
using System.Text;

namespace Cab_Invoice_Generator
{
    public class Ride
    {
        public int distance;
        public int time;


        public Ride(int distance, int time)
        {
            this.distance = distance;
            this.time = time;
        }
    }
}
