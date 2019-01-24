using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using System.IO;
using System.IO.Ports;

namespace NZXTSharp.COM {
    class SerialController : ICOMController {
        
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

        public bool IsOpen { get => _Port.IsOpen; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="COMPort"></param>
        public SerialController(string COMPort) {
            this._PossiblePorts = new string[] { COMPort };

            Initialize();
        }

        public SerialController(string[] PossiblePorts, SerialCOMData Data) {
            this._PossiblePorts = PossiblePorts;
            this._StartData = Data;

            Initialize();
        }

        private void Initialize() {
            try
            {
                foreach (string port in _PossiblePorts) {
                    try
                    {
                        _Port = new SerialPort(port, _StartData.Baud, _StartData.Parity, _StartData.DataBits, _StartData.StopBits) {
                            WriteTimeout = _StartData.WriteTimeout,
                            ReadTimeout = _StartData.ReadTimeout
                        };

                        _Port.Open();
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
    }
}
