using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Exceptions {
    internal class SubDeviceDoesNotExistException : Exception {
        public SubDeviceDoesNotExistException() {

        }

        public SubDeviceDoesNotExistException(string message)
            : base(message) {

        }

    }
}
