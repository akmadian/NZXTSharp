using System;
using System.Collections.Generic;
using System.Text;

using RGB.NET.Core;

namespace RGB.NET.Devices.NZXT {
    /// <inheritdoc />
    /// <summary>
    /// Represents a generic information for a Corsair-<see cref="T:RGB.NET.Core.IRGBDevice" />.
    /// </summary>
    public class NZXTRGBDeviceInfo : IRGBDeviceInfo {
        #region Properties & Fields

        private int _ChannelByte;

        private int _NumLeds;

        /// <summary>
        /// Gets the corsair specific device type.
        /// </summary>
        public string DeviceName { get; }

        public int Channel => _ChannelByte;

        public int NumLeds { get; }

        /// <summary>
        /// Gets the corsair specific device type.
        /// </summary>
        public NZXTDeviceType NZXTDeviceType { get; }

        /// <summary>
        /// Gets the index of the <see cref="CorsairRGBDevice{TDeviceInfo}"/>.
        /// </summary>
        public int NZXTDeviceIndex { get; }

        /// <inheritdoc />
        public RGBDeviceType DeviceType { get; }

        /// <inheritdoc />
        public string Manufacturer => "NZXT";

        /// <inheritdoc />
        public string Model { get; }

        /// <inheritdoc />
        public Uri Image { get; set; }

        /// <inheritdoc />
        public bool SupportsSyncBack => false;

        /// <inheritdoc />
        public RGBDeviceLighting Lighting => RGBDeviceLighting.Key;

        #endregion

        #region Constructors

        /// <summary>
        /// Internal constructor of managed <see cref="CorsairRGBDeviceInfo"/>.
        /// </summary>
        /// <param name="deviceIndex">The index of the <see cref="CorsairRGBDevice{TDeviceInfo}"/>.</param>
        /// <param name="deviceType">The type of the <see cref="IRGBDevice"/>.</param>
        /// <param name="nativeInfo">The native <see cref="_CorsairDeviceInfo" />-struct</param>
        internal NZXTRGBDeviceInfo(int deviceIndex, RGBDeviceType deviceType, _NZXTDeviceInfo nativeInfo) {
            this.DeviceName = nativeInfo.DeviceName;
            this.NZXTDeviceIndex = deviceIndex;
            this.NZXTDeviceType = nativeInfo.type;
            this.Model = nativeInfo.Model;
            this._ChannelByte = nativeInfo.ChannelByte;
            this._NumLeds = nativeInfo.ledsCount;

            switch(nativeInfo.type) {
                case NZXTDeviceType.Fan:
                    this.DeviceType = RGBDeviceType.Fan;
                    break;
                case NZXTDeviceType.Strip:
                    this.DeviceType = RGBDeviceType.LedStripe;
                    break;
                case NZXTDeviceType.Unknown:
                    this.DeviceType = RGBDeviceType.Unknown;
                    break;
                case NZXTDeviceType.Cooler:
                    this.DeviceType = RGBDeviceType.Cooler;
                    break;
            }
        }

        #endregion
    }
}
