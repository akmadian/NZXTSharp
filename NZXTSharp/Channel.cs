using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace NZXTSharp {
    /*
    public class Channel {
        private byte ChannelByte = 0x00;
        public void SetFixed(Color color) {
            Devices.ColDevice.nzxtDevice.WriteSerial(new byte[] { 0x4b, ChannelByte, 0x00, 0x02, 0x02 }, 0, 5); //Initial
            for (int i = 0; i < NZXTDevice.NumOfLeds; i++) {
                Devices.ColDevice.nzxtDevice.WriteSerial(new byte[] { color.G, color.R, color.B }, 0, 3); //Set all
            }
        }
        public void SetFading(int speed, Color[] colors) {
            int NumOfColors = 0;
            foreach (Color color in colors) {
                Devices.ColDevice.nzxtDevice.WriteSerial(new byte[] { 0x4b, ChannelByte, 0x01, 0x02, Convert.ToByte(speed) }, 0, 5); //Initial
                for (int i = 0; i < NZXTDevice.NumOfLeds; i++) {
                    Devices.ColDevice.nzxtDevice.WriteSerial(new byte[] { 0x00, 0x00, 0xFF }, 0, 3); //Set all
                }
                NumOfColors++;
            }
        }
        public void SetWave(int speed, NZXTDevice.Direction direction) {
            Devices.ColDevice.nzxtDevice.WriteSerial(new byte[] { 0x4b, ChannelByte, 0x02, Convert.ToByte(direction), Convert.ToByte(speed) }, 0, 5); //Initial
            for (int i = 0; i < NZXTDevice.NumOfLeds; i++) {
                Devices.ColDevice.nzxtDevice.WriteSerial(new byte[] { 0x00, 0x00, 0xFF }, 0, 3); //Set all
            }
        }
        public Channel(byte channel) {
            ChannelByte = channel;
        }
    }*/
}
