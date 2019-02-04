using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Exceptions;

namespace NZXTSharp
{

    /// <summary>
    /// Represents an LSS param.
    /// </summary>
    public class LSS : IParam {
        private readonly List<List<int>> LSSTable = new List<List<int>>() {
            new List<int>() {0x00, 0x08, 0x10, 0x18},
            new List<int>() {0x01, 0x09, 0x11, 0x19},
            new List<int>() {0x02, 0x0a, 0x12, 0x1a},
            new List<int>() {0x03, 0x0b, 0x13, 0x1b},
            new List<int>() {0x04, 0x0c, 0x14, 0x1c}
        };
        private readonly int _speed;
        private readonly int _LEDSize;
        private readonly int _Value;

        /// <inheritdoc/>
        private readonly List<NZXTDeviceType> _CompatibleWith = new List<NZXTDeviceType>() { NZXTDeviceType.HuePlus };

        /// <inheritdoc/>
        public int Value { get => GetValue(); }

        /// <summary>
        /// Constructs an <see cref="LSS"/> param.
        /// </summary>
        /// <param name="speed">Speed values must be 0-4 (inclusive). 0 being slowest, 2 being normal, and 4 being fastest. Defaults to 2.</param>
        /// <param name="LEDSize">The LED group size, LEDSizes must be between 3-6 (inclusive).</param>
        public LSS(int speed = 2, int LEDSize = 4) {
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

        /// <inheritdoc/>
        public int GetValue() {
            return LSSTable[_speed][this._LEDSize - 3];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        public static implicit operator byte(LSS param) {
            return (byte)param.GetValue();
        }
    }
}
