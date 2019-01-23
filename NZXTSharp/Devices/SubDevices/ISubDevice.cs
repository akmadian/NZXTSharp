using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Devices {
    public interface ISubDevice {

        NZXTDeviceType Type { get; }

        bool IsActive { get; }

        int NumLeds { get; }

        List<bool> Leds { get; }

        void ToggleState();

        void ToggleLed(int Index);

        void ToggleLedRange(int Start, int End);

        void SetLedRange(int Start, int End, bool Value);

        void AllLedOn();

        void AllLedOff();

        string LedsToString();
    }
}
