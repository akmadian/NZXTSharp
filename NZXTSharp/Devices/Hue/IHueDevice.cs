using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Effects;

namespace NZXTSharp.Devices {

    /// <summary>
    /// Represents a generic NZXT Hue device.
    /// </summary>
    public interface IHueDevice : INZXTDevice {

        /// <summary>
        /// Applies a given <see cref="IEffect"/> to the given <see cref="Channel"/>.
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="effect"></param>
        /// <param name="ApplyToChannel"></param>
        void ApplyEffect(Channel channel, IEffect effect, bool ApplyToChannel = true);

        /// <summary>
        /// Sends a given <paramref name="Buffer"/> to the <see cref="IHueDevice"/>'s <see cref="NZXTSharp.COM.ICOMController"/>.
        /// </summary>
        /// <param name="Buffer">The buffer to send.</param>
        void ApplyCustom(byte[] Buffer);

        /// <summary>
        /// Updates the <see cref="ChannelInfo"/> object owned by the given <see cref="Channel"/>.
        /// </summary>
        /// <param name="Channel"></param>
        void UpdateChannelInfo(Channel Channel);

        /// <summary>
        /// Represents both channels owned by a Hue device.
        /// </summary>
        Channel Both { get; }

        /// <summary>
        /// Represents Channel 1 of a Hue device.
        /// </summary>
        Channel Channel1 { get; }

        /// <summary>
        /// Represents Channel 2 of a hue device.
        /// </summary>
        Channel Channel2 { get; }

        /// <summary>
        /// A list of all <see cref="Channel"/>s owned by a hue device.
        /// </summary>
        List<Channel> Channels { get; }
    }
}
