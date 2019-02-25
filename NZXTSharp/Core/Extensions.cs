using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp {

    internal static class IntExtensions {

        public static int DecimalToByte(this int toConvert) {
            return Convert.ToInt32(toConvert.ToString("X"));
        }

        public static int ConcatenateInt(this int thisone, int other)
        {
            return Convert.ToInt32(thisone.ToString() + other.ToString());
        }
    }

    internal static class DoubleExtensions
    {
        public static int? Round(this double thisone)
        {
            double part = thisone - Math.Truncate(thisone);
            if (part < 0.5)
            {
                return (int)thisone;
            } else if (part >= 0.5)
            {
                return (int)(thisone + (1.0 - part));
            }
            return null;
        }
    }
    
    internal static class StringExtensions {

        public static string[] SplitEveryN(this string toSplit, int n) {
            List<string> returnArr = new List<string>();
            for (int i = 0; i < toSplit.Length; i += n) {
                returnArr.Add(toSplit.Substring(i, Math.Min(n, toSplit.Length - i)));
            }
            return returnArr.ToArray();
        }

        public static string StripSpaces(this string str) => str.Replace(" ", "");

        public static string MultString(this string str, int n) {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= n; i++) { sb.Append(str); }
            return sb.ToString();
        }
    }

    internal static class ListTExtensions
    {
        public static List<byte> PadList(this List<byte> thisone, int ToLength)
        {
            int numToPad = ToLength - thisone.Count;
            for (int i = 0; i < numToPad; i++)
            {
                thisone.Add(0x00);
            }
            return thisone;
        }
    }

    internal static class ByteArrExtensions {

        public static byte[] ConcatenateByteArr(this byte[] thisone, byte[] other) {
            List<byte> l1 = new List<byte>(thisone);
            List<byte> l2 = new List<byte>(other);
            l1.AddRange(l2);
            byte[] sum = l1.ToArray();
            return sum;
        }

        public static string ToString(this byte[] thisone) {
            StringBuilder sb = new StringBuilder();
            foreach (byte thing in thisone) {
                sb.Append(thing.ToString() + " ");
            }
            return sb.ToString();
        }

        public static byte[] PadColorArr(this byte[] thisone)
        {
            int numToPad = 120 - thisone.Length;
            List<byte> temp = new List<byte>();

            foreach (byte thing in thisone) { temp.Add(thing); }

            for (int curr = 0; curr < numToPad; curr++)
            {
                temp.Add(0x00);
            }
            return temp.ToArray();
        }

        // TOFIX
        public static string ColorArrToString(this byte[] thisone)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < thisone.Length; i++)
            {
                if (!(i % 12 == 0) || i == 0)
                {
                    sb.Append(thisone[i] + " ");
                } else
                {
                    sb.Append("\n");
                }
            }
            return sb.ToString();
        }
    }

    internal static class ByteExtensions
    {
        public static int ConcatenateInt(this byte thisone, int other)
        {
            int thisByte = (int)thisone;
            return Convert.ToInt32(thisByte.ToString() + other.ToString());
        }
    }
}
