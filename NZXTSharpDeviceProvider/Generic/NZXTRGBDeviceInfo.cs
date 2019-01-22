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

        /// <summary>
        /// Gets the corsair specific device type.
        /// </summary>
        public string DeviceName { get; }

        /// <summary>
        /// Gets the corsair specific device type.
        /// </summary>
        public NZXTDeviceType CorsairDeviceType { get; }

        /// <summary>
        /// Gets the index of the <see cref="CorsairRGBDevice{TDeviceInfo}"/>.
        /// </summary>
        public int CorsairDeviceIndex { get; }

        /// <inheritdoc />
        public RGBDeviceType DeviceType { get; }

        /// <inheritdoc />
        public string Manufacturer => "Corsair";

        /// <inheritdoc />
        public string Model { get; }

        /// <inheritdoc />
        public Uri Image { get; set; }

        /// <inheritdoc />
        public bool SupportsSyncBack => true;

        /// <inheritdoc />
        public RGBDeviceLighting Lighting => RGBDeviceLighting.Key;

        /// <summary>
        /// Gets a flag that describes device capabilities. (<see cref="CorsairDeviceCaps" />)
        /// </summary>
        public CorsairDeviceCaps CapsMask { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Internal constructor of managed <see cref="CorsairRGBDeviceInfo"/>.
        /// </summary>
        /// <param name="deviceIndex">The index of the <see cref="CorsairRGBDevice{TDeviceInfo}"/>.</param>
        /// <param name="deviceType">The type of the <see cref="IRGBDevice"/>.</param>
        /// <param name="nativeInfo">The native <see cref="_CorsairDeviceInfo" />-struct</param>
        internal CorsairRGBDeviceInfo(int deviceIndex, RGBDeviceType deviceType, _CorsairDeviceInfo nativeInfo) {
            this.CorsairDeviceIndex = deviceIndex;
            this.DeviceType = deviceType;
            this.CorsairDeviceType = nativeInfo.type;
            this.Model = nativeInfo.model == IntPtr.Zero ? null : Marshal.PtrToStringAnsi(nativeInfo.model);
            this.CapsMask = (CorsairDeviceCaps)nativeInfo.capsMask;
        }

        #endregion
    }
}
