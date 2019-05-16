using System;
using System.Collections.Generic;
using System.Text;

using RGB.NET.Core;

namespace RGB.NET.Devices.NZXT
{
    public class NZXTKrakenXRingDeviceInfo : NZXTRGBDeviceInfo
    {
        internal NZXTKrakenXRingDeviceInfo(int deviceIndex, _NZXTDeviceInfo nativeInfo)
            : base(deviceIndex, RGBDeviceType.Cooler, nativeInfo) { }
    }
}
