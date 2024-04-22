This project (OpcUaClientAPI) is capable of building and publishing one single exe that contains all embedded resources that is necessary to make client API calls to the OpcUa Server.
To create this, you must either right-click on the OpcUaClientAPI project solution item, select 'Publish...', on the "Publish" tab item, select which operating system you would like to target and publish it to a local folder.
If you prefer not to do this manually via Visual Studio, you can do this by CLI as well:

 dotnet publish -r linux-x64 -p:PublishSingleFile=true 
						-OR-
 dotnet publish -r win10-x64  -p:PublishSingleFile=true  

In my dev environment, I have published my items to: C:\Dev\HawkeyeOpcUa\OpcUaClientAPI\bin\Release\netcoreapp3.1\publish

At this point you should have everything you need (OpcUaClientAPI.exe) and only require a valid json file to pass to it as the first parameter.
You can pass multiple json files as well.