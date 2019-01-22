using System;
using System.Collections.Generic;
using System.Text;

namespace RGB.NET.Devices.NZXT.Native.Params {
    public interface IParam {

        int Value { get; }

        List<string> CompatibleWith { get; }

        int GetValue();


    }
}
