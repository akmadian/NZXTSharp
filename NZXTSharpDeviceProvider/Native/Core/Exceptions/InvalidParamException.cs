using System;
using System.Collections.Generic;
using System.Text;

namespace RGB.NET.Devices.NZXT.Native.Core.Exceptions {
    class InvalidParamException : Exception {

        public InvalidParamException() {

        }

        public InvalidParamException(string message) 
            : base(message)    
        {
            
        }

    }
}
