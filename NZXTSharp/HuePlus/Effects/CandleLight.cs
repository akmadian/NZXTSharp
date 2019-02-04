using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;

namespace NZXTSharp.HuePlus
{

    /// <summary>
    /// Represents an RGB Candle Light effect.
    /// </summary>
    public class CandleLight : IEffect {
        private int _EffectByte = 0x09;
        private string _EffectName = "CandleLight";

        /// <inheritdoc/>
        public readonly List<NZXTDeviceType> CompatibleWith = new List<NZXTDeviceType>() { NZXTDeviceType.HuePlus };

        /// <inheritdoc/>
        public Color Color;
        private HuePlusChannel _Channel;

        /// <inheritdoc/>
        public int EffectByte { get; }

        /// <inheritdoc/>
        public HuePlusChannel Channel { get; set; }

        /// <inheritdoc/>
        public string EffectName { get; }
        IChannel IEffect.Channel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Constructs a <see cref="CandleLight"/> effect with a given <paramref name="Color"/>.
        /// </summary>
        /// <param name="Color">The <see cref="Color"/> to display.</param>
        public CandleLight(Color Color) {
            this.Color = Color;
        }

        /// <inheritdoc/>
        public bool IsCompatibleWith(NZXTDeviceType Type)
        {
            return CompatibleWith.Contains(Type) ? true : false;
        }

        /// <inheritdoc/>
        public List<byte[]> BuildBytes(IChannel Channel) {
            byte[] SettingsBytes = new byte[] { 0x4b, (byte)Channel.ChannelByte, 0x09, 0x03, 0x02 };
            byte[] final = SettingsBytes.ConcatenateByteArr(Channel.BuildColorBytes(Color));
            return new List<byte[]>() { final };
        }
    }
}
