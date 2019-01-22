using System;

namespace RGB.NET.Devices.NZXT {
    internal class _NZXTDeviceInfo {
        /// <summary>
        /// CUE-SDK: enum describing device type
        /// </summary>
        internal NZXTDeviceType type;

        /// <summary>
        /// CUE-SDK: null - terminated device model(like “K95RGB”)
        /// </summary>
        internal IntPtr model;

        /// <summary>
        /// CUE-SDK: number of controllable LEDs on the device
        /// </summary>
        internal int ledsCount;
    }
}
