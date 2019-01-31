using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Devices {
    internal interface IUSBDevice : INZXTDevice {

        SerialDeviceID DeviceID { get; }

    }
}
