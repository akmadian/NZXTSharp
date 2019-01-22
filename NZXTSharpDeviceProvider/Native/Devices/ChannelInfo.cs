namespace RGB.NET.Devices.NZXT.Native.Devices {
    public class ChannelInfo {
        private int _NumLeds;
        private bool _IsFan;
        private bool _IsStrip;
        private int _NumSubDevices;
        private bool _IsActive;
        private Channel _Parent;

        public int NumLeds { get; }
        public int NumSubDevices { get; }
        public bool IsFan { get; }
        public bool IsStrip { get; }
        public bool IsActive { get; }
        private Channel Parent { get; }


        public ChannelInfo(Channel Parent, byte[] data) {
            ParseData(data);    
        }

        public void Update() {
            Parent.Parent.UpdateChannelInfo(Parent);
        }

        private void ParseData(byte[] data) {
            if (data.Length == 5) {
                this._IsActive = true;
                this._NumSubDevices = data[4];

                this._IsStrip = (data[3] == 0x00 ? true : false);
                this._IsFan = (data[3] == 0x01 ? true : false);

                if (this._IsFan)
                    this._NumLeds = _NumSubDevices * 8;
                else
                    this._NumLeds = _NumSubDevices * 10;

            } else 
                this._IsActive = false;
        }
    }
}
