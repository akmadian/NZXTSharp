using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Params;

namespace NZXTSharp.Devices.KrakenX
{
    public class DCB : IParam
    {
        public int Value => throw new NotImplementedException();

        private bool _IsForward;
        private KrakenRGBChannel Channel;

        public DCB() { }

        public DCB(KrakenRGBChannel Channel, bool IsForward)
        {
            this.Channel = Channel;
            this._IsForward = IsForward;
        }

        public int GetValue()
        {
            string Dir = _IsForward ? "0" : "1";
            string CB = Channel.ChannelByte.ToString();
            return Convert.ToInt32(Dir + CB);
        }
    }
}
