using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;

namespace NZXTSharp.KrakenX
{
    public class KrakenXChannel : IChannel
    {
        public int ChannelByte => throw new NotImplementedException();

        public bool State => throw new NotImplementedException();

        public byte[] BuildColorBytes(byte[] Custom)
        {
            throw new NotImplementedException();
        }

        public byte[] BuildColorBytes(Color Color)
        {
            throw new NotImplementedException();
        }
    }
}
