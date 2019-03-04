using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Exceptions {
    public class SubDeviceDoesNotExistException : Exception {
        public SubDeviceDoesNotExistException() {

        }

        public SubDeviceDoesNotExistException(string message)
            : base(message) {

        }

    }
}
