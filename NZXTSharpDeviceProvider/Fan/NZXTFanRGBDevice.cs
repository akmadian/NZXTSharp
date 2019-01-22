using System;
using System.Collections.Generic;
using System.Text;

namespace RGB.NET.Devices.NZXT {
    public class NZXTFanRGBDevice : NZXTRGBDevice<NZXTFanRGBDeviceInfo> {

        internal NZXTFanRGBDevice(NZXTFanRGBDeviceInfo info)
            : base(info) 
        { }

        protected override void InitializeLayout() {
            
        }
    }
}
