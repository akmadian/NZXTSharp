using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Exceptions {
    public class InvalidEffectSpeedException : Exception {

        public InvalidEffectSpeedException() 
            : base ("NZXTSharp.Exceptions.InvalidEffectSpeedException; Effect Speed Must Be Between 0 and 4 (Inclusive).") {

        }

        public InvalidEffectSpeedException(string message)
            : base(message) {

        }
    }
}
