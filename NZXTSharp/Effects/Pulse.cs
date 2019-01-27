using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Devices;
using NZXTSharp.Params;
using NZXTSharp.Exceptions;

// TOTEST
namespace NZXTSharp.Effects {
    public class Pulse : IEffect {
        #pragma warning disable IDE0044 // Add readonly modifier
        private int _EffectByte = 0x06;
        private string _EffectName = "Pulse";
        public readonly List<string> CompatibleWith = new List<string>() { "HuePlus" };

        public Color[] Colors;
        private Channel _Channel;
        private CISS _Param2;
        private int speed = 2;
        #pragma warning restore IDE0044 // Add readonly modifier

        public int EffectByte { get; }
        public Channel Channel { get; set; }
        public string EffectName { get; }

        public Pulse(Color[] Colors) {
            this.Colors = Colors;
            ValidateParams();
        }

        public Pulse(Color[] Colors, int speed) {
            this.Colors = Colors;
            this.speed = speed;
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
                byte[] SettingsBytes = new byte[] { 0x4b, (byte)Channel, 0x06, 0x03, new CISS(colorIndex, this.speed) };
                byte[] final = SettingsBytes.ConcatenateByteArr(Channel.BuildColorBytes(Colors[colorIndex]));
                outList.Add(final);
            }
            return outList;
        }
    }
}
