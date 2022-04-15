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

        }

        public InvoiceGenerator(RideType typeOfRide)
        {
            if (typeOfRide.Equals(RideType.NORMAL))
            {
                rideChargePerKm = 10;
                pricePrMinute = 1;
                minimumFare = 5;
            }
            else if (typeOfRide.Equals(RideType.PREMIUM))
            {
                rideChargePerKm = 15;
                pricePrMinute = 2;
                minimumFare = 20;
            }
            else 
            {
                throw new CustomException(CustomException.ExceptionType.InvalidRideType, "Invalid Ride type");
            }
        }
        public int CalculateFaresForSingleRide(Ride ride)
        {
            if (ride.time < 1 && ride.distance < 5)
            {
                throw new CustomException(CustomException.ExceptionType.InvalidDistanceAndTime, "Invalid Distance and Time");
            }
            else if (ride.time < 1)
            {
                throw new CustomException(CustomException.ExceptionType.TimeSmallerThaOneMin, "Time should be greater than One Minutes");
            }
            else if (ride.distance < 5)
            {
                throw new CustomException(CustomException.ExceptionType.DistanceSmallerThanFive, "Distance should be greater than or equal to Five Km");
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
