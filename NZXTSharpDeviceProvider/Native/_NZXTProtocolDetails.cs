using System;
using System.Runtime.InteropServices;

namespace RGB.NET.Devices.NZXT {

    [StructLayout(LayoutKind.Sequential)]
    internal struct _NZXTProtocolDetails {
        /// <summary>
        /// CUE-SDK: null - terminated string containing version of SDK(like “1.0.0.1”). Always contains valid value even if there was no CUE found
        /// </summary>
        internal IntPtr sdkVersion;

        /// <summary>
        /// CUE-SDK: integer number that specifies version of protocol that is implemented by current SDK.
        /// Numbering starts from 1. Always contains valid value even if there was no CUE found
        /// </summary>
        internal int sdkProtocolVersion;

        /// <summary>
        /// CUE-SDK: boolean value that specifies if there were breaking changes between version of protocol implemented by server and client
        /// </summary>
        internal byte breakingChanges;
    };
}
