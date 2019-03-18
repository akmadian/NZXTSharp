using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Exceptions {

    /// <summary>
    /// Thrown when an effect passed to <see cref="INZXTDevice.ApplyEffect(IEffect)"/> 
    /// is not compatible with that <see cref="INZXTDevice"/>.
    /// </summary>
    public class IncompatibleEffectException : Exception {

        private static string baseString = "NXZTSharp.Exceptions.IncompatibleEffectException; ";

        /// <inheritdoc/>
        public IncompatibleEffectException() 
            : base(baseString + "Invalid Effect Supplied To ApplyEffect()") {

        }

        /// <inheritdoc/>
        public IncompatibleEffectException(string message)
            : base(message) {

        }

        /// <summary>
        /// Constructs an <see cref="IncompatibleEffectException"/> with more information
        /// about the <see cref="INZXTDevice"/> and <see cref="IEffect"/>.
        /// </summary>
        /// <param name="DeviceName"></param>
        /// <param name="EffectName"></param>
        public IncompatibleEffectException(string DeviceName, string EffectName) 
            : base(string.Format(baseString + "Invalid Effect \"{0}\" Supplied To ApplyEffect() Of Device \"{1}\"",
                EffectName, DeviceName)){

        }

        /// <inheritdoc/>
        public IncompatibleEffectException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
