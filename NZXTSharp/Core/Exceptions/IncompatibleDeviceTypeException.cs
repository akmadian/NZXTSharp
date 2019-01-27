using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Exceptions
{
    internal class IncompatibleDeviceTypeException : Exception
    {

        public IncompatibleDeviceTypeException()
            : base("Invalid NZXTDeviceType given to USBController.")
        {

        }

        public IncompatibleDeviceTypeException(string message)
            : base(message)
        {

        }

        public IncompatibleDeviceTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
