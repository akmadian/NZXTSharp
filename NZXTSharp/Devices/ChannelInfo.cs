namespace NZXTSharp.Devices {
    public class ChannelInfo {
        private int _NumLeds;
        private NZXTDeviceType _Type;
        private int _NumSubDevices;
        private bool _IsActive;
        private Channel _Parent;

        public int NumLeds { get => _NumLeds; }
        public int NumSubDevices { get => _NumSubDevices; }
        public NZXTDeviceType Type { get => _Type; }
        public bool IsActive { get => _IsActive; }
        private Channel Parent { get; }


        public ChannelInfo(Channel Parent, byte[] data) {
            ParseData(data);    
        }

        public ChannelInfo(byte[] data) {
            ParseData(data);
        }

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

        public override string ToString() {
            return string.Format("Type: {0}, NumSubDevices: {1}, NumLeds: {2}, IsActive: {3}", Type, NumSubDevices, NumLeds, IsActive);
        }
    }
}
