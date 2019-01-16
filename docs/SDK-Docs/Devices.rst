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

Interfaces
=====

IHueDevice.cs
----------------
