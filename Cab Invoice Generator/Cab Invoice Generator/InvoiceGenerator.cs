using System;
using System.Collections.Generic;
using System.Text;

namespace Cab_Invoice_Generator
{
    public class InvoiceGenerator
    {
        public int time;
        public int distance;
        readonly int rideChargePerKm;
        readonly int pricePrMinute;
        public int totalFare;
        public int averageCostOfRide;
        public int numberOfRides;
        readonly int minimumFare;
        public InvoiceGenerator()
        {
            rideChargePerKm = 10;
            pricePrMinute = 1;
            minimumFare = 5;
        }
        public int CalculateFaresForSingleRide(Ride ride)
        {
            if (ride.time < 1 && ride.distance < 5)
            {
                throw new CustomException(CustomException.Exceptions.InvalidDistanceAndTime, "Invalid Distance and Time");
            }
            else if (ride.time < 1)
            {
                throw new CustomException(CustomException.Exceptions.TimeSmallerThaOneMin, "Time should be greater than One Minutes");
            }
            else if (ride.distance < 5)
            {
                throw new CustomException(CustomException.Exceptions.DistanceSmallerThanFive, "Distance should be greater than or equal to Five Km");
            }


            int fare = ride.distance * rideChargePerKm + ride.time * pricePrMinute;
            return Math.Max(minimumFare, fare);
        }

        public int CalculateFareForMultipleRide(List<Ride> rides)
        {
            foreach (var ride in rides)
            {
                totalFare += CalculateFaresForSingleRide(ride);
                numberOfRides++;
            }
            averageCostOfRide = totalFare / numberOfRides;
            return totalFare;
        }
    }
}
