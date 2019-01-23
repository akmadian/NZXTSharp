using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Devices {
    public interface ISubDevice {

        NZXTDeviceType Type { get; }

        bool IsActive { get; }

        int NumLeds { get; }

        void ToggleState();
    }
}
