#####
NZXTSharp.Devices
#####

.. contents:: Table of Contents

---------

*****
Enum
*****

NZXTDeviceType.cs
=====
Contains unique ID numbers for each model of NZXT device.

SerialDeviceID.cs
=====
Contains the NZXT Vendor ID, and Product IDs for NZXT devices.


*****
Interfaces
*****

IUSBDevice.cs
=====
Represents an HID NZXT device.




ISerialDevice.cs
=====
Represents an NZXT deivce that communicates over a COM port.




IHueDevice.cs
=====
Represents an NZXT Hue device.

**inherits:** INZXTDevice.cs

Properties
-----
**Channel Both { get; }**

**Channel Channel1 { get; }**

**Channel Channel2 { get; }**

**List<Channel> Channels { get; }**

Methods
-----
**void ApplyEffect(Channel channel, IEffect effect);**

**void ApplyEffect(Channel channel, IEffect effect, bool ApplyToChannel);**

**void ApplyCustom(byte[] Bytes);**

**void UpdateChannelInfo(Channel Channel);**




ISubDevice.cs
=====
Represents a subdevice: any device that may be connected to a device's channel.

Properties
-----
**NZXTDeviceType Type { get; }** The NZXTDeviceType of a given ISubDevice.

**bool IsActive { get; }** Whether or not a given ISubDevice is active (on).

**int NumLeds { get; }** The number of LEDs available on a given ISubDevice.

**List<bool> Leds { get; }** A list containing the power states of a given ISubDevice's LEDs.

Methods
-----
**void ToggleState()** Toggles the ISubDevice's state.

**void SetState(bool State)** Sets the ISubDevice's state.
    - param bool State - The state to set the ISubDevice to. true: on, false: off.
    
**void ToggleLed(int Index)** Toggles a specific LED owned by a given ISubDevice.
    - param int Index - The index of the ISubDevice's Leds list to toggle.
    
**void ToggleLedRange(int Start, int End)** Toggles all LEDs between a given start and end index.
    - param int Start - The index in the ISubDevice's Leds list to start at.
    - param int End   - The index in the ISubDevice's Leds list to end at.
    
**void SetLedRange(int Start, int End, bool Value)** Sets all LEDs between a given start and end index to a given state.
    - param int Start  - The index in the ISubDevice's Leds list to start at.
    - param int End    - The index in the ISubDevice's Leds list to end at.
    - param bool Value - The value to set each LED to.
    
**void AllLedOn()** Sets all LEDs in the ISubDevice's Leds list to true.

**void AllLedOff()** Sets all LEDs in the ISubDevice's Leds list to false.

*****
Classes
*****

HuePlus.cs
=====
Represents an NZXT Hue+ device.

**inherits:** IHueDevice.cs

Properties
-----
**string Name { get; }** The device's product name.

**Channel Both { get; }** A Channel object representing both channels on the Hue+

**Channel Channel1 { get; }** A channel object representing the Channel 1 of the Hue+ device.

**Channel Channel2 { get; }** A channel object representing the Channel 2 of the Hue+ device.

**List<Channel> Channels { get; }** A List containing all Channel objects owned by the Hue+ device.

**string CustomName { get; set; }** A custom name for the HuePlus device instance.

**NZXTDeviceType Type { get; }** The NZXTDeviceType of the HuePlus device. :code:`NZXTDeviceType.HuePlus`

Constructors
-----
**HuePlus()** 
Constructs a basic HuePlus instance.

**HuePlus(int MaxHandshakeRetry = 5, string CustomName = null)** Constructs a HuePlus instance with a custom retry count, and a custom name.
    - param int MaxHandshakeRetry - Defaults to 5
    - param string CustomName     - Deafults to null.
    
Methods
-----
**void Reconnect()** Disposes of and reinitializes to the HuePlus instance's COMController.

**void Dispose()**   Disposes of the HuePlus instance's COMController.

**void ApplyEffect(Channel channel, IEffect effect, bool SaveToChannel = true)** Applies a given IEffect to a given Channel.
    - param Channel channel    - The HuePlus Channel to apply the affect to. Must be owned by the same HuePlus instance the effect is being applied to.
    - param IEffect effect     - The IEffect to apply.
    - param bool SaveToChannel - Whether or not to save the given IEffect to the given Channel. Defaults to true.
    
**void ApplyCustom(byte[] Buffer)** Writes a custom buffer to the HuePlus instance's COMController.

**void UnitLedOn()** Turns on the Hue+ device's unit led.

**void UnitLedOff()** Turns off the Hue+ device's unit led.

**void SetUnitLed(bool State)** Sets the Hue+ device's unit led based on the :code:`State` param.
    - param bool State - Which state to set the LED to. true: on, false: off.
    
**void UpdateChannelInfo(Channel Channel)** Updates the given Channel's ChannelInfo.
    - param Channel Channel - The Channel instance to update.
    
    
Channel.cs
=====  
Channels are "owned" by devices. Channels also "own" a number of ISubDevices, and a ChannelInfo object.

Properties
-----
**int ChannelByte { get; }** The Channel instance's ChannelByte. 

**IEffect Effect { get: }** The IEffect currently applied to the Channel isntance.

**bool State { get; }** Whether or not the Channel instance is active (on).

**ChannelInfo ChannelInfo { get; }** The Channel's ChannelInfo object.

**IHueDevice Parent { get; }** The device that owns the Channel.

**List<ISubDevice> SubDevices { get; }** A list of ISubDevices owned by the Channel.

Constructors
-----
**Channel(int ChannelByte)** Constructs a Channel instance with the given ChannelByte.
    - param int ChannelByte - The Channel's ChannelByte; 0x00 for both, 0x01 for Channel 1, 0x02 for Channel 2.
    
**Channel(int ChannelByte, IHueDevice Parent)** Constructs a Channel instance with a given ChannelByte, owned by a given Parent.
    - param int ChannelByte   - The Channel's ChannelByte; 0x00 for both, 0x01 for Channel 1, 0x02 for Channel 2.
    - param IHueDevice Parent - The IHueDevice that will own the Channel object. 
    
**Channel(int ChannelByte, IHueDevice Parent, ChannelInfo Info)** Constructs a Channel instance with a given ChannelByte, owned by a given Parent, and a given ChannelInfo instance.
    - param int ChannelByte   - The Channel's ChannelByte; 0x00 for both, 0x01 for Channel 1, 0x02 for Channel 2.
    - param IHueDevice Parent - The IHueDevice that will own the Channel object. 
    - param ChannelInfo Info  - A ChannelInfo object about the Channel.

Methods
-----
**void RefreshSubDevices()** Refreshes all ISubDevices in the Channel's SubDevices list.

**void On()** Turns the Channel on, and re-applies the last applied effect.

**void Off()** Turns the Channel off, and applies an "#000000" fixed effect.

**void UpdateChannelInfo()** Updates the Channel instance's ChannelInfo object.

**void SetChannelInfo(ChannelInfo Info)** Sets the Channel instance's ChannelInfo to a given ChannelInfo "Info".


ChannelInfo.cs
=====  
Users of NZXTSharp are generally not meant to construct ChannelInfo instances.

Properites
-----
**int NumLeds { get; }** The total number of LEDs available on a Channel.

**int NumSubDevices { get; }** The number of ISubDevices available on a given Channel.

**NZXTDeviceType Type { get; }** The type of ISubDevices available on a Channel.

Constructors
-----
**ChannelInfo(byte[] data)** Constructs a ChannelInfo instance from some data returned from a channel handshake.

Methods
-----
**void Update()** Updates the information contained in a ChannelInfo object.


Fan.cs
=====
**inherits:** ISubDevice.cs

No methods or properties that are not in ISubDevice.cs


Strip.cs
=====
**inherits:** ISubDevice.cs

No methods or properties that are not in ISubDevice.cs
