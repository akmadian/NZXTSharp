using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Devices;
using NZXTSharp.Params;
using NZXTSharp.Exceptions;

// TOTEST
namespace NZXTSharp.Effects {
    public class CoveringMarquee : IEffect {

        private int _EffectByte = 0x04;
        private string _EffectName = "CoveringMarquee";
        public readonly List<string> CompatibleWith = new List<string>() { "HuePlus" };

        private Color[] _Colors;
        private Direction Param1;
        private CISS Param2;
        private Channel _Channel;
        private IHueDevice Parent;
        private int _Speed;
        
        public int EffectByte { get; }
        public Channel Channel { get; set; }
        public string EffectName { get; }

        public CoveringMarquee(IHueDevice Parent, Color Color1, Color Color2, Direction Direction, int speed = 2) {
            this.Parent = Parent;
            this._Colors = new Color[] { Color1, Color2 };
            this.Param1 = Direction;
            this._Speed = speed;
            ValidateParams();
        }

        public CoveringMarquee(IHueDevice Parent, Color[] Colors, Direction Direction, int speed = 2) {
            this.Parent = Parent;
            this._Colors = Colors;
            this.Param1 = Direction;
            this._Speed = speed;
            ValidateParams();
        }

        private void ValidateParams() {
            if (this._Colors.Length > 15) {
                throw new TooManyColorsProvidedException();
            }

            if (this._Speed > 4 || this._Speed < 0) {
                throw new InvalidEffectSpeedException();
            }
        }

        public bool IsCompatibleWith(string Device) {
            return CompatibleWith.Contains(Device) ? true : false;
        }

        public List<byte[]> BuildBytes(Channel Channel) {
            List<byte[]> outList = new List<byte[]>();
            foreach (Channel channel in Parent.Channels) {
                for (int colorIndex = 0; colorIndex < _Colors.Length; colorIndex++) {
                    byte[] SettingsBytes = new byte[] { 0x4b, (byte)channel, 0x04, Param1, new CISS(colorIndex, this._Speed) };
                    byte[] final = SettingsBytes.ConcatenateByteArr(Channel.BuildColorBytes(_Colors[colorIndex]));
                    outList.Add(final);
                }
            }

            return outList;
        }
    }
}
