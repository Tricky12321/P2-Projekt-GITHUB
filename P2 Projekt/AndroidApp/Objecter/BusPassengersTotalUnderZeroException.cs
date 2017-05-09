using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BusPassengersTotalUnderZeroException : Exception
{
    public BusPassengersTotalUnderZeroException() { }
    public BusPassengersTotalUnderZeroException(string message) : base(message) { }
    public BusPassengersTotalUnderZeroException(string message, Exception inner) : base(message, inner) { }
    protected BusPassengersTotalUnderZeroException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

