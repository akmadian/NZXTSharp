using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Exceptions {
    class InvalidParamException : Exception {

        public InvalidParamException() {

        }

        public InvalidParamException(string message) 
            : base(message)    
        {
            
        }

    }
}
