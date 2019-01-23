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

        public void ToggleLedRange(int Start, int End) {
            for (int Index = Start; Index <= End; Index++) {
                _Leds[Index] = (this._Leds[Index] ? false : true);
            }
        }

        public void SetLedRange(int Start, int End, bool Value) {
            for (int Index = Start; Index <= End; Index++) {
                _Leds[Index] = Value;
            }
        }

        public void AllLedOn() {
            for (int index = 0; index < 8; index++) {
                _Leds[index] = true;
            }
        }

        public void AllLedOff() {
            for (int index = 0; index < 8; index++) {
                _Leds[index] = false;
            }
        }

        public string LedsToString() {
            StringBuilder sb = new StringBuilder();
            _Leds.ForEach(c => sb.Append(c + " "));
            return sb.ToString();
        }
    }
}
