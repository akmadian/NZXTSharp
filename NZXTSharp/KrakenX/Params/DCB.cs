using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using NZXTSharp;
using NZXTSharp.Exceptions;

namespace NZXTSharp.KrakenX
{
    public class DCB : IParam
    {
        public int Value => GetValue();

        private bool _IsForward = true;
        private int ChannelByte;
        private KrakenXChannel Channel;

        public DCB(int ChannelByte, bool IsForward)
        {
            if (ChannelByte != 0x00 || ChannelByte != 0x02) {
                throw new InvalidParamException("ChannelBytes for DCB param must be 0x00 or 0x02.");
            }
            this.ChannelByte = ChannelByte;
            this._IsForward = IsForward;
        }

        public DCB(string Channel, bool IsForward)
        {
            if (!Regex.IsMatch(Channel, @"(0?(x|X)?)\d+"))
            {
                throw new InvalidParamException("ChannelByte input formatted incorrectly. Must be 0x0n, 0n, or n");
            }

            this.ChannelByte = Convert.ToInt32(Channel[Channel.Length - 1]);
            this._IsForward = IsForward;
        }

        public DCB(KrakenXChannel Channel, bool IsForward)
        {
            this.Channel = Channel;
            this._IsForward = IsForward;
        }

        public int GetValue()
        {
            string Dir = _IsForward ? "0" : "1";
            string CB = Channel != null ? Channel.ChannelByte.ToString() : ChannelByte.ToString();
            return Convert.ToInt32(Dir + CB);
        }
    }
}
