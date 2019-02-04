using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using NZXTSharp.Devices.Common;
using NZXTSharp.Exceptions;

namespace NZXTSharp.Devices.KrakenX
{
    public class DCBWM : IParam
    {
        int?[][] DCBWMArr = new int?[][] 
        {
            new int?[] {null, null},
            new int?[] {null, null},
            new int?[] {0x0A, 0x02}
        };

        private bool _IsForward = true;
        private int ChannelByte;
        private KrakenRGBChannel Channel;

        public int Value => throw new NotImplementedException();

        public DCBWM(int ChannelByte, bool IsForward)
        {
            if (ChannelByte != 0x02)
            {
                throw new InvalidParamException("ChannelByte for DCBWM or 0x02.");
            }
            this.ChannelByte = ChannelByte;
            this._IsForward = IsForward;
        }

        public DCBWM(string Channel, bool IsForward)
        {
            if (!Regex.IsMatch(Channel, @"(0?(x|X)?)\d+"))
            {
                throw new InvalidParamException("ChannelByte input formatted incorrectly. Must be 0x0n, 0n, or n");
            }

            this.ChannelByte = Convert.ToInt32(Channel[Channel.Length - 1]);
            this._IsForward = IsForward;
        }

        public DCBWM(KrakenRGBChannel Channel, bool IsForward)
        {
            this.Channel = Channel;
            this._IsForward = IsForward;
        }

        public int GetValue()
        {
            int CB = Channel != null ? Channel.ChannelByte : ChannelByte;
            int? outInt = DCBWMArr[CB][_IsForward ? 0 : 1];
            return outInt ?? 0x0A;
        }
    }
}
