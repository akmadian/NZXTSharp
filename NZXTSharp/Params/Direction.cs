using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Params {
    class Direction : IParam {
        private bool _withMovement;
        private bool _isForward;
        private int _Value;
        private List<string> _CompatibleWith = new List<string>() { "HuePlus" };

        public bool WithMovement { get; }
        public bool IsForward { get; }
        public int Value { get; }
        public List<string> CompatibleWith { get; }

        public Direction(bool isForward, bool withMovement) {
            this._withMovement = withMovement;
            this._isForward = isForward;
        }

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

        public static implicit operator byte(Direction param) {
            return (byte)param.GetValue();
        }
    }
}
