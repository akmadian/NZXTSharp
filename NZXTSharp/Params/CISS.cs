using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Exceptions;

namespace NZXTSharp.Params {
    internal class CISS : IParam {
        private int colorIndex;
        private int speed;
        private int evaluatedIndex;
        private int _Value;
        private List<string> _CompatibleWith = new List<string>() { "HuePlus" };

        public int Value { get => GetValue(); }
        public List<string> CompatibleWith { get; }

        public CISS(int speed) {
            this.speed = speed;
            ValidateInput();
        }

        public CISS(int colorIndex, int speed) {
            this.colorIndex = colorIndex;
            this.speed = speed;

            ValidateInput();

            this.evaluatedIndex = colorIndex * 2;
        }

        private void ValidateInput() {
            if (speed > 4 || speed < 0)
                throw new InvalidParamException("Invalid Param; Speed Must Be Between 0 and 4 (inclusive).");

            if (colorIndex > 7 || colorIndex < 0)
                throw new InvalidParamException("Invalid Param; ColorIndex Value Must Be Between 7 and 0 (inclusive). (Zero-Indexed)");
        }

        public int GetValue() {
            string concatenated = evaluatedIndex.ToString("X") + speed.ToString();
            return int.Parse(concatenated, System.Globalization.NumberStyles.HexNumber);
        }

        public static implicit operator byte(CISS param) {
            return (byte)param.GetValue();
        }
    }
}
