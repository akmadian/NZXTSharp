using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.IO.Ports;

namespace NZXTSharp.COM {
    class SerialController : ICOMController {
        
        private SerialPort _Port;
        private SerialCOMData _StartData;


        /// <summary>
        /// The SerialPort instance owned by the Controller.
        /// </summary>
        public SerialPort Port { get => _Port; }

        /// <summary>
        /// The <see cref="SerialCOMData"/> the <see cref="SerialController"/> used to start.
        /// </summary>
        public SerialCOMData StartData { get => _StartData; }

        public SerialController(string COMPort) {
            Initialize(new string[] { COMPort });
        }

        public SerialController(string[] PossiblePorts, SerialCOMData Data) {
            Initialize(PossiblePorts);
        }

        private void Initialize(string[] Ports) {
            try {
                foreach (string port in Ports) {
                    try {
                        _Port = new SerialPort(port, _StartData.Baud, _StartData.Parity, _StartData.DataBits, _StartData.StopBits) {
                            WriteTimeout = _StartData.WriteTimeout,
                            ReadTimeout = _StartData.ReadTimeout
                        };
                    }
                    catch (IOException) {
                        continue;
                    }
                }
            }
            catch (UnauthorizedAccessException) {
                throw new UnauthorizedAccessException(
                    "UnauthorizedAccessException When Opening Serial Port. Ensure that CAM is not running and try again."
                );
            }
            catch (IOException) {
                throw new IOException(
                    String.Format("{0} could not be found on any provided COM ports. Please be sure that your {0} is connected properly.", Data.Name)
                );
            }
            catch (Exception) {
                throw;
            }
        }
    }
}
