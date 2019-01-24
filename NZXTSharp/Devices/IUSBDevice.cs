using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Devices {
    public interface IUSBDevice : INZXTDevice {

        int DeviceID { get; }

    }
}
