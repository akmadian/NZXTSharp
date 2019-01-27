using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Devices;

namespace NZXTSharp.Devices {

    /// <summary>
    /// Contains the Manufacturer and Product IDs of NZXT devices.
    /// </summary>
    internal enum SerialDeviceID {

        /// <summary>
        /// An unknown ID.
        /// </summary>
        Unknown = -1,
        ManufacturerID = 0x1e71,
        
        // Kraken Devices
        KrakenM = 0x1715,
        KrakenX   = 0x170e,
        
        // Hue Devices
        Hue2       = 0x2001,
        HueAmbient = 0x2002,

        // Grid Devices
        GridV2 = Unknown,
        GridV3 = 0x1711,

        // Motherboards
        N7      = 0x1713,
        N7_Z390 = 0x2005,

        // Misc
        H7Lumi      = 0x1712,
        SmartDevice = 0x1714
    }
}
