using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Exceptions
{
    /// <summary>
    ///Thrown when an invalid speed value is passed to an
    /// <see cref="IParam"/> or <see cref="IEffect"/> constructor.
    /// 
    /// Speed values must be 0 - 4 (inclusive); 
    /// 0 being slowest, 2 being normal, and 4 being fastest.
    /// </summary>
    public class InvalidEffectSpeedException : Exception
    {

        /// <inheritdoc/>
        public InvalidEffectSpeedException() 
            : base ("NZXTSharp.Exceptions.InvalidEffectSpeedException; Effect Speed Must Be Between 0 and 4 (Inclusive).")
        {

        }

        /// <inheritdoc/>
        public InvalidEffectSpeedException(string message)
            : base(message)
        {

        }

        /// <inheritdoc/>
        public InvalidEffectSpeedException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
