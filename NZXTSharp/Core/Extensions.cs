using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp {

    public static class IntExtensions {

        public static int DecimalToByte(this int toConvert) {
            return Convert.ToInt32(toConvert.ToString("X"));
        }
    }
    
    public static class StringExtensions {

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

    public static class ByteArrExtensions {

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
    }
}
