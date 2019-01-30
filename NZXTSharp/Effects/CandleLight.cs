using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Devices;
using NZXTSharp.Params;

namespace NZXTSharp.Effects {

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
        private Channel _Channel;
        private _03Param _Param1;
        private _02Param _Param2;

        /// <inheritdoc/>
        public int EffectByte { get; }

        /// <inheritdoc/>
        public Channel Channel { get; set; }

        /// <inheritdoc/>
        public string EffectName { get; }

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
        public List<byte[]> BuildBytes(Channel Channel) {
            byte[] SettingsBytes = new byte[] { 0x4b, (byte)Channel, 0x09, _Param1, _Param2 };
            byte[] final = SettingsBytes.ConcatenateByteArr(Channel.BuildColorBytes(Color));
            return new List<byte[]>() { final };
        }
    }
}
