using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Devices;
using NZXTSharp.Exceptions;
using NZXTSharp.Params;

// TOTEST
namespace NZXTSharp.Effects {
    public class Fading : IEffect {
        private int _EffectByte = 0x01;
        private string _EffectName = "Fading";
        public readonly List<string> CompatibleWith = new List<string>() { "HuePlus" };

        public Color[] Colors;
        private _03Param Param1 = new _03Param();
        private CISS Param2;
        private Channel _Channel;
        private int _Speed = 2;

        public int EffectByte { get; }
        public Channel Channel { get; set; }
        public string EffectName { get; }

        public Fading(Color[] Colors) {
            this.Colors = Colors;
            ValidateParams();
        }

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

        public bool IsCompatibleWith(string Device) {
            return CompatibleWith.Contains(Device) ? true : false;
        }

        public List<byte[]> BuildBytes(Channel Channel) {
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
