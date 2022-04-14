using Cab_Invoice_Generator;
using NUnit.Framework;
using System.Collections.Generic;

namespace CabInvoiceTest
{
    public class Tests
    {
        InvoiceGenerator fare;
        [SetUp]
        public void Setup()
        {
            fare = new InvoiceGenerator();
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
        /// UC2 Calculate faRE FOR Multiple ride
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
    }
}