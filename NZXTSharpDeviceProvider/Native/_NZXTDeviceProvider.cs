using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using HidLibrary;

using System.Threading;
using System.IO;
using System.IO.Ports;
using System.Linq;

namespace RGB.NET.Devices.NZXT {
    public class _NZXTDeviceProvider { // TODO: Check performance, optimize if needed

        #region Fields and Properties

        private int _SDKVersion;

        private bool _ThrowExceptions;

        private SerialPort _serialPort;

        private List<_NZXTDeviceInfo> _DevicesCh1;

        private List<_NZXTDeviceInfo> _DevicesCh2;

        private List<_NZXTDeviceInfo> _Devices = new List<_NZXTDeviceInfo>();

        private bool _Initialized = false;

        private SerialController _COMController;

        private int NumSubDevices;


        public int SDKVersion { get; }

        public bool ThrowExceptions { get; }

        public _NZXTDeviceInfo[] Devices { get => _Devices.ToArray(); }

        public bool Initialized { get; }

        #endregion

        #region Constructors

        public _NZXTDeviceProvider() {
            Initialize();
        }

        public _NZXTDeviceProvider(bool ThrowExceptions) {
            _ThrowExceptions = ThrowExceptions;
            Initialize();
        }

        #endregion

        #region Methods


        public void Initialize() {
            Console.WriteLine("Native Provider Initializing");
            ScanForHuePlus();

            _Initialized = true;
        }

        private void ScanForHuePlus() 
        {
            Console.WriteLine("Starting Scan");
            SerialCOMData data = new SerialCOMData(
                Parity.None,
                StopBits.One,
                1000,
                1000,
                256000,
                8,
                "HuePlus"
            );
            Console.WriteLine("COMData Constructed");

            _COMController = new SerialController
            (
                SerialPort.GetPortNames(),
                data
            );
            Console.WriteLine("COMController Initialized");

            Console.WriteLine("Port is open - " + _COMController.Port.IsOpen);
            //Start connection
            if (_COMController.Port.IsOpen) {
                //Initial handshaking

                while (true)
                {

                    if (_COMController.Write(new byte[1] { 0xc0 }, 1).FirstOrDefault() == 1)
                    {
                        Console.WriteLine("Handshake Response Good");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Handshake Sent");
                        Thread.Sleep(50);
                    }
                }

                // TODO
                // Do channel handshakes
                //     from received data, process into desired class
                Console.WriteLine("Native Device Info Initializing");
                byte[] handshake1 = _COMController.Write(new byte[] { 0x8d, 0x01 }, 5);
                ProcessHuePlusChannelInfo(0x01, handshake1);
                Console.WriteLine("Native Device Info - Channel 1 - Initialized");

                Thread.Sleep(2000);

                Console.WriteLine("Starting Channel 2 handshake");
                byte[] handshake2 = _COMController.Write(new byte[] { 0x8d, 0x02 }, 5);
                ProcessHuePlusChannelInfo(0x02, handshake2);
                Console.WriteLine("Native Device Info - Channel 2 - Initialized");

                foreach(_NZXTDeviceInfo info in _DevicesCh1)
                {
                    _Devices.Add(info);
                }

                foreach (_NZXTDeviceInfo info in _DevicesCh2)
                {
                    _Devices.Add(info);
                }
            } else { }
        }

        public bool Reload() {
            Dispose();
            Thread.Sleep(100);

            Initialize();
            return true;
        }


        public void Dispose() {
            _COMController.Dispose();
        }

        public void UpdateChannel(_NZXTLedColor color, _NZXTDeviceInfo info) {
            
        }

        private void ProcessHuePlusChannelInfo(int ChannelByte, byte[] bytes) {
            Console.WriteLine("Processing Channel Info for - " + ChannelByte);

            Console.WriteLine("    Subdevices Found - " + bytes[4]);
            NumSubDevices += bytes[4];
            List<_NZXTDeviceInfo> temp = new List<_NZXTDeviceInfo>();

            for (int i = 0; i < NumSubDevices; i++)
            {
                Console.WriteLine("    Processing SubDevice");
                NZXTDeviceType Type;

                switch (bytes[3])
                {
                    case 0x00:
                        Console.WriteLine("    Strip Found");
                        Type = NZXTDeviceType.Strip;
                        break;
                    case 0x01:
                        Console.WriteLine("    Fan Found");
                        Type = NZXTDeviceType.Fan;
                        break;
                    default:
                        Console.WriteLine("    Unknown Device Found");
                        Type = NZXTDeviceType.Unknown;
                        break;
                }

                Console.WriteLine("    Initializing native DeviceInfo");
                _NZXTDeviceInfo info = new _NZXTDeviceInfo(ChannelByte, Type);
                Console.WriteLine("    Native DeviceInfo Initialized");
                Console.WriteLine("    Adding to _Devices");
                temp.Add(info);
                Console.WriteLine("    Added");
            }
            Console.WriteLine("    Adding From Temp");
            foreach(_NZXTDeviceInfo info in temp)
            {
                _DevicesCh1 = temp;
                Console.WriteLine("\tAdded");
            }

        }
        #endregion
    }

    internal class SerialCOMData
    {

        #region Properties and Fields
        private readonly Parity _Parity;
        private readonly StopBits _StopBits;
        private readonly int _WriteTimeout;
        private readonly int _ReadTimeout;
        private readonly int _Baud;
        private readonly int _DataBits;
        private readonly string _Name;

        /// <summary>
        /// The <see cref="System.IO.Ports.Parity"/> setting of the <see cref="SerialCOMData"/> instance.
        /// </summary>
        public Parity Parity { get => _Parity; }

        /// <summary>
        /// The <see cref="System.IO.Ports.StopBits"/> setting of the <see cref="SerialCOMData"/> instance.
        /// </summary>
        public StopBits StopBits { get => _StopBits; }

        /// <summary>
        /// The write timeout setting of the <see cref="SerialCOMData"/> instance (ms).
        /// </summary>
        public int WriteTimeout { get => _WriteTimeout; }

        /// <summary>
        /// The read timeout setting of the <see cref="SerialCOMData"/> instance (ms).
        /// </summary>
        public int ReadTimeout { get => _ReadTimeout; }

        /// <summary>
        /// The baud setting of the <see cref="SerialCOMData"/> instance.
        /// </summary>
        public int Baud { get => _Baud; }

        /// <summary>
        /// The databits setting of the <see cref="SerialCOMData"/> instance.
        /// </summary>
        public int DataBits { get => _DataBits; }

        /// <summary>
        /// The custom name of the <see cref="SerialCOMData"/> instance.
        /// </summary>
        public string Name { get => _Name; }
        #endregion


        #region Methods
        /// <summary>
        /// Constructs a <see cref="SerialCOMData"/> object.
        /// </summary>
        /// <param name="Parity"> The <see cref="Parity"/> type to use.</param>
        /// <param name="StopBits"> The number of <see cref="StopBits"/> to use. </param>
        /// <param name="WriteTimeout"> The WriteTimeout in ms.</param>
        /// <param name="ReadTimeout"> The ReadTimeout in ms.</param>
        /// <param name="Baud"> The baud to use.</param>
        /// <param name="DataBits"> The number of DataBits to use.</param>
        /// <param name="Name">A custom name for the <see cref="SerialCOMData"/>.</param>
        public SerialCOMData(Parity Parity, StopBits StopBits, int WriteTimeout, int ReadTimeout, int Baud, int DataBits, string Name = "")
        {
            this._Parity = Parity;
            this._StopBits = StopBits;
            this._WriteTimeout = WriteTimeout;
            this._ReadTimeout = ReadTimeout;
            this._Baud = Baud;
            this._DataBits = DataBits;
            this._Name = Name;
        }
        #endregion
    }

    internal class SerialController
    {

        #region Properties and Fields
        private SerialPort _Port;
        private SerialCOMData _StartData;
        private string[] _PossiblePorts;


        /// <summary>
        /// The SerialPort instance owned by the Controller.
        /// </summary>
        public SerialPort Port { get => _Port; }

        /// <summary>
        /// The <see cref="SerialCOMData"/> the <see cref="SerialController"/> used to start.
        /// </summary>
        public SerialCOMData StartData { get => _StartData; }

        /// <summary>
        /// Returns a bool telling whether or not the <see cref="SerialController"/>'s <see cref="Port"/> is open.
        /// </summary>
        public bool IsOpen { get => _Port.IsOpen; }
        #endregion

        #region Constructors
        /// <summary>
        /// Opens the SerialController on a specific COM port.
        /// </summary>
        /// <param name="COMPort">The COM port to open in "COMx" format.</param>
        public SerialController(string COMPort)
        {
            this._PossiblePorts = new string[] { COMPort };

            Initialize();
        }

        /// <summary>
        /// Attempts to open each of the COM ports in <paramref name="PossiblePorts"/> with the provided <paramref name="Data"/>.
        /// </summary>
        /// <param name="PossiblePorts">An array of possible ports the device could be on.</param>
        /// <param name="Data">The <see cref="SerialCOMData"/> to open the port with.</param>
        public SerialController(string[] PossiblePorts, SerialCOMData Data)
        {
            this._PossiblePorts = PossiblePorts;
            this._StartData = Data;

            Initialize();
        }
        #endregion

        #region Methods
        private void Initialize()
        {
            Console.WriteLine("Native Provider COMController Initializing");
            try
            {
                foreach (string port in _PossiblePorts)
                {
                    Console.WriteLine("    Trying port " + port);
                    try
                    {
                        _Port = new SerialPort(port, _StartData.Baud, _StartData.Parity, _StartData.DataBits, _StartData.StopBits)
                        {
                            WriteTimeout = _StartData.WriteTimeout,
                            ReadTimeout = _StartData.ReadTimeout
                        };
                        Console.WriteLine("    Opening port");
                        _Port.Open();
                        Console.WriteLine("    Port Opened");
                    }
                    catch (IOException)
                    {

                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException(
                    "UnauthorizedAccessException When Opening Serial Port. Ensure that CAM is not running and try again."
                );
            }
            catch (IOException)
            {
                throw new IOException(
                    String.Format("{0} could not be found on any provided COM ports. Please be sure that your {0} is connected properly.",
                    _StartData.Name)
                );
            }
            catch (Exception e)
            {
                throw new Exception("Generic exception occurred when initializing SerialController.", e);
            }
        }

        /// <summary>
        /// Writes the bytes in the <paramref name="buffer"/>, and returns the device's response.
        /// </summary>
        /// <param name="buffer">The bytes to write to the port.</param>
        /// <param name="responselength">The expected length of the response buffer (bytes).</param>
        /// <returns></returns>
        public byte[] Write(byte[] buffer, int responselength)
        {
            /*_serialPort.DiscardInBuffer();
             THIS CREATES ERRORS FOR NOW
            _serialPort.DiscardOutBuffer();*/
            _Port.Write(buffer, 0, buffer.Length); //Second handshake
            Thread.Sleep(50);
            List<byte> reply = new List<byte>();

            for (int bytes = 0; bytes < responselength; bytes++)
                reply.Add(Convert.ToByte(_Port.ReadByte()));

            return reply.ToArray();
        }

        /// <summary>
        /// Writes the given <paramref name="buffer"/> to the connected device.
        /// </summary>
        /// <param name="buffer">The bytes to write.</param>
        public void WriteNoReponse(byte[] buffer)
        {
            if (_Port.IsOpen)
                _Port.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Disposes of and reinitializes the <see cref="SerialController"/> instance.
        /// </summary>
        public void Reconnect()
        {
            Dispose();

            Initialize();
        }

        /// <summary>
        /// Disposes of the <see cref="SerialController"/> instance.
        /// </summary>
        public void Dispose()
        {
            _Port.Close();
        }
        #endregion
    }

    public class Fixed  {
        private int _EffectByte = 0x00;
        private string _EffectName = "Fixed";
        public readonly List<string> CompatibleWith = new List<string>() { "HuePlus" };

        private _NZXTDeviceInfo info;
        private _NZXTLedColor color;

        public Fixed(_NZXTLedColor color, _NZXTDeviceInfo info) {
            this.color = color;
            this.info = info;
        }

        public bool IsCompatibleWith(string name) {
            return CompatibleWith.Contains(name) ? true : false;
        }

        public List<byte[]> BuildBytes() {
            byte[] SettingsBytes = new byte[] { 0x4b, (byte)info.ChannelByte, 0x00, 0x02, 0x03 };
            //byte[] final = SettingsBytes.ConcatenateByteArr(_Color.Expanded());
            return new List<byte[]>() { };
        }
    }

    class DeviceEnumerator
    {
        public static IEnumerable<HidDevice> EnumAllDevices()
        {
            return HidDevices.Enumerate();
        }

        public static IEnumerable<HidDevice> EnumNZXTDevices()
        {
            return HidDevices.Enumerate(0x1e71);
        }

        public static IEnumerable<HidDevice> EnumKrakenXDevices()
        {
            foreach (var device in EnumNZXTDevices())
            {
                if (device.Attributes.ProductId == (int)0x170e)
                {
                    yield return device;
                }
            }
        }
    }
}
