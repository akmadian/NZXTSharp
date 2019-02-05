using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;
using NZXTSharp.Exceptions;

namespace NZXTSharp
{

    /// <summary>
    /// Represents an RGB Fading effect.
    /// </summary>
    public class Fading : IEffect {
        private int _EffectByte = 0x01;
        private string _EffectName = "Fading";

        /// <inheritdoc/>
        public readonly List<NZXTDeviceType> CompatibleWith = new List<NZXTDeviceType>() {
            NZXTDeviceType.HuePlus,
            NZXTDeviceType.KrakenX
        };

        /// <summary>
        /// The array of colors used by the effect.
        /// </summary>
        public Color[] Colors;
        private CISS Param2;
        private IChannel _Channel;
        private int _Speed = 2;

        /// <inheritdoc/>
        public int EffectByte { get; }

        /// <inheritdoc/>
        public IChannel Channel { get; set; }

        /// <inheritdoc/>
        public string EffectName { get; }

        /// <summary>
        /// Constructs a <see cref="Fading"/> effect.
        /// </summary>
        /// <param name="Colors">The <see cref="Color"/>s to display.</param>
        /// /// <param name="speed">Speed values must be 0-4 (inclusive). 0 being slowest, 2 being normal, and 4 being fastest. Defaults to 2.</param>
        public Fading(Color[] Colors, int speed = 2) {
            this.Colors = Colors;
            this._Speed = speed;
            ValidateParams();
        }

        private void ValidateParams() {
            if (this.Colors.Length > 15) {
                throw new TooManyColorsProvidedException();
            }
        }

        /// <inheritdoc/>
        public bool IsCompatibleWith(NZXTDeviceType Type)
        {
            return CompatibleWith.Contains(Type) ? true : false;
        }

        /// <inheritdoc/>
        public List<byte[]> BuildBytes(NZXTDeviceType Type, IChannel Channel_) {
            switch (Type)
            {
                case NZXTDeviceType.HuePlus:
                    List<byte[]> outList = new List<byte[]>();
                    for (int colorIndex = 0; colorIndex < Colors.Length; colorIndex++)
                    {
                        byte[] SettingsBytes = new byte[] { 0x4b, (byte)Channel_.ChannelByte, 0x01, 0x03, new CISS(colorIndex, this._Speed) };
                        byte[] final = SettingsBytes.ConcatenateByteArr(Channel_.BuildColorBytes(Colors[colorIndex]));
                        outList.Add(final);
                    }
                    return outList;
                case NZXTDeviceType.KrakenX:
                    List<byte[]> KrakenXOutList = new List<byte[]>();
                    for (int colorIndex = 0; colorIndex < Colors.Length; colorIndex++)
                    {
                        byte[] SettingsBytes = new byte[] { 0x02, 0x4c, (byte)Channel_.ChannelByte, 0x01, new CISS(colorIndex, this._Speed) };
                        byte[] KrakenXfinal = SettingsBytes.ConcatenateByteArr(Channel_.BuildColorBytes(Colors[colorIndex]));
                        KrakenXOutList.Add(KrakenXfinal);
                    }
                    return KrakenXOutList;
                default:
                    return null;
            }
        }
    }
}
