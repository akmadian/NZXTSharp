using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp;

namespace NZXTSharp.KrakenX
{
    public class DCB : IParam
    {
        public int Value => throw new NotImplementedException();

        private bool _IsForward;
        private KrakenXChannel Channel;

        public DCB() { }

        public DCB(KrakenXChannel Channel, bool IsForward)
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
