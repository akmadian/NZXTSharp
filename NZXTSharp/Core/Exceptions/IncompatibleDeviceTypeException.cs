using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Exceptions
{
    /// <summary>
    /// Thrown when an <see cref="NZXTDeviceType"/> is passed to a method
    /// or constructor that is not compatible with that method or constructor.
    /// </summary>
    public class IncompatibleDeviceTypeException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public IncompatibleDeviceTypeException()
            : base("Invalid NZXTDeviceType given to USBController.")
        {

        }

        /// <summary>
        /// Constructs an <see cref="IncompatibleDeviceTypeException"/> with a custom message.
        /// </summary>
        /// <param name="message">A custom message.</param>
        public IncompatibleDeviceTypeException(string message)
            : base(message)
        {

        }

        /// <inheritdoc/>
        public IncompatibleDeviceTypeException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
