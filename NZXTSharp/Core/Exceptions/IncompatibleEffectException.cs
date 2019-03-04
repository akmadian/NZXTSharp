using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Exceptions {
    public class IncompatibleEffectException : Exception {

        private static string baseString = "NXZTSharp.Exceptions.IncompatibleEffectException; ";

        public IncompatibleEffectException() 
            : base(baseString + "Invalid Effect Supplied To ApplyEffect()") {

        }

        public IncompatibleEffectException(string message)
            : base(message) {

        }

        public IncompatibleEffectException(string DeviceName, string EffectName) 
            : base(string.Format(baseString + "Invalid Effect \"{0}\" Supplied To ApplyEffect() Of Device \"{1}\"",
                EffectName, DeviceName)){

        }

        public IncompatibleEffectException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
