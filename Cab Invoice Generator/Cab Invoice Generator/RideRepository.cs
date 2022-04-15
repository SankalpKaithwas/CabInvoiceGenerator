using System;
using System.Collections.Generic;
using System.Text;

namespace Cab_Invoice_Generator
{
    public class RideRepository
    {
        public Dictionary<string, List<Ride>> rideRepository;
        public RideRepository()
        {
            rideRepository = new Dictionary<string, List<Ride>>();
        }
        public void AddRide(string userId, Ride ride)
        {
            if (rideRepository.ContainsKey(userId))
                rideRepository[userId].Add(ride);
            else
            {
                rideRepository.Add(userId, new List<Ride>());
                rideRepository[userId].Add(ride);
            }
        }
        public List<Ride> GetListOfRides(string userID)
        {
            if (rideRepository.ContainsKey(userID))
                return rideRepository[userID];
            else throw new CustomException(CustomException.Exceptions.InvalidUserId, "Invalid User ID");
        }
    }
}
