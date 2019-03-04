using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Exceptions
{
    /// <summary>
    /// Thrown when a param object passed to an <see cref="IEffect"/> 
    /// constructor is not compatible with that <see cref="IEffect"/>.
    /// </summary>
    public class IncompatibleParamException : Exception
    {

        /// <inheritdoc/>
        public IncompatibleParamException()
        {

        }

        /// <inheritdoc/>
        public IncompatibleParamException(string message)
            : base(message)
        {

        }

        /// <inheritdoc/>
        public IncompatibleParamException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
