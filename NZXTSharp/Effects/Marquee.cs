using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;
using NZXTSharp.Params;

// TOTEST
namespace NZXTSharp.Effects {
    public class Marquee : IEffect {
        private int _EffectByte = 0x03;
        private string _EffectName = "Marquee";
        public readonly List<string> CompatibleWith = new List<string>() { "HuePlus" };

        private HexColor _Color;
        private Direction Param1;
        private LSS Param2;
        private Channel _Channel;

        #region Properties
        public int EffectByte { get; }
        public Channel Channel { get; set; }
        public string EffectName { get; }
        #endregion

        public Marquee(HexColor Color, Direction Direction, LSS LSS) {
            this._Color = Color;
            this.Param1 = Direction;
            this.Param2 = LSS;
        }

        public bool IsCompatibleWith(string Device) {
            return CompatibleWith.Contains(Device) ? true : false;
        }

        public List<byte[]> BuildBytes() {
            byte[] SettingsBytes = new byte[] { 0x4b, (byte)Channel, 0x03, Param1, Param2 };
            byte[] final = SettingsBytes.ConcatenateByteArr(_Color.Expanded());
            return new List<byte[]>() { final };
        }
    }
}
