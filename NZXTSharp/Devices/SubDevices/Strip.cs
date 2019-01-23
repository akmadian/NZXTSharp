using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Devices {
    public class Strip : ISubDevice {

        private bool _IsActive = true;

        public bool IsActive { get => _IsActive; }

        public NZXTDeviceType Type { get => NZXTDeviceType.Fan; }

        public int NumLeds { get => 8; }


        public Strip() {

        }

        public void ToggleState() {
            this._IsActive = (this._IsActive ? false : true);
        }
    }
}
