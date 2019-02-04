using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.HuePlus
{

    /// <summary>
    /// Represents an 02 effect Param
    /// </summary>
    internal class _02Param : IParam {
        private readonly int _Value = 0x02;

        /// <inheritdoc/>
        public int Value { get => GetValue(); }

        /// <inheritdoc/>
        public int GetValue() {
            return 0x02;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        public static implicit operator byte(_02Param param) {
            return (byte)0x02;
        }
    }
}
