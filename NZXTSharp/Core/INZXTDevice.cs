using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp
{
    /// <summary>
    /// Represents an NZXT device.
    /// </summary>
    public interface INZXTDevice
    {
        /// <summary>
        /// The <see cref="NZXTDeviceType"/> of the <see cref="INZXTDevice"/>.
        /// </summary>
        NZXTDeviceType Type { get; }

        /// <summary>
        /// A unique device ID.
        /// </summary>
        int ID { get; }

        /// <summary>
        /// Applies an <see cref="IEffect"/> to the <see cref="INZXTDevice"/>.
        /// </summary>
        /// <param name="Effect">The <see cref="IEffect"/> to apply.</param>
        void ApplyEffect(IEffect Effect);

        /// <summary>
        /// Disposes of the <see cref="INZXTDevice"/> instance.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Attempts to reconnect to the <see cref="INZXTDevice"/>.
        /// </summary>
        void Reconnect();
    }
}
