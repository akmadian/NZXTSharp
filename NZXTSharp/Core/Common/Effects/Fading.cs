using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Devices;
using NZXTSharp.Exceptions;
using NZXTSharp.Params;

namespace NZXTSharp
{

    /// <summary>
    /// Represents an RGB Fading effect.
    /// </summary>
    public class Fading : IEffect {
        private int _EffectByte = 0x01;
        private string _EffectName = "Fading";

        /// <inheritdoc/>
        public readonly List<NZXTDeviceType> CompatibleWith = new List<NZXTDeviceType>() { NZXTDeviceType.HuePlus };

        /// <summary>
        /// The array of colors used by the effect.
        /// </summary>
        public Color[] Colors;
        private _03Param Param1 = new _03Param();
        private CISS Param2;
        private HuePlusChannel _Channel;
        private int _Speed = 2;

        /// <inheritdoc/>
        public int EffectByte { get; }

        /// <inheritdoc/>
        public HuePlusChannel Channel { get; set; }

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
        public List<byte[]> BuildBytes(HuePlusChannel Channel) {
            List<byte[]> outList = new List<byte[]>();
            for (int colorIndex = 0; colorIndex < Colors.Length; colorIndex++) {
                byte[] SettingsBytes = new byte[] { 0x4b, (byte)Channel, 0x01, Param1, new CISS(colorIndex, this._Speed) };
                byte[] final = SettingsBytes.ConcatenateByteArr(Channel.BuildColorBytes(Colors[colorIndex]));
                outList.Add(final);
            }
            return outList;
        }
    }
}
