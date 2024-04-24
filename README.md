This is the project for the OPC UA layer used in Vi-Cell BLU

## Project Descriptions

### OpcUaServerNetStandard
This is the code that handles communication on the OPC UA layer. 
It will not contain any data that is used by a specific implementation.

### ViCellBluOpcUaModelDesign
This project houses the ModelDesign.xml and ViCellBlu-specific implementation details and config files.
This project also includes a build step to generate the code form the ModelDesign.xml.

### ViCellOpcUaServer
This is a simple command line app made to test the implemenation to ensure a generic OPC UA client (like UA Expert) can 
connect and traverse the code generated from the ModelDesign.xml. This project just creates a while(true) loop 
to allow OPC UA client connections.

## Build Instructions
Like other ScoutX projects, you must run 'mvn package' to fetch required DLLs into the
target/dependencies folder. At that point, you can use Visual Studio's build process.