using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Exceptions;

namespace NZXTSharp.Params {

    /// <summary>
    /// Represents a CISS effect param.
    /// </summary>
    internal class CISS : IParam {
        private readonly int colorIndex;
        private int speed;
        private int evaluatedIndex;
        private readonly int _Value;

        /// <inheritdoc/>
        public int Value { get => GetValue(); }

        /// <summary>
        /// Constructs a <see cref="CISS"/> instance.
        /// </summary>
        /// <param name="speed">Speed values must be 0-4 (inclusive). 0 being slowest, 2 being normal, and 4 being fastest. Defaults to 2.</param>
        public CISS(int speed) {
            this.speed = speed;
            ValidateInput();
        }

        /// <summary>
        /// Constructs a <see cref="CISS"/> instance.
        /// </summary>
        /// <param name="colorIndex">The index of the color in the list.</param>
        /// <param name="speed">Speed values must be 0-4 (inclusive). 0 being slowest, 2 being normal, and 4 being fastest. Defaults to 2.</param>
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

        /// <inheritdoc/>
        public int GetValue() {
            string concatenated = evaluatedIndex.ToString("X") + speed.ToString();
            return int.Parse(concatenated, System.Globalization.NumberStyles.HexNumber);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        public static implicit operator byte(CISS param) {
            return (byte)param.GetValue();
        }
    }
}
