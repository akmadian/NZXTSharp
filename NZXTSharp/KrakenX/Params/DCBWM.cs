using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using NZXTSharp.Exceptions;

namespace NZXTSharp.KrakenX
{
    class DCBWM
    {
        public int Value => GetValue();

        private bool IsForward = true;
        private bool WithMovement = true;
        private int ChannelByte;
        private KrakenXChannel Channel;

        public DCBWM(int ChannelByte, bool IsForward, bool WithMovement)
        {
            if (ChannelByte != 0x02)
            {
                throw new InvalidParamException("ChannelBytes for DCBWM param must be 0x02.");
            }
            this.ChannelByte = ChannelByte;
            this.IsForward = IsForward;
            this.WithMovement = WithMovement;
        }

        public DCBWM(string Channel, bool IsForward, bool WithMovement)
        {
            if (!Regex.IsMatch(Channel, @"(0?(x|X)?)\d+"))
            {
                throw new InvalidParamException("ChannelByte input formatted incorrectly. Must be 0x0n, 0n, or n");
            }

            this.ChannelByte = Convert.ToInt32(Channel[Channel.Length - 1]);
            if (this.ChannelByte != 2) 
                throw new InvalidParamException("ChannelBytes for DCBWM param must be 0x02."); 

            this.IsForward = IsForward;
            this.WithMovement = WithMovement;
        }

        public DCBWM(KrakenXChannel Channel, bool IsForward, bool WithMovement)
        {
            this.Channel = Channel;
            if (this.Channel.ChannelByte != 2)
                throw new InvalidParamException("ChannelBytes for DCBWM param must be 0x02.");

            this.IsForward = IsForward;
            this.WithMovement = WithMovement;
        }

        public int GetValue()
        {
            string Dir = IsForward ? "0" : "1";
            string WM = WithMovement ? "A" : "2";
            return Convert.ToInt32(Dir + WM);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        public static implicit operator byte(DCBWM param)
        {
            return (byte)param.GetValue();
        }
    }
}
