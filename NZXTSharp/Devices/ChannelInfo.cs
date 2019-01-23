namespace NZXTSharp.Devices {
    public class ChannelInfo {
        private int _NumLeds;
        private bool _IsFan;
        private bool _IsStrip;
        private int _NumSubDevices;
        private Channel _Parent;

        public int NumLeds { get => _NumLeds; }
        public int NumSubDevices { get => _NumSubDevices; }
        public bool IsFan { get => _IsFan; }
        public bool IsStrip { get => _IsStrip; }
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

            this._IsStrip = (data[3] == 0x00 ? true : false);
            this._IsFan = (data[3] == 0x01 ? true : false);

            if (this._IsFan)
                this._NumLeds = _NumSubDevices * 8;
            else
                this._NumLeds = _NumSubDevices * 10;

        }
    }
}
