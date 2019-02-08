namespace NZXTSharp.HuePlus {

    /// <summary>
    /// Represents information about a <see cref="HuePlusChannel"/>.
    /// </summary>
    public class HuePlusChannelInfo {
        #pragma warning disable IDE0044 // Add readonly modifier
        private int _NumLeds;
        private NZXTDeviceType _Type;
        private int _NumSubDevices;
        private bool _IsActive;
        private HuePlusChannel _Parent;
        #pragma warning restore IDE0044 // Add readonly modifier

        /// <summary>
        /// Represents the total number of LEDs available on a <see cref="HuePlusChannel"/>.
        /// </summary>
        public int NumLeds { get => _NumLeds; }

        /// <summary>
        /// The number of SubDevices available on a <see cref="HuePlusChannel"/>.
        /// </summary>
        public int NumSubDevices { get => _NumSubDevices; }

        /// <summary>
        /// Type of <see cref="ISubDevice"/>s available on a <see cref="HuePlusChannel"/>.
        /// </summary>
        public NZXTDeviceType Type { get => _Type; }

        /// <summary>
        ///
        /// </summary>
        public bool IsActive { get => _IsActive; }
        private HuePlusChannel Parent { get; }

        /// <summary>
        /// Constructs a <see cref="HuePlusChannelInfo"/> with a given <paramref name="Parent"/> as its parent, from some given channel handshake <paramref name="data"/>.
        /// </summary>
        /// <param name="Parent"></param>
        /// <param name="data"></param>
        public HuePlusChannelInfo(HuePlusChannel Parent, byte[] data) {
            ParseData(data);
        }

        /// <summary>
        /// Constructs a <see cref="HuePlusChannelInfo"/> object from some given channel handshake <paramref name="data"/>.
        /// </summary>
        /// <param name="data"></param>
        public HuePlusChannelInfo(byte[] data) {
            ParseData(data);
        }

        /// <summary>
        /// Updates the parent <see cref="HuePlusChannel"/>'s <see cref="HuePlusChannelInfo"/>.
        /// </summary>
        public void Update() {
            Parent.Parent.UpdateChannelInfo(Parent);
        }

        private void ParseData(byte[] data) {
            this._NumSubDevices = data[4];

            switch(data[3]) {
                case 0x00:
                    this._Type = NZXTDeviceType.Strip;
                    break;
                case 0x01:
                    this._Type = NZXTDeviceType.Fan;
                    break;
            }

            switch (Type) {
                case NZXTDeviceType.Fan:
                    this._NumLeds = _NumSubDevices * 8;
                    break;
                case NZXTDeviceType.Strip:
                    this._NumLeds = _NumSubDevices * 10;
                    break;
            }

        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return string.Format("Type: {0}, NumSubDevices: {1}, NumLeds: {2}, IsActive: {3}", Type, NumSubDevices, NumLeds, IsActive);
        }
    }
}
