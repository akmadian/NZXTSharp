using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp
{
    /// <summary>
    /// Represents an RGB or Fan channel on an <see cref="INZXTDevice"/>.
    /// </summary>
    public interface IChannel
    {
        /// <summary>
        /// The ChannelByte of the <see cref="IChannel"/>.
        /// </summary>
        int ChannelByte { get; }

        /// <summary>
        /// Whether or not the <see cref="IChannel"/> is on.
        /// </summary>
        bool State { get; }

        /// <summary>
        /// Builds color bytes for an RGB effect for a given <see cref="Color"/>.
        /// </summary>
        /// <param name="Color"></param>
        /// <returns></returns>
        byte[] BuildColorBytes(Color Color);

        /// <summary>
        /// Builds color bytes for an RGB effect for a given custom color set.
        /// </summary>
        /// <param name="Custom"></param>
        /// <returns></returns>
        byte[] BuildColorBytes(byte[] Custom);
    }
}
