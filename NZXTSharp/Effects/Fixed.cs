using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Devices;
using NZXTSharp.Params;

namespace NZXTSharp.Effects {

    /// <summary>
    /// Represents an RGB fixed effect.
    /// </summary>
    public class Fixed : IEffect {
        private int _EffectByte = 0x00;
        private string _EffectName = "Fixed";

        /// <inheritdoc/>
        public readonly List<NZXTDeviceType> CompatibleWith = new List<NZXTDeviceType>() { NZXTDeviceType.HuePlus };

        private Color _Color;
        private _03Param _Param1 = new _03Param();
        private _02Param _Param2 = new _02Param();
        private Channel _Channel;

        /// <inheritdoc/>
        public int EffectByte { get; }

        /// <inheritdoc/>
        public Channel Channel { get; set; }

        /// <inheritdoc/>
        public string EffectName { get; }
        
        /// <summary>
        /// Constructs an RGB Fixed effect.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> to display.</param>
        public Fixed(Color color) {
            this._Color = color;
        }

        /// <summary>
        /// Constructs an RGB Fixed effect.
        /// </summary>
        /// <param name="Channel">The <see cref="Channel"/> the effect will be applied to.</param>
        /// <param name="color">The <see cref="Color"/> to display.</param>
        public Fixed(Channel Channel, Color color)
        {
            this._Channel = Channel;
            this._Color = color;
        }

        /// <inheritdoc/>
        public bool IsCompatibleWith(NZXTDeviceType Type)
        {
            return CompatibleWith.Contains(Type) ? true : false;
        }

        /// <inheritdoc/>
        public List<byte[]> BuildBytes(Channel Channel) {
            this._Channel = Channel;
            byte[] SettingsBytes = new byte[] { 0x4b, (byte)_Channel, 0x00, 0x02, 0x03 };
            byte[] final = SettingsBytes.ConcatenateByteArr(_Channel.BuildColorBytes(_Color));
            return new List<byte[]>() { final };
        }
    }
}
