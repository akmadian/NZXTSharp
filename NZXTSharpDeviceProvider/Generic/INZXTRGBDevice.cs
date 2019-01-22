using RGB.NET.Core;

namespace RGB.NET.Devices.NZXT {
    /// <summary>
    /// Represents an NZXT RGB-device.
    /// </summary>
    internal interface INZXTRGBDevice : IRGBDevice {
        void Initialize(NZXTUpdateQueue updateQueue);
    }
}
