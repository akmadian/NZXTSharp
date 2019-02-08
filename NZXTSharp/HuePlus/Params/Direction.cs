using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.HuePlus {

    /// <summary>
    /// Represents a <see cref="Direction"/> effect param.
    /// </summary>
    public class Direction : IParam {
        private bool _withMovement;
        private bool _isForward;
        private int _Value;
        private List<string> _CompatibleWith = new List<string>() { "HuePlus" };

        /// <summary>
        /// Whether or not the effect will move.
        /// </summary>
        public bool WithMovement { get; }

        /// <summary>
        /// Whether or not the effect will move forward or backward.
        /// </summary>
        public bool IsForward { get; }

        /// <inheritdoc/>
        public int Value { get => GetValue(); }

        /// <summary>
        /// Constructs a <see cref="Direction"/> param.
        /// </summary>
        /// <param name="isForward">Whether or not the param moves forward or backward.</param>
        /// <param name="withMovement">Whether or not the effect will move smoothly.</param>
        public Direction(bool isForward = true, bool withMovement = true) {
            this._withMovement = withMovement;
            this._isForward = isForward;
        }

        /// <inheritdoc/>
        public int GetValue() {
            if (IsForward)
                if (WithMovement)
                    return 0x0b; // Forward W/ movement
                else
                    return 0x03; // Forward W/O movement
            else
                if (WithMovement)
                return 0x1b; // Backward W/ movement
            else
                return 0x13; // Backward W/O movement
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        public static implicit operator byte(Direction param) {
            return (byte)param.GetValue();
        }
    }
}
