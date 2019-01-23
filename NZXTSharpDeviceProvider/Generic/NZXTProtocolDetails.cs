using System;
using System.Collections.Generic;
using System.Text;

namespace RGB.NET.Devices.NZXT {
    public class NZXTProtocolDetails {

        #region Properties & Fields

        /// <summary>
        /// String containing version of SDK(like "1.0.0.1").
        /// Always contains valid value even if there was no CUE found.
        /// </summary>
        public string SdkVersion { get; }

        /// <summary>
        /// Integer that specifies version of protocol that is implemented by current SDK.
        /// Numbering starts from 1.
        /// Always contains valid value even if there was no CUE found.
        /// </summary>
        public int SdkProtocolVersion { get; }

        /// <summary>
        /// Boolean that specifies if there were breaking changes between version of protocol implemented by server and client.
        /// </summary>
        public bool BreakingChanges { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Internal constructor of managed CorsairProtocolDetails.
        /// </summary>
        /// <param name="nativeDetails">The native CorsairProtocolDetails-struct</param>
        internal NZXTProtocolDetails(_NZXTProtocolDetails nativeDetails) {
            this.SdkProtocolVersion = nativeDetails.sdkProtocolVersion;
            this.BreakingChanges = nativeDetails.breakingChanges != 0;
        }
        #endregion
    }
}
