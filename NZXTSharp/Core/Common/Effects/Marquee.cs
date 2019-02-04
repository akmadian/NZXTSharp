using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;
using NZXTSharp.HuePlus;

namespace NZXTSharp
{

    /// <summary>
    /// Represents an RGB marquee effect.
    /// </summary>
    public class Marquee : IEffect {

        private int _EffectByte = 0x03;
        private string _EffectName = "Marquee";

        /// <inheritdoc/>
        public readonly List<NZXTDeviceType> CompatibleWith = new List<NZXTDeviceType>() {
            NZXTDeviceType.HuePlus,
            NZXTDeviceType.KrakenX
        };

        private Color _Color;
        private Direction Param1;
        private LSS Param2;
        private IChannel _Channel;

        #region Properties
        /// <inheritdoc/>
        public int EffectByte { get; }

        /// <inheritdoc/>
        public IChannel Channel { get; set; }

        /// <inheritdoc/>
        public string EffectName { get; }
        #endregion

        /// <summary>
        /// Constructs a <see cref="Marquee"/> effect.
        /// </summary>
        /// <param name="Color">The <see cref="Color"/> of the effect.</param>
        /// <param name="Direction">The <see cref="Direction"/> of the effect.</param>
        /// <param name="LSS">The <see cref="LSS"/> param to apply.</param>
        public Marquee(Color Color, Direction Direction, LSS LSS) {
            this._Color = Color;
            this.Param1 = Direction;
            this.Param2 = LSS;
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
                    byte[] SettingsBytes = new byte[] { 0x4b, (byte)Channel.ChannelByte, 0x03, Param1, Param2 };
                    byte[] final = SettingsBytes.ConcatenateByteArr(Channel.State == false ? new Color().AllOff() : Channel.BuildColorBytes(_Color));
                    outList.Add(final);

                    return outList;
                case NZXTDeviceType.KrakenX:
                // TODO
                default:
                    return null;
            }
        }
    }
}
