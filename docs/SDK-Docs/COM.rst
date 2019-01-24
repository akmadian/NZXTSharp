#####
NZXTSharp.COM
#####

The :code:`NZXTSharp.COM` namespace contains classes used for facilitating communication between INZXTDevices and their respective hardware.

Nothing in the COM namespace is meant to be accessed by the user of NZXTSharp. All classes serve infrastructural purposes.

.. contents:: Table Of Contents

*****
Classes
*****

SerialController.cs
=====

**Inherets** :code:`NZXTSharp.COM.ICOMController.cs`

Properties
-----
:code:`SerialPort Port { get; }` The serial port operated by the SerialController.

:code:`SerialCOMData StartData { get; }` The SerialCOMData used to start the SerialController.

:code:`bool IsOpen { get => Port.IsOpen; }` Whether or not the SerialPort operated by the SerialController is open.

Constructors
-----
:code:`SerialController(string[] PossiblePorts, SerialCOMData Data) {}` 
    Params:
        - :code:`PossiblePorts` - A string array containing the ports to attempt opening. Format: "COM3".
        - :code:`Data` - A SerialCOMData object containing the information needed to open the port.

Methods
-----
:code:`byte[] Write(byte[] buffer, int responselength) {}` Writes the bytes in the given buffer, and returns the device's response.
    Params:
        - :code:`buffer` - The bytes to write to the device.
        - :code:`responselength` - The expected length of the response: dictates the size of the returned buffer. Improper sizing will result in lost data.
        
:code:`void WriteNoResponse(byte[] buffer) {}` Writes the bytes in the given buffer to the device. Does not return a response.
    Params:
        - :code:`buffer` - The bytes to write to the device.
        
:code:`void Reconnect() {}` Disposes of and reinitializes the SerialController instance.

:code:`void Dispose() {}` Disposes of the SerialController instance.

SerialCOMData.cs
=====

Properties
-----
:code:`System.IO.Ports.Parity Parity { get; }` The parity type to use.

:code:`System.IO.Ports.StopBits StopBits { get; }` The stopbits to use.

:code:`int WriteTimeout { get; }` The write timeout in ms.

:code:`int ReadTimeout { get; }` The read timeout in ms.

:code:`int Baud { get; }` The baud rate to open the port with.

:code:`int DataBits { get; }` The number of databits to use.

Constructors
-----
The SerialCOMData class only has one constructor, and takes arguments corresponding to each of the available properties. The name parameter is optional, and defaults to an empty string.

*****
Interfaces
*****

ICOMController.cs
=====
Currently empty.
