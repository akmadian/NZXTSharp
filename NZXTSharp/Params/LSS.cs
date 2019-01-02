using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Exceptions;

namespace NZXTSharp.Params {
    class LSS : IParam {
        private List<List<int>> LSSTable = new List<List<int>>() {
            new List<int>() {0x00, 0x08, 0x10, 0x18},
            new List<int>() {0x01, 0x09, 0x11, 0x19},
            new List<int>() {0x02, 0x0a, 0x12, 0x1a},
            new List<int>() {0x03, 0x0b, 0x13, 0x1b},
            new List<int>() {0x04, 0x0c, 0x14, 0x1c}
        };
        private int _speed;
        private int _LEDSize;
        private int _Value;
        private List<string> _CompatibleWith = new List<string>() { "HuePlus" };

        public int Value { get; }
        public List<string> CompatibleWith { get; }

        public LSS(int speed, int LEDSize) {
            this._speed = speed;
            this._LEDSize = LEDSize;
            ValidateParams();
        }

        private void ValidateParams() {
            if (_speed > 4 || _speed < 0)
                throw new InvalidParamException("Invalid Param; Speed Must Be Between 0 and 4 (inclusive).");

            if (_LEDSize > 6 || _LEDSize < 3)
                throw new InvalidParamException("Invalid Param; LEDSize Must Be Between 3 and 6 (inclusive).");
        }

        public int GetValue() {
            return LSSTable[_speed][this._LEDSize - 3];
        }

        public static implicit operator byte(LSS param) {
            return (byte)param.GetValue();
        }
    }
}
