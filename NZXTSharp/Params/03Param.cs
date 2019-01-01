using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;

namespace NZXTSharp.Params {
    public class _03Param : IParam {
        private int _Value = 0x03;

        public int Value { get; }

        public int GetValue() {
            return 0x03;
        }

        public static implicit operator byte(_03Param param) {
            return (byte)0x03;
        }
    }
}
