using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp
{
    /// <summary>
    /// Defines filters for getting loaded devices.
    /// Ex. DeviceLoadFilter.Coolers will return ONLY coolers.
    /// </summary>
    public enum DeviceLoadFilter
    {
        /// <summary>
        /// All Devices
        /// </summary>
        All = 0,

        /// <summary>
        /// Any Cooler
        /// </summary>
        Coolers = 1,

        /// <summary>
        /// Any Lighting Controller
        /// </summary>
        LightingControllers = 2,

        /// <summary>
        /// Any Fan Controller
        /// </summary>
        FanControllers = 3,

        /// <summary>
        /// Any Kraken Device
        /// </summary>
        Kraken = 4,

        /// <summary>
        /// Any KrakenM Device
        /// </summary>
        KrakenM = 5,

        /// <summary>
        /// Any KrakenX Device
        /// </summary>
        KrakenX = 6,

        /// <summary>
        /// Any Grid Device
        /// </summary>
        Grid = 7,

        /// <summary>
        /// Any Gridv3 Device
        /// </summary>
        Gridv3 = 8,

        /// <summary>
        /// Any Hue Device
        /// </summary>
        Hue = 9,

        /// <summary>
        /// Any Hue+ Device
        /// </summary>
        HuePlus = 10,

        /// <summary>
        /// Any Hue2 Device
        /// </summary>
        Hue2 = 11,

        /// <summary>
        /// Any Hue Ambient Devices
        /// </summary>
        HueAmbient = 12,
    }
}
