using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

using NZXTSharp;
using NZXTSharp.COM;
using NZXTSharp.Exceptions;

namespace NZXTSharp.KrakenX
{
    /// <summary>
    /// Represents an NZXT KrakenX device.
    /// </summary>
    public class KrakenX : INZXTDevice
    {

        private KrakenXChannel _Both;
        private KrakenXChannel _Logo;
        private KrakenXChannel _Ring;
        private Thread OverrideLoop;
        private bool StopOverrideLoop = false;

        private USBController _COMController;

        /// <summary>
        /// The <see cref="HIDDeviceID"/> of the <see cref="KrakenX"/> device. Will always be <see cref="HIDDeviceID.KrakenX"/>.
        /// </summary>
        public HIDDeviceID DeviceID { get => HIDDeviceID.KrakenX; }

        /// <summary>
        /// The <see cref="NZXTDeviceType"/> of the <see cref="KrakenX"/> device. Will always be <see cref="NZXTDeviceType.KrakenX"/>.
        /// </summary>
        public NZXTDeviceType Type { get => NZXTDeviceType.KrakenX; }

        /// <summary>
        /// Represents both the <see cref="Logo"/>, and <see cref="Ring"/> channels.
        /// </summary>
        public KrakenXChannel Both { get => _Both; }

        /// <summary>
        /// Represents the <see cref="KrakenX"/>'s logo RGB channel.
        /// </summary>
        public KrakenXChannel Logo { get => _Logo; }
        
        /// <summary>
        /// Represents the <see cref="KrakenX"/>'s ring RGB channel.
        /// </summary>
        public KrakenXChannel Ring { get => _Ring; }
        

        /// <summary>
        /// Constructs an instance of a <see cref="KrakenX"/> device.
        /// </summary>
        public KrakenX()
        {
            InitializeChannels();
            Initialize();
        }

        private void Initialize()
        {
            _COMController = new USBController(Type);
        }

        private void InitializeChannels()
        {
            _Both = new KrakenXChannel(0x00, this);
            _Logo = new KrakenXChannel(0x01, this);
            _Ring = new KrakenXChannel(0x02, this);
            Console.WriteLine("Channels Initialized");
        }

        private void InitializeDeviceInfo()
        {
        }

        /// <summary>
        /// Writes a custom <paramref name="Buffer"/> to the <see cref="KrakenX"/> device.
        /// </summary>
        /// <param name="Buffer"></param>
        public void WriteCustom(byte[] Buffer)
        {
            _COMController.Write(Buffer);
        }

        public void StopOverrideThread()
        {
            this.StopOverrideLoop = true;
        }

        /// <summary>
        /// Applies a given <see cref="IEffect"/> <paramref name="Effect"/> to a given 
        /// <see cref="KrakenXChannel"/> <paramref name="Channel"/>.
        /// </summary>
        /// <param name="Channel"></param>
        /// <param name="Effect"></param>
        public void ApplyEffect(KrakenXChannel Channel, IEffect Effect, bool ApplyToChannel = true)
        {

            Console.WriteLine("Applying Effect");
            Console.WriteLine(Channel.ChannelByte);
            if (!Effect.IsCompatibleWith(Type))
                throw new IncompatibleEffectException("KrakenX", Effect.EffectName);

            if (ApplyToChannel)
            {
                if (Channel.ChannelByte == 0x00)
                {
                    this._Both.UpdateEffect(Effect);
                    this._Logo.UpdateEffect(Effect);
                    this._Ring.UpdateEffect(Effect);
                }
                else if (Channel.ChannelByte == 0x01)
                {
                    this.Logo.UpdateEffect(Effect);
                }
                else if (Channel.ChannelByte == 0x02)
                {
                    this.Ring.UpdateEffect(Effect);
                }
            }

            List<byte[]> CommandQueue = Effect.BuildBytes(Type, Channel);
            foreach (byte[] Command in CommandQueue)
                _COMController.Write(Command);
        }

        /// <summary>
        /// Gets the pump speed last reported by the <see cref="KrakenX"/> device.
        /// </summary>
        /// <returns>The last reported pump speed in RPM.</returns>
        public int GetPumpSpeed()
        {
            if (_COMController.LastReport != null)
            {
                HidLibrary.HidReport report = _COMController.LastReport;
                return report.Data[4] << 8 | report.Data[5];
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Sets the <see cref="KrakenX"/>'s pump speed to a given percent or RPM.
        /// </summary>
        /// <param name="Speed">
        /// The speed value to set.
        ///     Percentage values must be 50-100 (inclusive).
        ///     RPM values must be 2050-2750 (inclusive).
        /// </param>
        /// <param name="isPercent">Whether or not the speed value being set is a percentage or an RPM value. Defaults to true.</param>
        public void SetPumpSpeed(int Speed, bool isPercent = true)
        {
            if (OverrideLoop != null)
                OverrideLoop.Abort(); // I know it's bad code, but no other safe method works properly :/

            if (isPercent)
            {
                if (Speed > 100 || Speed < 50) {
                    throw new InvalidParamException("Pump speed percentages must be between 50-100 (inclusive).");
                }
            } else
            {
                if (Speed > 2750 || Speed < 1375) {
                    throw new InvalidParamException("Pump speed RPM must be between 2050-2750 (inclusive).");
                }
                else
                {
                    int diff = Speed - 2050;    // Convert RPM to percentage
                    Speed = (diff * 100) / 700;
                }
            }
            byte[] command = new byte[] { 0x02, 0x4d, 0x40, 0x00, Convert.ToByte(Speed) };
            this.StopOverrideLoop = false;
            OverrideLoop = new Thread(new ParameterizedThreadStart(PumpSpeedOverrideLoop));

            OverrideLoop.Start(command);
        }

        public void SetPumpProfile()
        {

        }

        /// <summary>
        /// Gets the last fan speed reported by the <see cref="KrakenX"/> device.
        /// </summary>
        /// <returns>The last reported fan speed in RPM.</returns>
        public int GetFanSpeed()
        {
            if (_COMController.LastReport != null)
            {
                HidLibrary.HidReport report = _COMController.LastReport;
                return report.Data[4] * 0x100 + report.Data[5];
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Sets all fans connected to the <see cref="KrakenX"/> device to a given <paramref name="Percent"/>.
        /// </summary>
        /// <param name="Percent">The percentage to set the fans to. Must be 25-100 (inclusive).</param>
        public void SetFanSpeed(int Percent)
        {
            if (Percent > 100 || Percent < 25) {
                throw new InvalidParamException("Fan speed percentage must be between 25-100 (inclusive).");
            }
            // TODO
        }

        /// <summary>
        /// Gets the last reported liquid temp.
        /// </summary>
        /// <returns>The last reported liquid temp as a rounded integer, in degrees C.</returns>
        public int? GetLiquidTemp()
        {
            if (_COMController.LastReport != null)
            {
                HidLibrary.HidReport report = _COMController.LastReport;
                double temp = (report.Data[0] + (report.Data[1] * 0.1));
                return temp.Round();
            } else
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the last HID report received from the KrakenX device.
        /// </summary>
        /// <returns>An <see cref="HidLibrary.HidReport"/>.</returns>
        public HidLibrary.HidReport GetLastReport()
        {
            return _COMController.LastReport;
        }

        /// <summary>
        /// Gets the KrakenX device's firmware version
        /// </summary>
        /// <returns>An int[]; int[0] is Major version, int[1] is Minor version.</returns>
        public int[] GetFirmwareVersion()
        {
            if (_COMController.LastReport != null)
            {
                HidLibrary.HidReport report = _COMController.LastReport;
                int minor = report.Data[12];
                return new int[] { report.Data[10], minor.ConcatenateInt(report.Data[13]) };
            } else
            {
                return null;
            }
        }

        internal void PumpSpeedOverrideLoop(object Buffer)
        {
            while (!this.StopOverrideLoop)
            {
                _COMController.Write((byte[])Buffer);
                Thread.Sleep(5000);
            }
        }
    }
}
