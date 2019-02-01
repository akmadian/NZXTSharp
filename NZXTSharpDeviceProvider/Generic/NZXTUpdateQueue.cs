using System;
using System.Collections.Generic;
using System.Text;

using RGB.NET.Core;

namespace RGB.NET.Devices.NZXT {
    public class NZXTUpdateQueue : UpdateQueue {
        #region Properties & Fields

        private int _deviceIndex;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CorsairDeviceUpdateQueue"/> class.
        /// </summary>
        /// <param name="updateTrigger">The update trigger used by this queue.</param>
        /// <param name="deviceIndex">The index used to identify the device.</param>
        public NZXTUpdateQueue(IDeviceUpdateTrigger updateTrigger, int deviceIndex)
            : base(updateTrigger) {
            this._deviceIndex = deviceIndex;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void Update(Dictionary<object, Color> dataSet) {
            foreach (KeyValuePair<object, Color> data in dataSet) {
                _NZXTLedColor color = new _NZXTLedColor
                (
                    data.Value.R,
                    data.Value.G,
                    data.Value.B
                );
                
            }
        }

        #endregion
    }
}
