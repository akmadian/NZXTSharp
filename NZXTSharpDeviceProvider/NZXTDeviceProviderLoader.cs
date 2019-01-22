using System;
using System.Collections.Generic;
using System.Text;

using RGB.NET.Core;

namespace RGB.NET.Devices.NZXT {
    class NZXTDeviceProviderLoader : IRGBDeviceProviderLoader {
        #region Properties & Fields

        /// <inheritdoc />
        public bool RequiresInitialization => true;

        #endregion

        #region Methods

        /// <inheritdoc />
        public IRGBDeviceProvider GetDeviceProvider() => NZXTDeviceProvider.Instance;

        #endregion
    }
}
