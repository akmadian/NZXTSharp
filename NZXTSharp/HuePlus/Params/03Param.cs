using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;

namespace NZXTSharp.HuePlus {

    /// <summary>
    /// Represents an 03 effect param.
    /// </summary>
    internal class _03Param : IParam {
        private int _Value = 0x03;
        private List<string> _CompatibleWith = new List<string>() { "HuePlus" };

        /// <inheritdoc/>
        public int Value { get => GetValue(); }

        /// <inheritdoc/>
        public int GetValue() {
            return 0x03;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        public static implicit operator byte(_03Param param) {
            return (byte)0x03;
        }
    }
}
