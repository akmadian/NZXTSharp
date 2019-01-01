using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp {
    public interface IEffect {

        int EffectByte { get; }

        byte[] EffectBytes { get; }

        Channel Channel { get; set; }

        HexColor Color { get; set; }

        byte[] BuildBytes();
        
    }
}
