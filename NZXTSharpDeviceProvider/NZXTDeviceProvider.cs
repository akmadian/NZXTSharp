using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using RGB.NET.Core;


namespace RGB.NET.Devices.NZXT {
    public class NZXTDeviceProvider : IRGBDeviceProvider {
        #region Properties & Fields

        private IEnumerable<IRGBDevice> _Devices;

        private static NZXTDeviceProvider _instance;
        /// <summary>
        /// Gets the singleton <see cref="NZXTDeviceProvider"/> instance.
        /// </summary>
        public static NZXTDeviceProvider Instance => _instance ?? new NZXTDeviceProvider();

        /// <inheritdoc />
        /// <summary>
        /// Indicates if the SDK is initialized and ready to use.
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Gets the protocol details for the current SDK-connection.
        /// </summary>
        public NZXTProtocolDetails ProtocolDetails { get; private set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets whether the application has exclusive access to the SDK or not.
        /// </summary>
        public bool HasExclusiveAccess { get; private set; }

        /// <inheritdoc />
        public IEnumerable<IRGBDevice> Devices 
            {
                get { return _Devices; }
            private set {; }
        }

        /// <summary>
        /// The <see cref="DeviceUpdateTrigger"/> used to trigger the updates for corsair devices. 
        /// </summary>
        public DeviceUpdateTrigger UpdateTrigger { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NZXTDeviceProvider"/> class.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if this constructor is called even if there is already an instance of this class.</exception>
        public NZXTDeviceProvider() {
            Console.WriteLine("Creating New DeviceProvider");
            if (_instance != null) throw new InvalidOperationException($"There can be only one instance of type {nameof(NZXTDeviceProvider)}");
            _instance = this;
            
            UpdateTrigger = new DeviceUpdateTrigger();
        }


        public NZXTDeviceProvider(bool Init)
        {
            Console.WriteLine("Creating New DeviceProvider");
            if (_instance != null) throw new InvalidOperationException($"There can be only one instance of type {nameof(NZXTDeviceProvider)}");
            _instance = this;

            UpdateTrigger = new DeviceUpdateTrigger();

            Initialize();
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <exception cref="RGBDeviceException">Thrown if the SDK is already initialized or if the SDK is not compatible to CUE.</exception>
        /// <exception cref="CUEException">Thrown if the CUE-SDK provides an error.</exception>
        public bool Initialize(RGBDeviceType loadFilter = RGBDeviceType.All, bool exclusiveAccessIfPossible = false, bool throwExceptions = false) 
        {
            IsInitialized = false;

            try 
            {
                UpdateTrigger?.Stop();

                IList<IRGBDevice> devices = new List<IRGBDevice>();
                Console.WriteLine("PROV - Devices List Initialized");
                _NZXTDeviceProvider provider = new _NZXTDeviceProvider(throwExceptions);
                Console.WriteLine("PROV - Native Provider Initialized");
                int deviceCount = provider.Devices.Length;
                Console.WriteLine(" PROV - Devices Count - " + deviceCount);

                for(int i = 0; i < deviceCount; i++) 
                {
                    Console.WriteLine("PROV - Processing Device");
                    try 
                    {
                        NZXTRGBDeviceInfo Info = new NZXTRGBDeviceInfo(i, RGBDeviceType.Unknown, provider.Devices[i]);
                        Console.WriteLine("    PROV - DeviceInfo Initialized");
                        foreach (INZXTRGBDevice device in GetRGBDevice(Info, i, provider.Devices[i])) 
                        {
                            Console.WriteLine("    PROV - Adding Device");
                            devices.Add(device);
                            Console.WriteLine("    PROV - Native Device Added to Provider Devices List");
                        }
                    }
                    catch { if (throwExceptions) throw; }
                }
                
                UpdateTrigger?.Start();

                Console.WriteLine("PROV - Setting Devices List to Readonly Collection");
                Console.WriteLine("PROV - Devices Length: " + devices.Count);
                foreach(IRGBDevice device in devices)
                {
                    Console.WriteLine(
                        String.Format("RGB Device Found - {0} {1} - Type: {2}",
                        device.DeviceInfo.Manufacturer,
                        device.DeviceInfo.Model,
                        device.DeviceInfo.DeviceType)

                        );
                }
                this._Devices = new ReadOnlyCollection<IRGBDevice>(devices);
                Console.WriteLine("PROV - Done");
                IsInitialized = true;
            }
            catch { if (throwExceptions) throw; }
            return true;
        }

        private static IEnumerable<INZXTRGBDevice> GetRGBDevice(NZXTRGBDeviceInfo info, int i, _NZXTDeviceInfo nativeDeviceInfo) {
            switch (info.NZXTDeviceType) {
                case NZXTDeviceType.Strip:
                    yield return new NZXTStripRGBDevice(new NZXTStripRGBDeviceInfo(i, nativeDeviceInfo));
                    break;
                case NZXTDeviceType.Fan:
                    yield return new NZXTFanRGBDevice(new NZXTFanRGBDeviceInfo(i, nativeDeviceInfo));
                    break;
                case NZXTDeviceType.Cooler:
                    yield return new NZXTCoolerRGBDevice(new NZXTCoolerRGBDeviceInfo(i, nativeDeviceInfo));
                    break;
                

                // ReSharper disable once RedundantCaseLabel
                case NZXTDeviceType.Unknown:
                default:
                    throw new RGBDeviceException("Unknown Device-Type");
            }
        }

        /// <inheritdoc />
        public void ResetDevices() {
            if (IsInitialized)
                try {
                    // TODO
                }
                catch {/* shit happens */}
        }

        private void Reset() {
            ProtocolDetails = null;
            HasExclusiveAccess = false;
            Devices = null;
            IsInitialized = false;
        }

        /// <inheritdoc />
        public void Dispose() 
        { }

        #endregion
    }
}
