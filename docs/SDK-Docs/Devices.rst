#######
NZXTSharp.Devices
#######

.. role:: csharp(code)
   :language: csharp

.. contents:: Table Of Contents

Classes
=====

HuePlus.cs
----------------
The implementation of the Hue+ lighting controller.

**Inherets** :code:`NZXTSharp.Device.IHueDevice.cs`

Properties
^^^^^

:code:`Channel Channel1 { get; }` Channel 1 on the Hue+ device.  

Events
^^^^^

:code:`OnLogMessage` Sends a string argument when a general event worth logging arises.

:code:`OnDataReceived` Sends a string argument when data from the Hue+ device is received

Constructors
^^^^^

To make an instace of a HuePlus Device:

.. code-block:: csharp
   
   HuePlus hue = new HuePlus();
   HuePlus hue = new HuePlus("CustomName"); //Or with custom device name

Methods
^^^^^

ApplyEffect
"""""

:code:`void ApplyEffect(Channel channel, IEffect effect)` 
   Applies an effect object to the given channel.

   .. code-block:: csharp

      HuePlus hue = new HuePlus();
      hue.ApplyEffect(hue.Both, IEffect);

Dispose
"""""

:code:`void Dispose()`
   Closes the HuePlus device's SerialPort connection.
   
Reconnect
"""""

:code:`void Reconnect()`
   Closes the HuePlus device's SerialPort connection, then reinitializes it.
   
Channel.cs
----------------

A channel object corresponds to a channel on a Hue device. Hue+ devices have two channels.

Properties
^^^^^
:code:`int ChannelByte { get; }` The channel's ChannelByte. For more information on ChannelBytes, see the Hue+ protocol.

:code:`bool State { get; set; }` Whether or not the channel is activated, true for on, false for off.

:code:`IEffect Effect { get; set; }` The effect currently set on the channel.

:code:`IHueDevice Parent { get; }` The Hue device that "owns" the channel object.

:code:`ChannelInfo ChannelInfo { get; set; }` The Channel's ChannelInfo. See the ChannelInfo class in this file for more info.

Constructors
^^^^^
:code:`Channel() {}`

:code:`Channel(int _ChannelByte) {}`
   See the protocols section for more information about ChannelBytes.

:code:`Channel(int _ChannelByte, IHueDevice Parent) {}`
   Params:
     - :code:`_ChannelByte` - See the protocols section for more information about ChannelBytes.
     - :code:`Parent` - The parent device that "owns" the Channel object.
     
:code:`Channel(int _ChannelByte, IHueDevice Parent, ChannelInfo Info) {}` 
   Params:
     - :code:`_ChannelByte` - See the protocols section for more information about ChannelBytes.
     - :code:`Parent` - The parent device that "owns" the Channel object.
     - :code:`Info` - The ChannelInfo owned by the Channel
     

Methods
^^^^^
:code:`void On() {}` Sets the Channel's state to :code:`true`. Re-sets the effect currently applied to the channel.

:code:`void Off() {}` Sets the Channel's state to :code:`false`. Sets a fixed effect with a `#000000` color applied.

:code:`void UpdateChannelInfo() {}` Updates the channel's ChannelInfo property.

ChannelInfo.cs
----------------

Information about a given Channel object.

Properties
^^^^^
:code:`int NumLeds { get; }` The number of LEDs available on the parent channel.

:code:`int NumSubDevices { get; }` The number of fans or strips available on the parent channel.

:code:`bool IsFan { get; }` Whether or not fans are connected to the parent channel.

:code:`bool IsStrip { get; }` Whether or not strips are connected to the parent channel.

:code:`bool IsActive { get; }` Whether or not the parent channel is active.

:code:`Channel Parent { get; }` The ChannelInfo's parent Channel.

Constructors
^^^^^
:code:`ChannelInfo(Channel Parent, byte[] data) {}`
   Params:
      - :code:`Parent` - The parent Channel object.
      - :code:`data` - The response from the :code:`8d 01` or :code:`8d 02` command.
      
Methods
^^^^^
:code:`void Update() {}` Updates the properties of the ChannelInfo object.

Kraken.cs
----------------
Currently blank. Just boilerplate for future Kraken implementations.

**Inherets:** :code:`NZXTSharp.Devices.IKrakenDevice.cs`

Interfaces
=====
All interfaces in the :code:`NZXTSharp.Devices` namespace inheret from the :code:`INZXTDevice.cs` interface in the :code:`NZXTSharp` namespace.

IHueDevice.cs
----------------
All Hue devices inheret from this interface; currently just the Hue+, when the Hue 2 is implemented, it will inheret from this interface too.

**Inherets** :code:`NZXTSharp.INZXTDevice.cs`

Properties
^^^^^
:code:`Channel Both { get; }` Both Channel Objects; ChannelByte: 0x00.

:code:`Channel Channel1 { get; }` Channel 1; ChannelByte: 0x01.

:code:`Channel Channel2 { get; }` Channel 2; ChannelByte: 0x02.

:code:`List<Channel> Channels { get; }` A list containing the above Channel objects.

Methods
^^^^^
:code:`ApplyEffect(Channel channel, IEffect effect) {}` Sets the given effect on the given channel.
   Params:
      - :code:`channel` - The channel object to set the effect on.
      - :code:`effect` - The effect object to set.
      
:code:`ApplyCustom(byte[] Bytes) {}` Send a custom byte array to the device.
   Params:
      - :code:`Bytes` - The custom byte array.
      
:code:`UpdateChannelInfo(Channel Channel) {}` Updates the ChannelInfo property on the given Channel.
   Params:
      - :code:`Channel` - The channel to update.

IKrakenDevice.cs
-----
Currently blank. Just boilerplate for future Kraken implementations.

**Inherets** :code:`NZXTSharp.INZXTDevice.cs`
