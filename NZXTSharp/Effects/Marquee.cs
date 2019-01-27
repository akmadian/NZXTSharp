using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;
using NZXTSharp.Params;
using NZXTSharp.Devices;

// TOTEST
namespace NZXTSharp.Effects {

    /// <summary>
    /// Represents an RGB marquee effect.
    /// </summary>
    public class Marquee : IEffect {

        private int _EffectByte = 0x03;
        private string _EffectName = "Marquee";

        /// <inheritdoc/>
        public readonly List<NZXTDeviceType> CompatibleWith = new List<NZXTDeviceType>() { NZXTDeviceType.HuePlus };

        private Color _Color;
        private Direction Param1;
        private LSS Param2;
        private Channel _Channel;
        private IHueDevice Parent;

        #region Properties
        /// <inheritdoc/>
        public int EffectByte { get; }

        /// <inheritdoc/>
        public Channel Channel { get; set; }

        /// <inheritdoc/>
        public string EffectName { get; }
        #endregion

        /// <summary>
        /// Constructs a <see cref="Marquee"/> effect.
        /// </summary>
        /// <param name="Parent">The <see cref="IHueDevice"/> that the effect is being applied to.</param>
        /// <param name="Color">The <see cref="Color"/> of the effect.</param>
        /// <param name="Direction">The <see cref="Direction"/> of the effect.</param>
        /// <param name="LSS">The <see cref="LSS"/> param to apply.</param>
        public Marquee(IHueDevice Parent, Color Color, Direction Direction, LSS LSS) {
            this.Parent = Parent;
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
        public List<byte[]> BuildBytes(Channel Channel) {
            List<byte[]> outList = new List<byte[]>();
            byte[] SettingsBytes = new byte[] { 0x4b, (byte)Channel, 0x03, Param1, Param2 };
            byte[] final = SettingsBytes.ConcatenateByteArr(Channel.State == false ? new Color().AllOff() : Channel.BuildColorBytes(_Color));
            outList.Add(final);

            return outList;
        }
    }
}
