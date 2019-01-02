# HuePlus Device Documentation

The HuePlus device class is the interaction point for Hue+ Devices.

It is contained in the `NZXTSharp.Devices` namespace.

The serial protocol for this device can be found [here](https://github.com/akmadian/NZXTSharp/blob/master/Docs/Protocols/HuePlus.md).

## Constructors
To make an instace of a HuePlus Device:
``` C#
HuePlus hue = new HuePlus();
HuePlus hue = new HuePlus("CustomName"); //Or with custom device name
```

## Methods
##### ApplyEffect(Channel channel, IEffect effect)
Applies an effect object to the given channel.
```C#
HuePlus hue = new HuePlus();
hue.ApplyEffect(hue.Both, EffectObject);
```
##### Dispose()
Closes the HuePlus device's SerialPort connection.

##### Reconnect()
Closes the HuePlus device's SerialPort connection, then reinitializes it.

## Events
If you would like to receive messages from a HuePlus device for logging, subscribe to the `LogHandler` event.
``` C#
HuePlus hue = new HuePlus();
hue.OnLogMessage += new HuePlus.LogHandler(MethodName);
```
