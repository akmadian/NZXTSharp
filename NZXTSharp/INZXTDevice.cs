using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp {

    /// <summary>
    /// Represents a generic NZXT device.
    /// </summary>
    public interface INZXTDevice {

        /// <summary>
        /// The <see cref="Devices.NZXTDeviceType"/> of the <see cref="INZXTDevice"/>.
        /// </summary>
        Devices.NZXTDeviceType Type { get; }
        
    }
}
