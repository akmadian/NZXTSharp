using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Effects.Kraken
{
    public interface IKrakenEffect
    {
        byte[] BuildBytes(Devices.RGBChannel Channel);
    }
}
