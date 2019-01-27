using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Exceptions
{
    internal class MaxHandshakeRetryExceededException : Exception
    {
        public MaxHandshakeRetryExceededException()
        {

        }

        public MaxHandshakeRetryExceededException(string message)
            : base(message)
        {

        }

        public MaxHandshakeRetryExceededException(int MaxCount)
            : base(String.Format("Max handshake retry count ({0}) exceeded.", MaxCount))
        {

        }
    }
}
