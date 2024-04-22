# Debug using you local gRPC code {#running_your_local_grpc_code}

## Script that does this work for you:
Script location: **./tools/SetUpDevEnvironment.ps1**

#### Requirements:
- You can run powershell scripts
    - Open Powershell as an admin (right-click the Windows button in bottom left)
	- Run this: `Set-ExecutionPolicy RemoteSigned`
- Your local source code all shares a common folder.
  - The script requires a tree like this:
    - Root Folder for all git repos
      - Hawkeye_gRpc
      - HawkeyeOpcUa
      - HawkeyeUserInterface
- Feel free to update and commit changes to this script but try to not break it for others.

#### What it does
- This script will update the following csproj files to use your local gRPC dlls:
  - ViCellBluOpcUaModelDesign.csproj
  - ViCellBluOpcUaModelDesignTests.csproj
  - ViCellOpcUaServer.csproj
  - OpcUaIntegrationTest.csproj
- It also updates `ViCellOpcUaServer.csproj` to use Exe instead of WinExe

## Manual steps:
If you have made changes to the Hawkeye_gRPC code and you need to update this project (HawkeyeOpcUa) to use those changes, you need to temporarily modify the project depencies for:
- GrpcClient.dll
- GrpcServer.dll
- Protos.dll

You need to remove the current/existing dll references and re-add them from your local system's bin/debug folder:

![gRpc](Images/Debugging_GrpcDependencies_1.jpg)

![gRpc](Images/Debugging_GrpcDependencies_2.jpg)

![gRpc](Images/Debugging_GrpcDependencies_3.jpg)

GrpcClient.dll and Protos.dll can be found here:
- Hawkeye_gRpc\GrpcClient\bin\Debug\net48

GrpcServer.dll can be found here:
- Hawkeye_gRpc\GrpcServer\bin\Debug\net48

## If running ScoutUI.sln
You will need to disable the watchdog that auto-starts the ViCellOpcUaServer.
Edit "HawkeyeUserInterface\ScoutServices\Watchdog\WatchdogConfiguration.json" file and remove the location:

```json
{
  "PollingIntervalMS": "5000",
  "servers": []
}
```

Also, for opc ua events to be wired up correctly, you need to start ScoutUI.sln first before starting the ViCellOpcUaServer.exe

### Do not commit/push these changes! Be sure to revert them before committing and making a pull request!