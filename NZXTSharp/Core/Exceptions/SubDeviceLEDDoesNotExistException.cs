using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Exceptions {
    internal class SubDeviceLEDDoesNotExistException : Exception {
        public SubDeviceLEDDoesNotExistException() {

        }

        public SubDeviceLEDDoesNotExistException(string message)
            : base(message) {

        }

    }
}
