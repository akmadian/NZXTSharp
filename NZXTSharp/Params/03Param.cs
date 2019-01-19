using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;

namespace NZXTSharp.Params {
    public class _03Param : IParam {
        private int _Value = 0x03;
        private List<string> _CompatibleWith = new List<string>() { "HuePlus" };

        public int Value { get => GetValue(); }
        public List<string> CompatibleWith { get; }

        public int GetValue() {
            return 0x03;
        }

        public static implicit operator byte(_03Param param) {
            return (byte)0x03;
        }
    }
}
