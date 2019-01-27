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

**inherets:** INZXTDevice.cs

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
**NZXTDeviceType Type { get; }**

**bool IsActive { get; }** 


*****
Classes
*****

HuePlus.cs
=====
Represents an NZXT Hue+ device.

**inherets:** IHueDevice.cs

Constructors
-----
**Color()**

