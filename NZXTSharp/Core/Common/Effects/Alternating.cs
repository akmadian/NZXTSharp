using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;
using NZXTSharp.HuePlus;
using NZXTSharp.KrakenX;
using NZXTSharp.Exceptions;

namespace NZXTSharp
{

    /// <summary>
    /// Represents an RGB alternating effect.
    /// </summary>
    public class Alternating : IEffect {
        #pragma warning disable IDE0044 // Add readonly modifier
        private int _EffectByte = 0x05;
        private string _EffectName = "Alternating";

        /// <inheritdoc/>
        public readonly List<NZXTDeviceType> CompatibleWith = new List<NZXTDeviceType>() {
            NZXTDeviceType.HuePlus,
            NZXTDeviceType.KrakenX
        };

        /// <inheritdoc/>
        public Color[] Colors;
        private IChannel _Channel;
        private Direction _Param1 = new Direction(true, false);
        private CISS _Param2;
        private int speed = 2;
        #pragma warning restore IDE0044 // Add readonly modifier

        /// <inheritdoc/>
        public int EffectByte { get; }

        /// <inheritdoc/>
        public IChannel Channel { get; set; }

        /// <inheritdoc/>
        public string EffectName { get; }

        /// <summary>
        /// Creates an <see cref="Alternating"/> effect with the given <paramref name="Colors"/>.
        /// </summary>
        /// <param name="Colors">The <see cref="Color"/>s to display. Must be of length (2).</param>
        public Alternating(Color[] Colors) {
            this.Colors = Colors;
            ValidateParams();
        }

        /// <summary>
        /// Constructs an <see cref="Alternating"/> effect.
        /// </summary>
        /// <param name="Colors">The <see cref="Color"/> to display.</param>
        /// <param name="Direction">The <see cref="Direction"/> the effect will move in.</param>
        /// <param name="speed">Speed values must be 0-4 (inclusive). 0 being slowest, 2 being normal, and 4 being fastest. Defaults to 2.</param>
        public Alternating(Color[] Colors, Direction Direction, int speed = 2) {
            this.Colors = Colors;
            this._Param1 = Direction;
            this.speed = speed;
            ValidateParams();
        }

        /// <summary>
        /// Constructs an <see cref="Alternating"/> effect.
        /// </summary>
        /// <param name="Color1">The first <see cref="Color"/> to display.</param>
        /// <param name="Color2">The second <see cref="Color"/> to display.</param>
        /// <param name="Direction">The <see cref="Direction"/> the effect will move in.</param>
        /// <param name="speed">Speed values must be 0-4 (inclusive). 0 being slowest, 2 being normal, and 4 being fastest. Defaults to 2.</param>
        public Alternating(Color Color1, Color Color2, Direction Direction, int speed = 2) {
            this.Colors = new Color[] { Color1, Color2 };
            this._Param1 = Direction;
            this.speed = speed;
        }

        private void ValidateParams() {
            if (Colors.Length > 2) {
                throw new TooManyColorsProvidedException();
            }
        }

        /// <inheritdoc/>
        public bool IsCompatibleWith(NZXTDeviceType Type)
        {
            return CompatibleWith.Contains(Type) ? true : false;
        }

        /// <inheritdoc/>
        public List<byte[]> BuildBytes(NZXTDeviceType Type, IChannel Channel) {
            switch (Type)
            {
                case NZXTDeviceType.HuePlus:
                    List<byte[]> outList = new List<byte[]>();
                    for (int colorIndex = 0; colorIndex < Colors.Length; colorIndex++)
                    {
                        byte[] SettingsBytes = new byte[] { 0x4b, (byte)Channel.ChannelByte, 0x05, _Param1, new CISS(colorIndex, this.speed) };
                        byte[] final = SettingsBytes.ConcatenateByteArr(Channel.BuildColorBytes(Colors[colorIndex]));
                        outList.Add(final);
                    }
                    return outList;
                case NZXTDeviceType.KrakenX:
                    DCBWM direction = new DCBWM(Channel.ChannelByte, _Param1.IsForward, _Param1.WithMovement);
                    List<byte[]> KrakenXOutList = new List<byte[]>();
                    for (int colorIndex = 0; colorIndex < Colors.Length; colorIndex++)
                    {
                        byte[] KrakenXSettingsBytes = new byte[] { 0x2, 0x4c, direction, 0x05, new CISS(colorIndex, this.speed) };
                        byte[] final = KrakenXSettingsBytes.ConcatenateByteArr(Channel.BuildColorBytes(Colors[colorIndex]));
                        KrakenXOutList.Add(final);
                    }
                    return KrakenXOutList;
                default:
                    return null;
            }
        }
    }
}
