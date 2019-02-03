using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp
{
    public interface IChannel
    {

        int ChannelByte { get; }

        byte[] BuildColorBytes(byte[] Custom);

        byte[] BuildColorBytes(Color Color);
    }
}
