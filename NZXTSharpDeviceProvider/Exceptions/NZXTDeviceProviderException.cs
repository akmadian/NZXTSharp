using System;
using System.Collections.Generic;
using System.Text;

namespace RGB.NET.Devices.NZXT {
    class NZXTDeviceProviderException : Exception {

        public NZXTDeviceProviderException() {

        }

        public NZXTDeviceProviderException(string message)
            :base(message) { }
    }
}
