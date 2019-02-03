using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Devices;
using NZXTSharp.Params;

namespace NZXTSharp
{

    /// <summary>
    /// Represents an RGB Spectrum Wave effect.
    /// </summary>
    public class SpectrumWave : IEffect {
        private int _EffectByte = 0x02;
        private string _EffectName = "SpectrumWave";

        /// <inheritdoc/>
        public readonly List<NZXTDeviceType> CompatibleWith = new List<NZXTDeviceType>() { NZXTDeviceType.HuePlus };

        private int speed;
        private Direction Param1;
        private CISS Param2;
        private HuePlusChannel _Channel;

        /// <inheritdoc/>
        public int EffectByte { get; }

        /// <inheritdoc/>
        public HuePlusChannel Channel { get; set; }

        /// <inheritdoc/>
        public string EffectName { get; }

        /// <summary>
        /// Constructs a <see cref="SpectrumWave"/> effect with the given <see cref="Direction"/>.
        /// </summary>
        /// <param name="Direction"></param>
        /// <param name="Speed">Speed values must be 0-4 (inclusive). 0 being slowest, 2 being normal, and 4 being fastest. Defaults to 2.</param>
        public SpectrumWave(Direction Direction, int Speed = 2) {
            this.Param1 = Direction;
            this.Param2 = new CISS(0, Speed);
        }

        /// <inheritdoc/>
        public bool IsCompatibleWith(NZXTDeviceType Type)
        {
            return CompatibleWith.Contains(Type) ? true : false;
        }

        /// <inheritdoc/>
        public List<byte[]> BuildBytes(HuePlusChannel Channel) {
            byte[] SettingsBytes = new byte[] { 0x4b, (byte)Channel, 0x02, Param1, Param2 };
            byte[] final = SettingsBytes.ConcatenateByteArr(Channel.BuildColorBytes(new Color(0, 0, 255)));
            return new List<byte[]>() { final };
        }
    }
}
