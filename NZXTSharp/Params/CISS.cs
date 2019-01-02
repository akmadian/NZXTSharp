using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Exceptions;

namespace NZXTSharp.Params {
    class CISS : IParam {
        private int colorIndex;
        private int speed;
        private int evaluatedIndex;
        private int _Value;
        private List<string> _CompatibleWith = new List<string>() { "HuePlus" };

        public int Value { get; }
        public List<string> CompatibleWith { get; }

        public CISS(int speed) {
            this.speed = speed;
        }

        public CISS(int colorIndex, int speed) {
            this.colorIndex = colorIndex;
            this.speed = speed;

            this.evaluatedIndex = colorIndex * 2;
        }

        private void ValidateInput() {
            if (speed > 4 || speed < 0)
                throw new InvalidParamException("Invalid Param; Speed Must Be Between 0 and 4 (inclusive).");

            if (colorIndex > 15 || colorIndex < 0)
                throw new InvalidParamException("Invalid Param; ColorIndex Value Must Be Between 15 and 0 (inclusive).");
        }

        public int GetValue() {
            return Convert.ToInt32(evaluatedIndex.DecimalToByte().ToString() + speed.ToString());
        }

        public static implicit operator byte(CISS param) {
            return (byte)param.GetValue();
        }
    }
}
