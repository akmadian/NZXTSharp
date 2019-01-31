using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Effects.Kraken
{
    public enum KrakenEffectBytes
    {
        Off = 0x00,
          
        Fixed = 0x00,
        Fading = 0x01,
        SpectrumWave = 0x02,
        Marquee = 0x03,
        CoveringMarquee = 0x04,
        Alternating = 0x05,
        Breathing = 0x06,
        Pulse = 0x07,
        TaiChi = 0x08,
        WaterCooler = 0x09,
        Loading = 0x0a,
        Wings = 0x0c,
        SuperWave = 0x0d

    }
}
