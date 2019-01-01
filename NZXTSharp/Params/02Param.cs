using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Params {
    public class _02Param : IParam {
        private int _Value = 0x02;

        public int Value { get; }

        public int GetValue() {
            return 0x02;
        }

        public static implicit operator byte(_02Param param) {
            return (byte)0x02;
        }
    }
}
