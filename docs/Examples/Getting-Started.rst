##############################
Getting Started with NZXTSharp
##############################

This page contains getting started examples and code snippets.

NZXTSharp's syntax is designed to be simple and easy to use, almost like python. 
The structure is built around devices, subdevices, effects, and params.

Effects are created with params, and effects are applied to devices. Devices can own subdevices.

An example of a subdevice is a fan, or RGB strip. Once Hue 2 support is added, the CableComb and Underglow SubDevices will be added.

.. contents:: Table of Contents

----------

*****
Hue+
*****
The Hue+ model consists of the HuePlus class instance. The instance owns three Channel objects, but only Channel1 and Channel2 have SubDevices. 

Boilerplate
-----------

To get started, you'll need the following:

.. code-block:: csharp

  using NZXTSharp.Devices;
  
  HuePlus hue = new HuePlus();
  
With this HuePlus instance, you can apply effects, get channel info, toggle the unit LED, etc.
  

Applying Effects
----------------
Effects are applied to a Hue+ device with the :code:`ApplyEffect()` method. The channel(s) to apply the effect to, and the effect object are passed as params to this method.

Adding on to the last example:

.. code-block:: csharp

  using NZXTSharp;
  using NZXTSharp.Devices;
  using NZXTSharp.Effects;
  
  HuePlus hue = new HuePlus();
  
  HexColor color = new Color(255, 255, 255); // You can make colors with RGB values
  HexColor color = new Color("#ffffff"); // Also works with Hex codes (with or without the leading #)
  
  Fixed effect = new Fixed(color); // Create effect
  
  hue.ApplyEffect(hue.Both, effect); // Apply effect to both channels
  hue.ApplyEffect(hue.Channel1, effect); // Or just one
  
  hue.UnitLedOff(); // Turn unit LED off
  hue.UnitLedOn(); // And back on again!
  
  
SubDevices
----------
A HuePlus instance has two Channels that own subdevices: Channel1 and Channel2. The Both Channel does not own any because it is not a "physical" channel. Keep in mind that ALL changes to subdevices need to be "pushed" to the device by setting, or re-setting an effect.

All subdevice classes are held in the :code:`NZXTSharp.Devices` namespace.

Building on the Boilerplate example:

.. code-block:: csharp

  using NZXTSharp.Devices;
  using NZXTSharp.Effects;
  
  HuePlus hue = new HuePlus();
  Fixed effect = new Fixed(new Color("#FFFFFF"));
  
  List<ISubDevice> Ch1Devices = hue.Channel1.SubDevices; // Not necessary, but syntax looks better
  
  Ch1Devices[0].AllLedOff(); // Turn off all LEDs on first subdevice in channel.
  hue.ApplyEffect(hue.Channel1, effect); // Apply changes
  
  Ch1Devices[1].ToggleLed(9); // Toggle individual LEDs with the ToggleLed method
  Ch1Devices[1].Leds[8] = false; // Or by setting the value.
  hue.ApplyEffect(hue.Channel1, effect); // Apply changes
  
  Ch1Devices[1].ToggleLedRange(1, 5); // Or, toggle ranges of LEDs
  hue.ApplyEffect(hue.Channel1, effect); // Apply changes
 
Custom RGB values for a Fixed Effect
------------------------------------
The fixed effect, being the most versatile, allows the user of NZXTSharp to construct a fixed effect with custom RGB values for each LED. This is done by passing a byte array to a Fixed effect constructor.

**Byte array schema:** The byte array must have at least 1 RGB value, and at most 120 RGB values. If the length is not within this range, an InvalidParamException will be thrown.

**RGB Value formatting:** For the effect to display properly, all RGB values MUST be passed in G, R, B format. RGB Values, like all other RGB values in NZXTSharp must be between 0-255 (inclusive).

Building on the Boilerplate example:

.. code-block:: csharp
  
  using NZXTSharp;
  using NZXTSharp.Devices;
  using NZXTSharp.Effects;
  
  HuePlus hue = new HuePlus();
  
  // Create RGB array, this will have two LEDs lit: one white, one red.
  byte[] colors = new byte[] { 255, 255, 255, 0, 255, 0 };
  Fixed effect = new Fixed(colors); // Create Effect
  
  hue.ApplyEffect(hue.Both, effect); // And apply it!
  
  // Also supports subdevice and LED toggling.
  hue.Channel1.SubDevices[1].ToggleState(); // Toggle device
  hue.ApplyEffect(hue.Both, effect); // Apply changes
  
  
  
  
  
  
