using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Devices;
using NZXTSharp.Params;

// TOTEST
namespace NZXTSharp.Effects {
    public class SpectrumWave : IEffect {
        private int _EffectByte = 0x02;
        private string _EffectName = "SpectrumWave";
        public readonly List<string> CompatibleWith = new List<string>() { "HuePlus" };

        private int speed;
        private Direction Param1;
        private CISS Param2;
        private Channel _Channel;

        public int EffectByte { get; }
        public Channel Channel { get; set; }
        public string EffectName { get; }

        public SpectrumWave(Direction Direction, int speed = 2) {
            this.Param1 = Direction;
            this.Param2 = new CISS(0, speed);
        }

        public bool IsCompatibleWith(string Device) {
            return CompatibleWith.Contains(Device) ? true : false;
        }

        public List<byte[]> BuildBytes() {
            byte[] SettingsBytes = new byte[] { 0x4b, (byte)Channel, 0x02, Param1, Param2 };
            byte[] final = SettingsBytes.ConcatenateByteArr(new HexColor(0, 0, 255).Expanded());
            return new List<byte[]>() { final };
        }
    }
}
