using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Devices.KrakenX
{
    public class KrakenRGBChannel
    {
        private int _ChannelByte;


        public int ChannelByte { get => _ChannelByte; }

        public bool State => throw new NotImplementedException();

        public KrakenRGBChannel(int ChannelByte)
        {
            this._ChannelByte = ChannelByte;
        }

        public byte[] BuildColorBytes(Color Color)
        {
            throw new NotImplementedException();
        }
    }
}
