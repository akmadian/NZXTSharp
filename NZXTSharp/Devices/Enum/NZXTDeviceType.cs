using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Devices {
    public enum NZXTDeviceType 
    {
        Unknown = 0,
        NotActive = 1,

        // SubDevices
        Fan = 2,
        Strip = 3,

        // Krakens
        Kraken = 4,
        KrakenX = 5,
        KrakenM = 6,
        KrakenM22 = 7,
        KrakenX42 = 8,
        KrakenX52 = 9,
        KrakenX62 = 10,
        KrakenX72 = 11,

        // Hues
        Hue = 12,
        HuePlus = 13,
        Hue2 = 14,
        HueAmbient = 15,

        // N7s
        Motherboard = 16,
        N7 = 17,
        N7_Z390 = 18,

        // Grids
        Grid = 19,
        GridPlus = 20,
        GridV2 = 21,
        GridV3 = 22,

        // Misc
        H7Lumi = 23,
        SmartDevice = 24,
    }
}
