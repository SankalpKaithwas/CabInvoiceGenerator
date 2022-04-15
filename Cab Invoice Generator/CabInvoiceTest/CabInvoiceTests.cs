using Cab_Invoice_Generator;
using NUnit.Framework;
using System.Collections.Generic;

namespace CabInvoiceTest
{
    public class Tests
    {
        InvoiceGenerator fare;
        RideRepository rideRepository;
        [SetUp]
        public void Setup()
        {
            fare = new InvoiceGenerator();
            rideRepository = new RideRepository();
        }
        /// <summary>
        /// UC1 Fare for single ride
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="time"></param>
        [TestCase(6, 4)]
        public void GiveDistanceAndTIme_CalcualteFare(int distance, int time)
        {
            Ride ride = new Ride(distance, time);
            int calFare = fare.CalculateFaresForSingleRide(ride);
            Assert.AreEqual(64, calFare);
        }
        /// <summary>
        /// TC 1.1 Invalid Distance
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="time"></param>
        [TestCase(3, 4)]
        public void WrongDistanceCalcualteFare(int distance, int time)
        {
            Ride ride = new Ride(distance, time);
            CustomException exception = Assert.Throws<CustomException>(() => fare.CalculateFaresForSingleRide(ride));
            Assert.AreEqual(exception.type, CustomException.Exceptions.DistanceSmallerThanFive);
        }
        /// <summary>
        /// TC 1.2 Invalid time
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="time"></param>
        [TestCase(5, 0)]
        public void InvalidTime_ThrowException(int distance, int time)
        {
            Ride ride = new Ride(distance, time);
            CustomException exception = Assert.Throws<CustomException>(() => fare.CalculateFaresForSingleRide(ride));
            Assert.AreEqual(exception.type, CustomException.Exceptions.TimeSmallerThaOneMin);
        }

        /// <summary>
        /// UC2 Calculate fare FOR Multiple ride
        /// </summary>
        [Test]
        public void GiveDistanceAndTIme_CalcualteFareMultipleRide()
        {
            Ride rideOne = new Ride(6, 4);
            Ride rideTwo = new Ride(5, 6);
            List<Ride> rides = new List<Ride>();
            rides.Add(rideOne);
            rides.Add(rideTwo);
            Assert.AreEqual(120, fare.CalculateFareForMultipleRide(rides));
        }

        /// <summary>
        /// Tc 2.1 Invalid Distance throw exception
        /// </summary>
        [Test]
        public void GiveInvalidDistance_CalcualteFareMultipleRide()
        {
            Ride rideOne = new Ride(4, 4);
            Ride rideTwo = new Ride(3, 6);
            List<Ride> rides = new List<Ride>();
            rides.Add(rideOne);
            rides.Add(rideTwo);
            int calFare;
            try
            {
                calFare = fare.CalculateFareForMultipleRide(rides);
            }
            catch (CustomException exception)
            {
                Assert.AreEqual("Distance should be greater than or equal to Five Km", exception.Message);
            }
        }

        /// <summary>
        /// TC 2.2 Invalid Time throw exception
        /// </summary>
        [Test]
        public void GiveInvalidTime_CalcualteFareMultipleRide()
        {
            Ride rideOne = new Ride(7, 0);
            Ride rideTwo = new Ride(8, 0);
            List<Ride> rides = new List<Ride>();
            rides.Add(rideOne);
            rides.Add(rideTwo);
            int calFare;
            try
            {
                calFare = fare.CalculateFareForMultipleRide(rides);
            }
            catch (CustomException exception)
            {
                Assert.AreEqual("Time should be greater than One Minutes", exception.Message);
            }
        }
        /// <summary>
        /// TC 2.3 Invalid Distance And Time
        /// </summary>
        [Test]
        public void GiveInvalidDistanceAndTime_CalcualteFareMultipleRide()
        {
            Ride rideOne = new Ride(4, 0);
            Ride rideTwo = new Ride(2, 0);
            List<Ride> rides = new List<Ride>();
            rides.Add(rideOne);
            rides.Add(rideTwo);
            int calFare;
            try
            {
                calFare = fare.CalculateFareForMultipleRide(rides);
            }
            catch (CustomException exception)
            {
                Assert.AreEqual("Invalid Distance and Time", exception.Message);
            }
        }


        /// <summary>
        /// UC 3 TC 3.1 Give average cost of ride
        /// </summary>
        [Test]
        public void GiveDistanceAndTime_CalcualteAverage_FareFor_MultipleRide()
        {
            Ride rideOne = new Ride(6, 4);
            Ride rideTwo = new Ride(5, 6);
            Ride rideThree = new Ride(5, 6);
            List<Ride> rides = new List<Ride>();
            rides.Add(rideOne);
            rides.Add(rideTwo);
            rides.Add(rideThree);
            fare.CalculateFareForMultipleRide(rides);
            int avergareFair = fare.averageCostOfRide;
            Assert.AreEqual(58, avergareFair);
        }

        /// <summary>
        /// UC 3 TC 3.2 Give total number of Rides
        /// </summary>
        [Test]
        public void GiveDistanceAndTime_CalcualteNumberOfRidesFor_MultipleRide()
        {
            Ride rideOne = new Ride(6, 4);
            Ride rideTwo = new Ride(5, 6);
            Ride rideThree = new Ride(5, 6);
            List<Ride> rides = new List<Ride>();
            rides.Add(rideOne);
            rides.Add(rideTwo);
            rides.Add(rideThree);
            fare.CalculateFareForMultipleRide(rides);
            Assert.AreEqual(3, fare.numberOfRides);
        }

        /// <summary>
        /// UC 4 TC 4.1 Valid Invoice for user
        /// </summary>
        [Test]
        public void GivenValidUserIdGenerateCabInvoice()
        {
            Ride rideOne = new Ride(5, 7);
            Ride rideTwo = new Ride(6, 10);
            Ride rideThree = new Ride(6, 23);
            rideRepository.AddRide("Sankalp", rideOne);
            rideRepository.AddRide("Sankalp", rideTwo);
            rideRepository.AddRide("Sankalp", rideThree);
            //Fare for multiple ride but give list of rides for a perticular rider(User) and then pass to Calculate fare
            Assert.AreEqual(210, fare.CalculateFareForMultipleRide(rideRepository.GetListOfRides("Sankalp")));
        }
        /// <summary>
        /// TC 4.2 Invalid user id throw custom exception
        /// </summary>
        [Test]
        public void GivenInValidUserIdGenerateCabInvoice()
        {
            try
            {
                Ride rideOne = new Ride(5, 7);
                Ride rideTwo = new Ride(6, 10);
                Ride rideThree = new Ride(6, 23);
                rideRepository.AddRide("Sankalp", rideOne);
                rideRepository.AddRide("Sankalp", rideTwo);
                rideRepository.AddRide("Sankalp", rideThree);
            }
            catch (CustomException)
            {
                //Fare for multiple ride but give list of rides for a perticular rider(User) and then pass to Calculate fare
                Assert.AreEqual("Invalid User ID", fare.CalculateFareForMultipleRide(rideRepository.GetListOfRides("Kaithwas")));
            }
        }
    }
}