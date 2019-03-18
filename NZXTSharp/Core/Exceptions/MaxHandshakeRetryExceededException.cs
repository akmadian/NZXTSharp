using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Exceptions
{
    /// <summary>
    /// Thrown when the maximum number of handshake attempts 
    /// has been exceeded during device intitialization.
    /// 
    /// Max Retry Count is 5 by default.
    /// </summary>
    public class MaxHandshakeRetryExceededException : Exception
    {
        /// <inheritdoc/>
        public MaxHandshakeRetryExceededException()
        {

        }

        /// <inheritdoc/>
        public MaxHandshakeRetryExceededException(string message)
            : base(message)
        {

        }


        /// <summary>
        /// Constructs a <see cref="MaxHandshakeRetryExceededException"/>,
        /// with more information about the max handshake retry.
        /// </summary>
        /// <param name="MaxCount">The max retry count.</param>
        public MaxHandshakeRetryExceededException(int MaxCount)
            : base(String.Format("Max handshake retry count ({0}) exceeded.", MaxCount))
        {

        }

        /// <inheritdoc/>
        public MaxHandshakeRetryExceededException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
