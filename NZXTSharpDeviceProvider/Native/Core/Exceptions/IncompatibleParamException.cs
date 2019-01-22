using System;
using System.Collections.Generic;
using System.Text;

namespace RGB.NET.Devices.NZXT.Native.Core.Exceptions {
    class IncompatibleParamException : Exception {
        public IncompatibleParamException() {

        }

        public IncompatibleParamException(string message)
            : base(message) {

        }
    }
}
