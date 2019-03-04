using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Exceptions {
    public class IncompatibleParamException : Exception {
        public IncompatibleParamException() {

        }

        public IncompatibleParamException(string message)
            : base(message) {

        }

        public IncompatibleParamException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
