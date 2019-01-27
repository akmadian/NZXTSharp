using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Exceptions;
using System.Linq;

namespace NZXTSharp.Devices {

    /// <summary>
    /// Represents an RGB Strip subdevice.
    /// </summary>
    public class Strip : ISubDevice {

        private bool _IsActive = true;

        private List<bool> _Leds = new List<bool>()
        {
            true, true, true, true, true,
            true, true, true, true, true
        };

        /// <summary>
        /// Whether or not the current <see cref="Strip"/> instance is active (on).
        /// </summary>
        public bool IsActive { get => _IsActive; }

        /// <summary>
        /// Returns the <see cref="NZXTDeviceType"/> of the fan.
        /// </summary>
        public NZXTDeviceType Type { get => NZXTDeviceType.Fan; }

        /// <summary>
        /// Returns the number of LEDs available on the <see cref="Strip"/>.
        /// </summary>
        public int NumLeds { get => 10; }

        /// <summary>
        /// A list containing the power states of the <see cref="Strip"/>'s LEDs.
        /// </summary>
        public List<bool> Leds { get => _Leds; }

        /// <summary>
        /// Constructs a <see cref="Strip"/> instance.
        /// </summary>
        public Strip() {

        }

        /// <summary>
        /// Toggles the <see cref="Strip"/>'s state.
        /// </summary>
        public void ToggleState() {
            this._IsActive = (this._IsActive ? false : true);
        }

        /// <summary>
        /// Toggles a specific LED owned by the <see cref="Strip"/> device.
        /// </summary>
        /// <param name="Index">The index in the <see cref="Strip"/>'s <see cref="Leds"/> list to toggle.</param>
        public void ToggleLed(int Index) {
            if (Index > 7 || Index < 0) { throw new SubDeviceLEDDoesNotExistException(); }
            this._Leds[Index] = (this._Leds[Index] ? false : true);
        }

        /// <summary>
        /// Toggles all LEDs between a given <paramref name="Start"/> and <paramref name="End"/> index.
        /// </summary>
        /// <param name="Start">The index in the <see cref="Strip"/>'s <see cref="Leds"/> list to start at.</param>
        /// <param name="End">The index in the <see cref="Strip"/>'s <see cref="Leds"/> list to end at.</param>
        public void ToggleLedRange(int Start, int End) {
            for (int Index = Start; Index <= End; Index++) {
                _Leds[Index] = (this._Leds[Index] ? false : true);
            }
        }

        /// <summary>
        /// Sets all LEDs between a given <paramref name="Start"/> index and an <paramref name="End"/> index to a given <paramref name="Value"/>.
        /// </summary>
        /// <param name="Start">The index in the <see cref="Strip"/>'s <see cref="Leds"/> list to start at.</param>
        /// <param name="End">The index in the <see cref="Strip"/>'s <see cref="Leds"/> list to end at.</param>
        /// <param name="Value">The value to set each LED to.</param>
        public void SetLedRange(int Start, int End, bool Value) {
            for (int Index = Start; Index <= End; Index++) {
                _Leds[Index] = Value;
            }
        }

        /// <summary>
        /// Sets all LEDs in the <see cref="Strip"/>'s <see cref="Leds"/> list to true.
        /// </summary>
        public void AllLedOn() {
            for (int index = 0; index < 10; index++) {
                _Leds[index] = true;
            }
        }

        /// <summary>
        /// Sets all LEDs in the <see cref="Strip"/>'s <see cref="Leds"/> list to false.
        /// </summary>
        public void AllLedOff() {
            for (int index = 0; index < 10; index++) {
                _Leds[index] = false;
            }
        }

        /// <summary>
        /// Returns a string with all LED states.
        /// </summary>
        /// <returns></returns>
        public string LedsToString() {
            StringBuilder sb = new StringBuilder();
            _Leds.ForEach(c => sb.Append(c + " "));
            return sb.ToString();
        }
    }
}
