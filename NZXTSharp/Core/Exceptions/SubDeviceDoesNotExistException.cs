using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Exceptions
{
    /// <summary>
    /// Thrown When a user attempts to reference an
    /// <see cref="ISubDevice"/>that does not exist.
    /// </summary>
    public class SubDeviceDoesNotExistException : Exception
    {
        /// <inheritdoc/>
        public SubDeviceDoesNotExistException()
        {

        }

        /// <inheritdoc/>
        public SubDeviceDoesNotExistException(string message)
            : base(message)
        {

        }

        /// <inheritdoc/>
        public SubDeviceDoesNotExistException(string message, Exception innerException) : 
            base(message, innerException)
        {
        }
    }
}
