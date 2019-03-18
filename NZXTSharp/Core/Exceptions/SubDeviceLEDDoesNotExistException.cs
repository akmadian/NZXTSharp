using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Exceptions
{
    public class SubDeviceLEDDoesNotExistException : Exception
    {
        public SubDeviceLEDDoesNotExistException()
        {

        }

        public SubDeviceLEDDoesNotExistException(string message)
            : base(message)
        {

        }

    }
}
