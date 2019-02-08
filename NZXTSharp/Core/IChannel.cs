using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp
{
    public interface IChannel
    {
        int ChannelByte { get; }

        bool State { get; }

        byte[] BuildColorBytes(Color Color);

        byte[] BuildColorBytes(byte[] Custom);
    }
}
