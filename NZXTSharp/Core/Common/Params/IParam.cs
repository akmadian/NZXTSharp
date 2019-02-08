using System;
using System.Collections.Generic;
using System.Text;


namespace NZXTSharp
{

    /// <summary>
    /// An effect parameter.
    /// </summary>
    public interface IParam {

        /// <summary>
        /// The int representation of the effect's byte value.
        /// </summary>
        int Value { get; }

        /// <summary>
        /// Gets the int representation of the effect's byte value.
        /// </summary>
        /// <returns></returns>
        int GetValue();


    }
}
