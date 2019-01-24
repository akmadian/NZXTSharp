using System;
using System.Collections.Generic;
using System.Text;

using System.IO.Ports;

namespace NZXTSharp.COM {

    /// <summary>
    /// Contains information needed to open a COM port.
    /// </summary>
    class SerialCOMData {

        private Parity _Parity;
        private StopBits _StopBits;
        private int _WriteTimeout;
        private int _ReadTimeout;
        private int _Baud;
        private int _DataBits;
        private string _Name;

        public Parity Parity { get; }
        public StopBits StopBits { get; }
        public int WriteTimeout { get; }
        public int ReadTimeout { get; }
        public int Baud { get; }
        public int DataBits { get; }
        public string Name { get; }

        /// <summary>
        /// Constructs a <see cref="SerialCOMData"/> object.
        /// </summary>
        /// <param name="Parity"> The <see cref="Parity"/> type to use.</param>
        /// <param name="StopBits"> The number of <see cref="StopBits"/> to use. </param>
        /// <param name="WriteTimeout"> The WriteTimeout in ms.</param>
        /// <param name="ReadTimeout"> The ReadTimeout in ms.</param>
        /// <param name="Baud"> The baud to use.</param>
        /// <param name="DataBits"> The number of DataBits to use.</param>
        public SerialCOMData(Parity Parity, StopBits StopBits, int WriteTimeout, int ReadTimeout, int Baud, int DataBits, string Name = "") {
            this._Parity = Parity;
            this._StopBits = StopBits;
            this._WriteTimeout = WriteTimeout;
            this._ReadTimeout = ReadTimeout;
            this._Baud = Baud;
            this._DataBits = DataBits;
            this._Name = Name;
        }
    }
}
