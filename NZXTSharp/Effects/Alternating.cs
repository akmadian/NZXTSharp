using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;
using NZXTSharp.Exceptions;
using NZXTSharp.Params;

// TOTEST
namespace NZXTSharp.Effects {
    class Alternating : IEffect {
        private int _EffectByte = 0x05;
        private string _EffectName = "Alternating";
        public readonly List<string> CompatibleWith = new List<string>() { "HuePlus" };

        public HexColor[] Colors;
        private Channel _Channel;
        private Direction _Param1 = new Direction(true, false);
        private CISS _Param2;
        private int speed = 2;

        public int EffectByte { get; }
        public Channel Channel { get; set; }
        public string EffectName { get; }
        
        public Alternating(HexColor[] Colors) {
            this.Colors = Colors;
            ValidateParams();
        }

        // Speed Optional, Direction Provided
        public Alternating(HexColor[] Colors, Direction Direction, int speed = 2) {
            this.Colors = Colors;
            this._Param1 = Direction;
            this.speed = speed;
            ValidateParams();
        }

        public Alternating(HexColor Color1, HexColor Color2, Direction Direction, int speed) {
            this.Colors = new HexColor[] { Color1, Color2 };
            this._Param1 = Direction;
            this.speed = speed;
        }

        private void ValidateParams() {
            if (Colors.Length > 2) {
                throw new TooManyColorsProvidedException();
            }
        }

        public bool IsCompatibleWith(string Device) {
            return CompatibleWith.Contains(Device) ? true : false;
        }

        public List<byte[]> BuildBytes() {
            List<byte[]> outList = new List<byte[]>();
            for (int colorIndex = 0; colorIndex < Colors.Length; colorIndex++) {
                byte[] SettingsBytes = new byte[] { 0x4b, (byte)Channel, 0x05, _Param1, new CISS(colorIndex, this.speed) };
                byte[] final = SettingsBytes.ConcatenateByteArr(Colors[colorIndex].Expanded());
                outList.Add(final);
            }
            return outList;
        }
    }
}
