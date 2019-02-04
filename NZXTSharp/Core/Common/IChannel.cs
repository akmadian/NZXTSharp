using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp
{
    public interface IChannel
    {
        bool State { get; }

        int ChannelByte { get; }

        byte[] BuildColorBytes(byte[] Custom);

        byte[] BuildColorBytes(Color Color);
    }
}
