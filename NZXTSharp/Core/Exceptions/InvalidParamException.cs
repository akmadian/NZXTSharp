using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class InvalidParamException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public InvalidParamException()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public InvalidParamException(string message) 
            : base(message)    
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public InvalidParamException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
