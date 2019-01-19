using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Params {
    public interface IParam {

        int Value { get; }

        List<string> CompatibleWith { get; }

        int GetValue();


    }
}
