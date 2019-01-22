using System;
using System.Collections.Generic;
using System.Text;

namespace RGB.NET.Devices.NZXT {
    public class NZXTStripRGBDevice : NZXTRGBDevice<NZXTStripRGBDeviceInfo> {

        internal NZXTStripRGBDevice(NZXTStripRGBDeviceInfo info)
            : base(info) 
        { }

        protected override void InitializeLayout() {
            
        }
    }
}
