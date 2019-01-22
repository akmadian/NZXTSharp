using System;
using System.Collections.Generic;
using System.Text;

using RGB.NET.Core;

namespace RGB.NET.Devices.NZXT {
    public class NZXTCoolerRGBDevice : NZXTRGBDevice<NZXTCoolerRGBDeviceInfo> {

        internal NZXTCoolerRGBDevice(NZXTCoolerRGBDeviceInfo info)
            : base(info) { }

        protected override void InitializeLayout() {

        }
    }
}
