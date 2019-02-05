using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;
using NZXTSharp.Exceptions;

namespace NZXTSharp.KrakenX
{
    public class KrakenXChannel : IChannel
    {
        private int _ChannelByte;
        private bool[] _Leds;
        private bool _IsActive = true;
        private IEffect _Effect = new Fixed(new Color(255, 255, 255));
        private KrakenX _Parent;

        public int ChannelByte => _ChannelByte;

        public bool State => _IsActive;

        public bool[] Leds { get => _Leds; }
        
        public int NumLeds { get => _Leds.Length; }

        public IEffect Effect { get => _Effect; }


        public KrakenXChannel(int ChannelByte, KrakenX Parent)
        {
            this._ChannelByte = ChannelByte;
            this._Parent = Parent;
            BuildLEDs();
        }

        public void BuildLEDs()
        {
            switch(_ChannelByte)
            {
                case 0x00:
                    this._Leds = new bool[]
                    {
                        true, true, true, true,
                        true, true, true, true
                    };
                    break;
                case 0x01:
                    this._Leds = new bool[] { true };
                    break;
                case 0x02:
                    this._Leds = new bool[]
                    {
                        true, true, true, true,
                        true, true, true, true
                    };
                    break;
                default:
                    throw new InvalidParamException("Invalid ChannelByte given to KrakenXChannel constructor.");
            }
        }

        internal void UpdateEffect(IEffect newOne)
        {
            this._Effect = newOne;
        }

        public void On()
        {
            this._IsActive = true;
            _Parent.ApplyEffect(this, _Effect);
        }

        public void Off()
        {
            this._IsActive = false;
            _Parent.ApplyEffect(this, new Fixed(this, new Color(0, 0, 0)), false);
        }

        public byte[] BuildColorBytes(byte[] _Buffer)
        {
            for (int LedN = 0; LedN < this._Leds.Length; LedN++)
            {
                if (!_Leds[LedN]) // If LED IS NOT active
                {
                    _Buffer[LedN * 3] = 0x00;
                    _Buffer[LedN * 3 + 1] = 0x01;
                    _Buffer[LedN * 3 + 2] = 0x01;
                }
            }
            return _Buffer;
        }

        public byte[] BuildColorBytes(Color Color)
        {
            List<byte> outBytes = new List<byte>();
            if (_IsActive)
            {
                outBytes.Add(Convert.ToByte(Color.G));
                outBytes.Add(Convert.ToByte(Color.R));
                outBytes.Add(Convert.ToByte(Color.B));

                for (int i = 0; i < _Leds.Length; i++)
                {
                    if (!_Leds[i])
                    {
                        outBytes.Add(0x00);
                        outBytes.Add(0x00);
                        outBytes.Add(0x00);
                    }
                    else
                    {
                        outBytes.Add(Convert.ToByte(Color.R));
                        outBytes.Add(Convert.ToByte(Color.G));
                        outBytes.Add(Convert.ToByte(Color.B));
                    }
                }

                int numToPad = 0x41 - 5 - (9 * 3);

                outBytes = outBytes.PadList(numToPad);

                return outBytes.ToArray();
            } else
            {
                int numToPad = 0x41 - 5;
                outBytes = outBytes.PadList(numToPad);
                return outBytes.ToArray();
            }
        }

        public void ToggleLed(int Index)
        {
            this._Leds[Index] = !this._Leds[Index];
        }

        public void SetLed(bool State, int Index)
        {
            this._Leds[Index] = State;
        }

        public void ToggleLedRange(int Start, int End)
        {
            for (int Index = Start; Index <= End; Index++)
            {
                _Leds[Index] = !_Leds[Index];
            }
        }

        public void SetLedRange(int Start, int End, bool State)
        {
            for (int Index = Start; Index <= End; Index++)
            {
                _Leds[Index] = State;
            }
        }

        public void ToggleState()
        {
            this._IsActive = !_IsActive;
        }

        public void SetState(bool State)
        {
            this._IsActive = State;
        }

        public void AllLedOn()
        {
            for (int index = 0; index < _Leds.Length; index++)
            {
                _Leds[index] = true;
            }
        }

        public void AllLedOff()
        {
            for (int index = 0; index < _Leds.Length; index++)
            {
                _Leds[index] = false;
            }
        }
    }
}
