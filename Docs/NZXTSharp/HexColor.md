# HexColor

The HexColor class can be found [here][0].
<br>The HexColor class' namespace is in the NZXTSharp root.

To import the HexColor class:
``` C#
using NZXTSharp;
```

## Constructors
A HexColor instance can be made from one of a few constructors.

#### HexColor(string hexColor)
This constructor allows construction of a HexColor object with a hex color code.

``` C#
HexColor color = new HexColor("ffffff");  // Supports colors without hash tags
HexColor color = new HexColor("#ffffff"); // Also supports those with!
```

#### HexColor(int R, int G, int B)
This constructor allows construction of a HexColor object with RGB values.

``` C#
HexColor color = new HexColor(255, 255, 255);
```


## Methods

#### byte[] Expanded()
This method returns a byte array of 40 color codes in B, R, G format.

#### byte[] AllOff()
Like Expanded(), the AllOff method returns a byte array of 40 color codes, 
but AllOff doesn't use any of the instance's fields, instead returning an array of 40 "#000000" color codes.






[0]: https://github.com/akmadian/NZXTSharp/blob/master/NZXTSharp/Core/HexColor.cs
