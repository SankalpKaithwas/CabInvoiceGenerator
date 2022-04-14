using System;
using System.Collections.Generic;
using System.Text;

namespace Cab_Invoice_Generator
{
    public class CustomException : Exception
    {
        public Exceptions type;
        public enum Exceptions
        {
            TimeSmallerThaOneMin,
            DistanceSmallerThanFive,
            InvalidUserId,
            InvalidDistanceAndTime
        }
        public CustomException(Exceptions type, string message) : base(message)
        {
            this.type = type;
        }
    }
}
