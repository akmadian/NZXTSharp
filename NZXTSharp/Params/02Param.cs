using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Params {
    internal class _02Param : IParam {
        private int _Value = 0x02;
        private List<string> _CompatibleWith = new List<string>() { "HuePlus" };

        public int Value { get => GetValue(); }
        public List<string> CompatibleWith { get; }

        public int GetValue() {
            return 0x02;
        }

        public static implicit operator byte(_02Param param) {
            return (byte)0x02;
        }
    }
}
