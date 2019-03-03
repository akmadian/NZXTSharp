################
KrakenX Protocol
################

I want to give a special thanks to `Jonas Malaco <https://github.com/jonasmalacofilho>`_ for his help with building
out KrakenX support in NZXTSharp, and his work in reverse engineering the KrakenX protocol.

.. contents:: Table of Contents

This document describes the HID communication protocol for NZXT KrakenX (x42, x52, x62, x72) devices. The vendor ID for NZXT is 0x1e71,
and the device id for KrakenX devices is 0x170e. 

CAM receives device information about once/ second, I call these "status reports". CAM also consistently sends packets to the Kraken device.
The packets sent from CAM are meant to set the pump/ fan speeds based on whatever pump/ fan profile is currently set.
Jonas Malaco

**********
Handshakes
**********
KrakenX devices have no hello or goodbye handshake process.

**************
Status Reports
**************
The Kraken device continuously sends status reports upstream to the PC. These reports are always 0x40 bytes long. 
So far, information about how to get pump/ fan speeds, liquid temp, and firmware version. Here is how to get that information from a status report:
In the following examples, :code:`data` refers to the array of bytes last received from the device (zero-indexed).

**Pump Speed:** data[4] << 8 | data[5] - :code:`<<` is the bitwise left-shift operator, and :code:`|` is the bitwise OR operator.

**Fan Speed:** data[4] * 0x100 + data[5]

**Liquid Temp:** data[0] + (data[1] * 0.1) - The liquid temp value in degrees C, unrounded.

**Firmware Version:**
    - Major: data[10]
    - Minor: (int)data[12].ToString() + data[13].ToString()

*********
Overrides
*********
If you want custom pump/ fan speeds to be set, the KrakenX device requires "overrides" at least once every 10 seconds, 
or the device will revert back to the CAM default "performance profile". The override buffer schema is as follows:

**Pump:** 0x02, 0x4d, 0x40, 0x00, (byte)Speed - Speed is the desired speed as a percentage.

**Fan:** 0x02, 0x4d, 0x00, 0x00, (byte)Speed - Speed is the desired speed as a percentage.

**********
Set Effect
**********
The process of setting an RGB effect is similar to how it is with the Hue+. RGB Effect packets are always 65 bytes long.
There are 5 settings bytes at the beginning, then 9 G, R, B color codes, then padding out to the 65 length with 0x00.
The 9 GRB color codes are for the 8 LEDs in the ring, and the one in the logo. Even when effects are set on just the logo or ring,
there are still 9 color codes. The first 8 seem to be for the ring, and the last is for the logo.

The settings bytes schema is as follows: 0x02, 0x4c, Param1, EffectByte, Param2

Below is a table outlining the settings packets for each effect. Bolded param values are defined below in the Param Schemas Section.

+-----------------+---------------+------+------+------------+------------+----------+-----------------------------------+
| Effect Name     | Packets/ Send |      |      | CB/ Param1 | EffectByte | Param2   | Compatible With                   |
+-----------------+---------------+------+------+------------+------------+----------+-----------------------------------+
| Fixed           | 1             | 0x02 | 0x4c | CB         | 0x00       | 0x02     | Both                              |
+-----------------+---------------+------+------+------------+------------+----------+-----------------------------------+
| Fading          | 1/ Color      | 0x02 | 0x4c | CB         | 0x01       | **CISS** | Both                              |
+-----------------+---------------+------+------+------------+------------+----------+-----------------------------------+
| SpectrumWave    | 1             | 0x02 | 0x4c | **DCB**    | 0x02       | Speed    | Both                              |
+-----------------+---------------+------+------+------------+------------+----------+-----------------------------------+
| Marquee         | 1             | 0x02 | 0x4c | CB         | 0x03       | **LSS**  | Ring Only                         |
+-----------------+---------------+------+------+------------+------------+----------+-----------------------------------+
| CoveringMarquee | 1/ Color      | 0x02 | 0x4c | **DCB**    | 0x04       | **CISS** | Both (at same time), or Ring Only |
+-----------------+---------------+------+------+------------+------------+----------+-----------------------------------+
| Alternating     | 1/ Color      | 0x02 | 0x4c | **DCBWM**  | 0x05       | **CISS** | Ring Only                         |
+-----------------+---------------+------+------+------------+------------+----------+-----------------------------------+
| Breathing       | 1/ Color      | 0x02 | 0x4c | CB         | 0x06       | **CISS** | Both                              |
+-----------------+---------------+------+------+------------+------------+----------+-----------------------------------+
| Pulse           | 1/ Color      | 0x02 | 0x4c | CB         | 0x07       | **CISS** | Both                              |
+-----------------+---------------+------+------+------------+------------+----------+-----------------------------------+
| TaiChi          | 2             | 0x02 | 0x4c | CB         | 0x08       | **CISS** | Ring Only                         |
+-----------------+---------------+------+------+------------+------------+----------+-----------------------------------+
| WaterColor      | 1             | 0x02 | 0x4c | CB         | 0x09       | Speed    | Ring Only                         |
+-----------------+---------------+------+------+------------+------------+----------+-----------------------------------+
| Loading         | 1             | 0x02 | 0x4c | CB         | 0x0a       | Speed    | Ring Only                         |
+-----------------+---------------+------+------+------------+------------+----------+-----------------------------------+
| Wings           | 1             | 0x02 | 0x4c | CB         | 0x0c       | Speed    | Ring Only                         |
+-----------------+---------------+------+------+------------+------------+----------+-----------------------------------+

Speed refers to the speed the effect will display at: 0 - 4 where 0 is slowest, and 4 is fastest. 2 is normal.

*************
Param Schemas
*************
The KrakenX shares the CISS and LSS param with the Hue+. The KrakenX does have a couple of its own unique (for now) params:

DCB - Direction/ ChannelByte
============================
This param is a concatenation of two ints:
 - First Byte: Direction -- Forward: 0, Backward: 1
 - Second Byte: ChannelByte -- The ChannelByte of the channel the effect will be applied to.
 
DCBWM - Direction/ ChannelByte (With Movement)
==============================================
This param is only used for the Alternating RGB effect. The desired value can be found with this table:

+------------------------------+------+-------+
| Direction v; With Movement > | True | False |
+------------------------------+------+-------+
| Forward                      | 0x0A | 0x02  |
+------------------------------+------+-------+
| Backward                     | 0x1A | 0x12  |
+------------------------------+------+-------+

CIS/S - Color In Set/ Speed
===========================
CIS/S params are a composite of a couple values: The index of the current color in a set of colors, and the speed of the effect.
Find the values individually, and concatenate them to get the btye to be passed as a param.

- First Digit: Color In Set. If there are multiple colors being applied, this digit denotes the index of the color.

  - To Find: digit = *x* * 2
 
    - *x*: The color number (Zero Indexed)
  
- Second Digit: Speed

  - 0 - 4 where 0 is slowest, and 4 is fastest. 2 is normal.
  
- IF Effect only uses one color, first digit is 0.
- Whole Byte: Concatenate Color In Set (IN HEX), and Speed.

  - Ex: If the effect uses one color, and was at normal speed, the resulting byte would be `02`.
  
  - Ex: If the color is the third one in the set, and the speed is at fastest, the resulting byte would be `44`.

LS/S - LED Size/ Speed
======================
To find the desired byte composite, use the table below:

+----------------------+----+----+----+----+ 
| Speed v ; LED Size > | 3  | 4  | 5  | 6  |
+======================+====+====+====+====+ 
| Slowest              | 00 | 08 | 10 | 18 |
+----------------------+----+----+----+----+ 
| Slow                 | 01 | 09 | 11 | 19 |
+----------------------+----+----+----+----+ 
| Normal               | 02 | 0a | 12 | 1a |
+----------------------+----+----+----+----+ 
| Fast                 | 03 | 0b | 13 | 1b |
+----------------------+----+----+----+----+ 
| Fastest              | 04 | 0c | 14 | 1c |
+----------------------+----+----+----+----+ 
