using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Devices {
    public class Fan : ISubDevice {

        private bool _IsActive = true;

        private List<bool> _Leds = new List<bool>()
        {
            true, true, true, true,
            true, true, true, true
        };

        public bool IsActive { get => _IsActive; }

        public NZXTDeviceType Type { get => NZXTDeviceType.Fan; }

        public int NumLeds { get => 8; }

        public List<bool> Leds { get => _Leds; }

        public Fan() {

        }

        public void ToggleState() {
            this._IsActive = (this._IsActive ? false : true);
        }

        public void ToggleLed(int Index) {
            this._Leds[Index] = (this._Leds[Index] ? false : true);
        }
    }
}
