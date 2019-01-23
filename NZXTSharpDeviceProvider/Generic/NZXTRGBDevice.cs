using System;
using System.Collections.Generic;
using System.Text;

using RGB.NET.Core;

namespace RGB.NET.Devices.NZXT {
    /// <inheritdoc cref="AbstractRGBDevice{TDeviceInfo}" />
    /// <inheritdoc cref="INZXTRGBDevice" />
    /// <summary>
    /// </summary>
    public abstract class NZXTRGBDevice<TDeviceInfo> : AbstractRGBDevice<TDeviceInfo>, INZXTRGBDevice
        where TDeviceInfo : NZXTRGBDeviceInfo {
        #region Properties & Fields

        /// <inheritdoc />
        /// <summary>
        /// Gets information about the <see cref="T:RGB.NET.Devices.NZXT.NZXTRGBDevice" />.
        /// </summary>
        public override TDeviceInfo DeviceInfo { get; }

        /// <summary>
        /// Gets a dictionary containing all <see cref="Led"/> of the <see cref="NZXTRGBDevice{TDeviceInfo}"/>.
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        protected Dictionary<NZXTLedId, Led> InternalLedMapping { get; } = new Dictionary<NZXTLedId, Led>();

        /// <summary>
        /// Gets or sets the update queue performing updates for this device.
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        protected NZXTUpdateQueue UpdateQueue { get; set; }

        #endregion

        #region Indexer

        /// <summary>
        /// Gets the <see cref="Led"/> with the specified <see cref="NZXTLedId"/>.
        /// </summary>
        /// <param name="ledId">The <see cref="NXZTLedId"/> of the <see cref="Led"/> to get.</param>
        /// <returns>The <see cref="Led"/> with the specified <see cref="NZXTLedId"/> or null if no <see cref="Led"/> is found.</returns>
        // ReSharper disable once MemberCanBePrivate.Global
        public Led this[NZXTLedId ledId] => InternalLedMapping.TryGetValue(ledId, out Led led) ? led : null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NZXTRGBDevice{TDeviceInfo}"/> class.
        /// </summary>
        /// <param name="info">The generic information provided by CUE for the device.</param>
        protected NZXTRGBDevice(TDeviceInfo info) {
            this.DeviceInfo = info;
        }

        #endregion

        #region Methods

        // TODO
        /// <summary>
        /// Initializes the device.
        /// </summary>
        public void Initialize(NZXTUpdateQueue updateQueue) {
            UpdateQueue = updateQueue;

            InitializeLayout();

            foreach (Led led in LedMapping.Values) {
                NZXTLedId ledId = (NZXTLedId)led.CustomData;
                if (ledId != NZXTLedId.Invalid)
                    InternalLedMapping.Add(ledId, led);
            }

            if (Size == Size.Invalid) {
                //ctangle ledRectangle = new Rectangle(this.Select(x => x.LedRectangle));
                //Size = ledRectangle.Size + new Size(ledRectangle.Location.X, ledRectangle.Location.Y);
            }
        }

        /// <summary>
        /// Initializes the <see cref="Led"/> and <see cref="Size"/> of the device.
        /// </summary>
        protected abstract void InitializeLayout();

        // TODO
        /// <inheritdoc />
        // protected override void UpdateLeds(IEnumerable<Led> ledsToUpdate)
            //=> UpdateQueue.SetData();

        #endregion
    }
}
