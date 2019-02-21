using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using NZXTSharp;
using NZXTSharp.Exceptions;

namespace NZXTSharp.KrakenX
{
    /// <summary>
    /// Represents a DCB param.
    /// </summary>
    public class DCB : IParam
    {
        /// <summary>
        /// The <see cref="DCB"/> param's value.
        /// </summary>
        public int Value => GetValue();

        private bool _IsForward = true;
        private int ChannelByte;
        private KrakenXChannel Channel;


        /// <summary>
        /// Constructs a <see cref="DCB"/> instance.
        /// </summary>
        /// <param name="ChannelByte">The ChannelByte to construct with.</param>
        /// <param name="IsForward">Whether or not the effect is moving forward.</param>
        public DCB(int ChannelByte, bool IsForward)
        {
            if (ChannelByte != 0x00 || ChannelByte != 0x02) {
                //throw new InvalidParamException("ChannelBytes for DCB param must be 0x00 or 0x02.");
            }
            this.ChannelByte = ChannelByte;
            this._IsForward = IsForward;
        }

        /// <summary>
        /// Constructs a <see cref="DCB"/> instance.
        /// </summary>
        /// <param name="Channel">A string representation of the ChannelByte to construct with.</param>
        /// <param name="IsForward">Whether or not the effect os moving forward.</param>
        public DCB(string Channel, bool IsForward)
        {
            if (!Regex.IsMatch(Channel, @"(0?(x|X)?)\d+"))
            {
                throw new InvalidParamException("ChannelByte input formatted incorrectly. Must be 0x0n, 0n, or n");
            }

            this.ChannelByte = Convert.ToInt32(Channel[Channel.Length - 1]);
            this._IsForward = IsForward;
        }

        /// <summary>
        /// Constructs a <see cref="DCB"/> instance.
        /// </summary>
        /// <param name="Channel">The <see cref="KrakenXChannel"/> to construct the param for.</param>
        /// <param name="IsForward">Whether or not the effect is moving forward.</param>
        public DCB(KrakenXChannel Channel, bool IsForward)
        {
            this.Channel = Channel;
            this._IsForward = IsForward;
        }

        /// <summary>
        /// Gets the <see cref="DCB"/> param's value.
        /// </summary>
        /// <returns></returns>
        public int GetValue()
        {
            string Dir = _IsForward ? "0" : "1";
            string CB = Channel != null ? Channel.ChannelByte.ToString() : ChannelByte.ToString();
            return Convert.ToInt32(Dir + CB);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        public static implicit operator byte(DCB param)
        {
            return (byte)param.GetValue();
        }
    }
}
