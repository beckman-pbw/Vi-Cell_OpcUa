@mainpage
# ScoutX OPC/UA Server

The ScoutX OPC/UA server is a separate executable running on the ViCell BLU hardware in
a different process than the ScoutX application. The ScoutX application now exposes
itself as a gRPC server providing access to its functions via the new ScoutServices layer.
This OPC/UA server is a thin layer that maps the OPC design model to mirrored gRPC proto
interface. Each OPC/UA server side stub makes a corresponding gRPC client call to the gRPC
server stub in the Scout application

## Generating Code from the OPC/UA Model Design
You can generate the code from the ModelDesign.xml 1 of 3 ways:
* Building the solution in VS (OpcUaServerNetStandard.csproj has a pre-build step to generate the code)
* Running 'mvn package'
* Running CodeGen\BuildDesign.bat directly

Ensure that Visual Studio displays the generated code objects in the solution explorer.

## Topics

* [OPC/UA Testing with UaExpert](@ref opc_ua_testing_with_ua_expert)
* [Implementing OPC/UA Events in the Server](@ref implmenting_opc_ua_events_in_the_server)
* [Running your local gRPC code](@ref running_your_local_grpc_code)