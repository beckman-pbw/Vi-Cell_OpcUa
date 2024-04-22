# OPC/UA Testing with UaExpert {#opc_ua_testing_with_ua_expert}

This page brought to you by Jeremiah Teague. This topic explains how to install
and configure UaExpert and launch the Scout OPC/UA server.

[Download](https://danaher-my.sharepoint.com/:u:/g/personal/jteague_beckman_com/ESXistsb-3hJu1U6aEviIw8BNspDDvRz6L-4mW6x24YCDQ?e=76u3D3) the UA Expert installer.
 
## How to find and test the objects/types in the Hawkeye OPC UA Server using UA Expert:

### Option 1- Run the "ViCellOpcUaServer" project from Visual Studio
- Edit the ViCellOpcUaServer.csproj and change the "OutputType" from "WinExe" to "Exe" -- this makes the opc server appear in a window (instead of running without a window)
- Open the HawkeyeOpcUa code in visual studio and run it.

### Option 2 - Run the ViCellOpcUaServer in the \Instrument\OpcUaServer directory.
```
dotnet ViCellOpcUaServer.dll
```
![Add the Hawkeye OPC/UA Server](Images/RunningCmdServer.png)
The OPC UA server starts automatically and displays the server address

Note: that the server name has been updated to ViCellOpcUaServer.

![Add the Hawkeye OPC/UA Server](Images/RunningCmdServer.png)
![Add the Hawkeye OPC/UA Server](Images/ServerSettings-EndpointUrl.png)

### Copy paste the address into UA Expert as a new connection
![Add the Hawkeye OPC/UA Server](Images/ConfigureUaExpert-AddServer.png)

You will need to enter in user credentials. Currently, the username and password are required but are not checked (and who knows if this documentation has been updated after that comes to pass).

And click Ok.

### Connect to the OPC UA Server
![Add the Hawkeye OPC/UA Server](Images/ConfigureUaExpert-Connect.png)

#### Security
The Scout OPC/UA server is configured for username/password credentials over an encrypted connection. The acceptable security modes are:
* Aes256Sha256RsaPss
* [Basic256Sha256](https://docs.microsoft.com/en-us/dotnet/api/system.servicemodel.security.securityalgorithmsuite.basic256sha256?view=netframework-4.8)
* Aes128Sha256RsaOaep (not as secure)

### Exploring the Address Space
This tree allows you to explore Data and Object Types, such as enums.
You can view more info on the right panes of UA Expert after clicking
on the something in the Address Space.
![Add the Hawkeye OPC/UA Server](Images/UaExpert-AddressSpace.png)

### Event Types
![Add the Hawkeye OPC/UA Server](Images/UaExpert-Events.png)

### Object Types
![Add the Hawkeye OPC/UA Server](Images/UaExpert-ObjectTypes.png)
