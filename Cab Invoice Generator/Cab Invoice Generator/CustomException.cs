using System;
using System.Collections.Generic;
using System.Text;

namespace Cab_Invoice_Generator
{
    public class CustomException : Exception
    {
        public ExceptionType type;
        public enum ExceptionType
        {
            TimeSmallerThaOneMin,
            DistanceSmallerThanFive,
            InvalidUserId,
            InvalidDistanceAndTime,
            InvalidRideType
        }
        public CustomException(ExceptionType type, string message) : base(message)
        {
            this.type = type;
        }
    }
}
