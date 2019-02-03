using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Devices;
using NZXTSharp.Exceptions;

namespace NZXTSharp
{

    /// <summary>
    /// Represents an RGB fixed effect.
    /// </summary>
    public class Fixed : IEffect {
        private int _EffectByte = 0x00;
        private string _EffectName = "Fixed";

        /// <inheritdoc/>
        public readonly List<NZXTDeviceType> CompatibleWith = new List<NZXTDeviceType>() { NZXTDeviceType.HuePlus };

        private Color _Color;
        private IChannel _Channel;

        private byte[] CustomBytes;
        private bool IsCustom;

        /// <inheritdoc/>
        public int EffectByte { get; }

        /// <inheritdoc/>
        public IChannel Channel { get; set; }

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
        public Fixed(IChannel Channel, Color color)
        {
            this._Channel = Channel;
            this._Color = color;
        }

        /// <summary>
        /// Constructs an RGB Fixed effect from custom LED colors.
        /// </summary>
        /// <param name="Colors">A byte[] of RGB color values in G, R, B format. 
        /// Length must be less than 120 and greater than 0. 
        /// RGB values must be 0-255 (inclusive).</param>
        public Fixed(byte[] Colors)
        {
            if (Colors.Length > 120 || Colors.Length < 0) 
                throw new InvalidParamException("Invalid RGB color array size. Must have at least one element, and fewer than 120 elements."); 

            foreach (byte color in Colors)
            {
                int value = Convert.ToInt32(color);
                if (value > 255 || value < 0)
                {
                    throw new InvalidParamException("Invalid RGB color found in array. All RGB color values must be 0-255 (inclusive).");
                }
            }
            this.CustomBytes = Colors.PadColorArr();
            this.IsCustom = true;
        }

        /// <inheritdoc/>
        public bool IsCompatibleWith(NZXTDeviceType Type)
        {
            return CompatibleWith.Contains(Type) ? true : false;
        }

        /// <inheritdoc/>
        public List<byte[]> BuildBytes(IChannel Channel) {
            this._Channel = Channel;
            byte[] SettingsBytes = new byte[] { 0x4b, (byte)_Channel.ChannelByte, 0x00, 0x02, 0x03 };
            byte[] final;
            if (IsCustom)
            {
                final = SettingsBytes.ConcatenateByteArr(_Channel.BuildColorBytes(CustomBytes));
            } else
            {
                final = SettingsBytes.ConcatenateByteArr(_Channel.BuildColorBytes(_Color));
            }
            return new List<byte[]>() { final };
        }
    }
}
