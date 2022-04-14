using Cab_Invoice_Generator;
using NUnit.Framework;

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

        [TestCase(6, 4)]
        public void GiveDistanceAndTIme_CalcualteFare(int distance, int time)
        {
            Ride ride = new Ride(distance, time);
            int calFare = fare.CalculateFaresForSingleRide(ride);
            Assert.AreEqual(64, calFare);
        }

        [TestCase(3, 4)]
        public void WrongDistanceCalcualteFare(int distance, int time)
        {
            Ride ride = new Ride(distance, time);
            CustomException exception = Assert.Throws<CustomException>(() => fare.CalculateFaresForSingleRide(ride));
            Assert.AreEqual(exception.type, CustomException.Exceptions.DistanceSmallerThanFive);
        }

        [TestCase(5, 0)]
        public void InvalidTime_ThrowException(int distance, int time)
        {
            Ride ride = new Ride(distance, time);
            CustomException exception = Assert.Throws<CustomException>(() => fare.CalculateFaresForSingleRide(ride));
            Assert.AreEqual(exception.type, CustomException.Exceptions.TimeSmallerThaOneMin);
        }
    }
}