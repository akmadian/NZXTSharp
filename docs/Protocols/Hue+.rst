#############
Hue+ Protocol
#############

.. contents:: Table of Contents

----------

Basic Protocol Schema: Command Type, ChannelByte, EffectByte, Param1, Param2, LedData
    **ChannelByte: Both = 00, Channel 1 = 01, Channel 2 = 02**
    
    **Command Types: Set Effect = 4b, Unit LED = 46, Get Channel Info = 8d**

The Hue+ operates on a serial port, and is made to handle discrete commands sent in packets.
To open a connection to a Hue+ device, open a serial connection on whatever COM port your Hue+ is operating on with a baud rate of :code:`256000`, parity set to :code:`None`, dataBits set to :code:`8`, and stopBits set to :code:`1`. Then, begin the handshake process.

Effect protocols are made of exactly 125 bytes or less. For all protocols, the first five bits in each packet are what I will call “settings bytes”, and the remaining 120 are LED data in G, R, B format. 

Settings bytes (in order) consist of which kind of command is being set, the channels to apply the effect to, which effect to set, and two parameters. See set effect protocol for more information.

**********
Handshakes
**********
To begin interaction with a Hue+ device, a handshake must first be completed.

The "Hello" handshake can be completed by continuously sending 0xc0, until the 
Hue+ unit reponds with 0x01.

There is no trick to a "GoodBye" handshake, just close the serial connection.

******************
Channel Handshakes
******************
To get information about what is connected to a channel, send an :code:`8d ChannelByte` command to the Hue+. For example, a channel handshake for channel 1 would be :code:`8d 01`.

The response should be 5 bytes long, and follows this schema:
    - ? : ? Consistent between devices
    - ? : ? Not consistent between devices
    - ? : ? Not consistent between devices.
    - X : Fan or Strip; 0x00 = strips, 0x01 = fans.
    - X : Number of fans or strips connected.

**********
Set Effect 
**********

Below is a table outlining the settings packets for each effect. Bolded param values are defined below in the `Param Scemas Section <https://nzxtsharp.readthedocs.io/en/latest/Protocols/Hue+.html#param-schemas>`_.

Direction params marked with `WM` can make use of movement in the effect. See the direction param schema below for more information.

+------------------+---------------+------+----+------------+-----------------------+----------------+
| Effect           | Packets/ Send |      |    | EffectByte | Param1                | Param2         |
+==================+===============+======+====+============+=======================+================+
| Fixed            | 1             | 0x4b | CB | 0x00       | 0x03                  | 0x02           |
+------------------+---------------+------+----+------------+-----------------------+----------------+
| Fading           | 1/ Color*     | 0x4b | CB | 0x01       | 0x03                  | **CIS/S**      |
+------------------+---------------+------+----+------------+-----------------------+----------------+
| Spectrum Wave    | 1             | 0x4b | CB | 0x02       | **Direction**         | **CIS/S**      |
+------------------+---------------+------+----+------------+-----------------------+----------------+
| Marquee          | 3             | 0x4b | CB | 0x03       | **Direction**         | **LS/S**       |
+------------------+---------------+------+----+------------+-----------------------+----------------+
| Covering Marquee | 3/ Color*     | 0x4b | CB | 0x04       | **Direction**         | **CIS/S**      |
+------------------+---------------+------+----+------------+-----------------------+----------------+
| Alternating      | 2             | 0x4b | CB | 0x05       | **Direction WM**      | **CIS/S**      |
+------------------+---------------+------+----+------------+-----------------------+----------------+
| Pulse            | 1/ Color*     | 0x4b | CB | 0x06       | 0x03                  | **CIS/S**      |
+------------------+---------------+------+----+------------+-----------------------+----------------+
| Breathing        | 1/ Color*     | 0x4b | CB | 0x07       | 0x03                  | **CIS/S**      |
+------------------+---------------+------+----+------------+-----------------------+----------------+
| Candle Light     | 1             | 0x4b | CB | 0x09       | 0x03                  | 0x02           |
+------------------+---------------+------+----+------------+-----------------------+----------------+
| Wings            | 1             | 0x4b | CB | 0x0c       | 0x03                  | **CIS/S**      |
+------------------+---------------+------+----+------------+-----------------------+----------------+

*************
Param Schemas
*************
CIS/S - Color In Set/ Speed
^^^^^^^^^^^^^^^^^^^^^^^^^^^
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

Direction
^^^^^^^^^
For direction, just like CIS/S, the byte result is a composite of two values: 
whether or not the effect's direction is forward or backward, and whether or not the effect should be moving.

If an effect's param1 byte is marked with `WM`, it can make use of movement toggling.

The byte values are as follows:
 - Forward:   03
 - Backward:  13
 - IF marked as `WM`, the following are also available:
 
   - Forward &nbsp;&nbsp;W/ Movement: 0b
   - Backward W/ Movement: 1b


LS/S - LED Size/ Speed
^^^^^^^^^^^^^^^^^^^^^^
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

********************
Getting Channel Info
********************
Information about channels can be aquired by issuing the following command:

**Structure: 8d ChannelByte**

Ex: To get channel info for channel 1, send :code:`8d 01`. For channel 2, :code:`8d 02`

The response structure is still being worked out, some of the values are still unclear, but I am working to figure it out. Here is what I have now: The response should be five or six bytes. The following schema is just what I've found in testing, and is a work in progress; take it with a grain:

+-------+---------------------------------------------+
| Value | Explanation                                 |
+=======+=============================================+
| C0    | ?                                           |
+-------+---------------------------------------------+
| 5A    | ?                                           |
+-------+---------------------------------------------+
| 8A    | ?                                           |
+-------+---------------------------------------------+
| XX    | Maybe whether fans or strips are connected? |
+-------+---------------------------------------------+
| XX    | Number of fans/ strips connected            |
+-------+---------------------------------------------+

The last byte seems to be completely absent when nothing is connected to a given channel. Sometimes, there is a 01 or 02 byte before the rest of the message, but this seems to be inconsistent.

******************
Unit LED Protocols
******************
Turning the Hue+ unit's LED on or off is pretty simple. All of the data needed fits into one packet, and seven bytes.

Just send the desired byte codes over the serial port, and the light should do as instructed.

**On: 46 00 c0 00 00 00 ff**

**Off: 46 00 c0 00 00 ff 00**

**Special Thanks to** `Pet0203 <https://github.com/Pet0203>`_. **for helping me get started and providing base code.**
