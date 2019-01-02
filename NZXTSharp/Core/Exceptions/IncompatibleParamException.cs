using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Exceptions {
    class IncompatibleParamException : Exception {
        public IncompatibleParamException() {

        }

        public IncompatibleParamException(string message)
            : base(message) {

        }
    }
}
