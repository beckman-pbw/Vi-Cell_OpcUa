# OPC UA CLIENT API

---
Generation of the API EXE
---

The OPC UA Client API is bundled into a singular EXE through the Publish function in Visual Studio 2019.

To perform this manually:
1. In Visual Studio's Solution Explorer panel, right-click the 'OpcUaClientAPI' project.
2. In the subsequent drop-down, select "Publish...".
3. In the Publish tab, you will see the ability to publish to a folder, but more importantly, the target runtime.
4. Select either Win10-x64 for Windows, or linux-x64 for Linux.
5. Click Publish and you should see in the output path you specified the exe with all the embedded resources totaling as of writing this a bit over 70MB in size.

---
Preface
---

(Windows) There are at least two components to the OpcUaClientAPI: 
```
1) OpcUaClientAPI.exe
2) At least one .json file that contains the operations and their associated parameters.
	An example of this file is found with the supplemented "operations.json" file which contains full examples of each operation.
```

(Linux) You will need the contents of the below generated folder to run on a linux environment:
```
1) \bin\Release\netcoreapp3.1\linux-x64
2) After copying this folder to your destination and the .NET Core runtime (3.1) is installed...
3) Run it via: dotnet OpcUaClientAPI.dll "/home/me/Desktop/operations1.json"
```

---

Supported Operating Systems
---
```
- Windows 10 (64-bit)
- Linux (64-bit)
```

---
Prerequisites
---

1) .NET CORE 3.1 RUNTIME INSTALLED.
	- Ensure that the .NET Core 3.1 Runtime is installed for your operating system.
	The download for this can be found at: https://dotnet.microsoft.com/download
---
Syntax
---

NOTE: Operations should be contained within the JSON file(s) that you pass to the OpcUaClientAPI.exe.\
The operations will be called in the order that you place them and you may have multiple operations in a JSON file.

**Command Line / CLI Syntax:**\
./OpcUaClientAPI.exe [full_path_to_operations_file1.json] [full_path_to_operations_file2.json]\
./OpcUaClientAPI.exe "C:\Test\operations1.json" "C:\Test\operations2.json"
dotnet OpcUaClientAPI.dll "C:\Test\operations1.json" "C:\Test\operations2.json"

---
## API Calls and Required Parameters within JSON file
---
 #### RequestLock
 **Operation**: RequestLock\
 **Parameters**: [Username], [Password]
```
	{
		"operation": "RequestLock",
		"parameters": [ 
			"administrator", // username - administrator-level access.
			"mypassword"	 // password - associated password.
		]
	}
```
---
 #### ReleaseLock
 **Operation**: ReleaseLock\
 **Parameters**: [Username], [Password]
```
 	{
		"operation": "ReleaseLock",
		"parameters": [
			"administrator", // username - administrator-level access.
			"mypassword"	 // password - associated password.
		]
	}
```
---
 #### CleanSampleCup
 **Operation**: CleanSampleCup\
 **Parameters**: [Empty]
```
 	{
		"operation": "CleanSampleCup",
		"parameters": [
		""
		]
	}
```
---
 #### CreateCellType
 **Operation**: CreateCellType\
 **Parameters**: [UUID], [UUID], ...
```
	{
		"operation": "CreateCellType",
		"parameters": [
			"2B9D4D71-62BF-44E9-AB4B-8978D05977F7",
			"73BB68B1-8F85-4184-B217-208FC20D3A67"
		]
	}
```
---
 #### CreateQualityControl
 **Operation**: CreateQualityControl\
 **Parameters**: \
			 [CellType_ConcentrationAdjustmentFactor], // -20.0 - 20.0 \
			 [CellType_CellSharpness], // 0.000 - 100.000 \
			 [CellType_TypeName], \
			 [CellType_Uuid], \
			 [CellType_DecluserDegree], \
			 [CellType_MaxDiameter], // 1.0 - 60.0 \
			 [CellType_NumAspirationCycles], \
			 [CellType_NumImages], // 10 - 100 \
			 [CellType_NumMixingCycles], \
			 [CellType_ViableSpotArea], // 0.00 - 95.00 \
			 [CellType_ViableSpotBrightness], // 0.00 - 95.00 \
			 [QualityControl_AcceptanceLimits], // 1 - 100 \
			 [QualityControl_AssayParameter], // Concentration = 0, PopulationPercentage = 1, Size = 2\
			 [QualityControl_AssayValue], // [Concentration (0.0 - 99999.0)], [PopulationPercentage (1.0 - 100.0)], [Size (1.0 - 22.0)] \
			 [QualityControl_Comments], \
			 [QualityControl_ExpirationDate], // Unix timestamp\
			 [QualityControl_LotNumber], \
			 [QualityControl_Name], \
			 [QualityControl_Uuid]
```
 	{
		"operation": "CreateQualityControl",
		"parameters": [
			"1.0",
			"1.0",
			"example",
			"DEA7EDDC-CE22-4CD1-8C5E-4764DD402A14",
			"1.0",
			"1.0",
			"1",
			"1",
			"1",
			"1.0",
			"1.0",
			"1",
			"1",
			"1.0",
			"test comment",
			"1606159715",
			"1",
			"test name",
			"DE989AD4-CEC4-4DC3-9D7E-991805A31EBA"
		]
	}
```
---
 #### DeleteCellType
 **Operation**: DeleteCellType\
 **Parameters**: [UUID], [UUID], ...
 ```
 		{
		"operation": "DeleteCellType",
		"parameters": [
			"DEA7EDDC-CE22-4CD1-8C5E-4764DD402A14",
			"DE989AD4-CEC4-4DC3-9D7E-991805A31EBA"
		]
	}
```
---
 #### DeleteSampleResults
 **Operation**: DeleteSampleResults\
 **Parameters**: [(GUID)UUID], [(GUID)UUID], ...
```
	{
		"operation": "DeleteSampleResults",
		"parameters": [
			"DEA7EDDC-CE22-4CD1-8C5E-4764DD402A14",
			"DE989AD4-CEC4-4DC3-9D7E-991805A31EBA"
		]
	}
```
---
 #### EjectStage
 **Operation**: EjectStage\
 **Parameter**: [Empty]
```
 	{
		"operation": "EjectStage",
		"parameters": [
			""
		]
	}
```
---
 #### ExportConfig
 **Operation**: ExportConfig\
 **Parameters**: [RawConfigFileContents]
```
 	{
		"operation": "ExportConfig",
		"parameters": [
			"My Example File Contents"
		]
	}
```
---
 #### RetrieveSampleExport
 **Operation**: RetrieveSampleExport\
 **Parameters**: [UUID]
```
 	{
		"operation": "RetrieveSampleExport",
		"parameters": [
			"DEA7EDDC-CE22-4CD1-8C5E-4764DD402A14"
		]
	}
```
---
 #### GetSampleResults
 **Operation**: GetSampleResults\
 **Parameters**: \
			 [FilterOn], // 0 = SampleSet, 1 = Sample, 2 = SampleSet\
			 [FromDate], // Unix timestamp\
			 [ToDate], // Unix timestamp\
			 [Username],\
			 [SearchName],\
			 [SearchTag],\
			 [CellTypeQualityControlName]
```
 	{
		"operation": "GetSampleResults",
		"parameters": [
			"0",
			"0",
			"0",
			"test username",
			"test searchname",
			"test searchtag",
			"test celltype quality control name"
		]
	}
```
---
 #### ImportConfig
 **Operation**: ImportConfig\
 **Parameters**: [RawConfigFileContents]
```
 	{
		"operation": "ImportConfig",
		"parameters": [
			"test config contents"
		]
	}
```
---
 #### Pause
 **Operation**: Pause\
 **Parameters**: [Empty]
```
 	{
		"operation": "Pause",
		"parameters": [
			""
		]
	}
```
---
 #### Resume
 **Operation**: Resume\
 **Parameters**: [Empty]
```
 	{
		"operation": "Resume",
		"parameters": [
			""
		]
	}
```
---
 #### StartSampleSet
 **Operation**: StartSampleSet\
 **Parameters**: \
			 [SampleSetName], \
			 [SampleSetUuid], \
			 [SampleData_Dilution], \
			 [SampleData_SampleName], \
			 [SampleData_SampleUuid], \
			 [SampleData_Tag], \
			 [SampleData_CellType_ConcentrationAdjustmentFactor], // -20.0 - 20.0 \
			 [SampleData_CellType_CellSharpness], // 0.000 - 100.000 \
			 [SampleData_CellType_TypeName], \
			 [SampleData_CellType_TypeUuid], \
			 [SampleData_CellType_DecluserDegree], \
			 [SampleData_CellType_MaxDiameter], // 1.0 - 60.0 \
			 [SampleData_CellType_MinCircularity], // 0.000 - 1.000 \
			 [SampleData_CellType_MinDiameter], // 1.00 - 60.00  \
			 [SampleData_CellType_NumAspirationCycles], \
			 [SampleData_CellType_NumImages], // 10 - 100 \
			 [SampleData_CellType_NumMixingCycles], \
			 [SampleData_CellType_ViableSpotArea], // 0.00 - 95.00 \
			 [SampleData_CellType_ViableSpotBrightness], // 0.00 - 95.00 \
			 [SampleData_QualityControl_CellType_ConcentrationAdjustmentFactor], // -20.0 - 20.0 \
			 [SampleData_QualityControl_CellType_CellSharpness], // 0.000 - 100.000 \
			 [SampleData_QualityControl_CellType_TypeName], \
			 [SampleData_QualityControl_CellType_TypeUuid], \
			 [SampleData_QualityControl_CellType_DecluserDegree], \
			 [SampleData_QualityControl_CellType_MaxDiameter], // 1.0 - 60.0 \
			 [SampleData_QualityControl_CellType_MinCircularity], // 0.000 - 1.000 \
			 [SampleData_QualityControl_CellType_MinDiameter], // 1.00 - 60.00 \
			 [SampleData_QualityControl_CellType_NumAspirationCycles], \
			 [SampleData_QualityControl_CellType_NumImages], 10 - 100 \
			 [SampleData_QualityControl_CellType_NumMixingCycles], \
			 [SampleData_QualityControl_CellType_ViableSpotArea], // 0.00 - 95.00 \
			 [SampleData_QualityControl_CellType_ViableSpotBrightness], // 0.00 - 95.00 \
			 [SampleData_QualityControl_AcceptanceLimits], // 1 - 100 \
			 [SampleData_QualityControl_AssayParameter], // Concentration = 0, PopulationPercentage = 1, Size = 2 \
			 [SampleData_QualityControl_AssayValue], \
			 [SampleData_QualityControl_Comments], \
			 [SampleData_QualityControl_ExpirationDate], // Unix Timestamp\
			 [SampleData_QualityControl_LotNumber], \
			 [SampleData_QualityControl_QualityControlName], \
			 [SampleData_QualityControl_QualityControlUuid], \
			 [SampleData_WashType] // 0 = NormalWash, 1 = FastWash, 2 = NormalWash \
			 ...
```
 	{
		"operation": "StartSampleSet",
		"parameters": [
			"1",
			"test sample name",

			"1.0",
			"test sample name",
			"1",
			"test sample name",
			"DEA7EDDC-CE22-4CD1-8C5E-4764DD402A14",
			"test tag",
			"0.0",
			"0.0",
			"test cell type name",
			"DEA7EDDC-CE22-4CD1-8C5E-4764DD402A14",
			"0.0",
			"0.0",
			"0.0",
			"0.0",
			"1",
			"1",
			"1",
			"0.0",
			"0.0",
			"0.0",
			"0.0",
			"test cell type name",
			"DEA7EDDC-CE22-4CD1-8C5E-4764DD402A14",
			"0.0",
			"0.0",
			"0.0",
			"0.0",
			"1",
			"1",
			"1",
			"0.0",
			"0.0",
			"1",
			"1",
			"0.0",
			"test comments",
			"1606159715",
			"test lot number",
			"test quality control name",
			"DEA7EDDC-CE22-4CD1-8C5E-4764DD402A14",
			"1",
			
			"1.0",
			"test sample name",
			"1",
			"test sample name",
			"DEA7EDDC-CE22-4CD1-8C5E-4764DD402A14",
			"test tag",
			"0.0",
			"0.0",
			"test cell type name",
			"DEA7EDDC-CE22-4CD1-8C5E-4764DD402A14",
			"0.0",
			"0.0",
			"0.0",
			"0.0",
			"1",
			"1",
			"1",
			"0.0",
			"0.0",
			"0.0",
			"0.0",
			"test cell type name",
			"DEA7EDDC-CE22-4CD1-8C5E-4764DD402A14",
			"0.0",
			"0.0",
			"0.0",
			"0.0",
			"1",
			"1",
			"1",
			"0.0",
			"0.0",
			"1",
			"1",
			"0.0",
			"test comments",
			"1606159715",
			"test lot number",
			"test quality control name",
			"DEA7EDDC-CE22-4CD1-8C5E-4764DD402A14",
			"1"
		]
	}
```
---
 #### StartSample
 **Operation**: StartSample\
 **Parameters**: \
			 [SampleSetName], \
			 [SampleSetUuid], \
			 [SampleData_Dilution], \
			 [SampleData_SampleName], \
			 [SampleData_SampleUuid], \
			 [SampleData_Tag], \
			 [SampleData_CellType_ConcentrationAdjustmentFactor], // -20.0 - 20.0 \
			 [SampleData_CellType_CellSharpness], // 0.000 - 100.000 \
			 [SampleData_CellType_TypeName], \
			 [SampleData_CellType_TypeUuid], \
			 [SampleData_CellType_DecluserDegree], \
			 [SampleData_CellType_MaxDiameter], // 1.0 - 60.0 \
			 [SampleData_CellType_MinCircularity], // 0.000 - 1.000 \
			 [SampleData_CellType_MinDiameter], // 1.00 - 60.00  \
			 [SampleData_CellType_NumAspirationCycles], \
			 [SampleData_CellType_NumImages], // 10 - 100 \
			 [SampleData_CellType_NumMixingCycles], \
			 [SampleData_CellType_ViableSpotArea], // 0.00 - 95.00 \
			 [SampleData_CellType_ViableSpotBrightness], // 0.00 - 95.00 \
			 [SampleData_QualityControl_CellType_ConcentrationAdjustmentFactor], // -20.0 - 20.0 \
			 [SampleData_QualityControl_CellType_CellSharpness], // 0.000 - 100.000 \
			 [SampleData_QualityControl_CellType_TypeName], \
			 [SampleData_QualityControl_CellType_TypeUuid], \
			 [SampleData_QualityControl_CellType_DecluserDegree], \
			 [SampleData_QualityControl_CellType_MaxDiameter], // 1.0 - 60.0 \
			 [SampleData_QualityControl_CellType_MinCircularity], // 0.000 - 1.000 \
			 [SampleData_QualityControl_CellType_MinDiameter], // 1.00 - 60.00 \
			 [SampleData_QualityControl_CellType_NumAspirationCycles], \
			 [SampleData_QualityControl_CellType_NumImages], 10 - 100 \
			 [SampleData_QualityControl_CellType_NumMixingCycles], \
			 [SampleData_QualityControl_CellType_ViableSpotArea], // 0.00 - 95.00 \
			 [SampleData_QualityControl_CellType_ViableSpotBrightness], // 0.00 - 95.00 \
			 [SampleData_QualityControl_AcceptanceLimits], // 1 - 100 \
			 [SampleData_QualityControl_AssayParameter], // Concentration = 0, PopulationPercentage = 1, Size = 2 \
			 [SampleData_QualityControl_AssayValue], \
			 [SampleData_QualityControl_Comments], \
			 [SampleData_QualityControl_ExpirationDate], // Unix Timestamp\
			 [SampleData_QualityControl_LotNumber], \
			 [SampleData_QualityControl_QualityControlName], \
			 [SampleData_QualityControl_QualityControlUuid], \
			 [SampleData_WashType] // 0 = NormalWash, 1 = FastWash, 2 = NormalWash \
```
 	{
		"operation": "StartSample",
		"parameters": [
			"1",
			"test sample name",

			"1.0",
			"test sample name",
			"DEA7EDDC-CE22-4CD1-8C5E-4764DD402A14",
			"test tag",
			"0.0",
			"0.0",
			"test cell type name",
			"DEA7EDDC-CE22-4CD1-8C5E-4764DD402A14",
			"0.0",
			"0.0",
			"0.0",
			"0.0",
			"1",
			"1",
			"1",
			"0.0",
			"0.0",
			"0.0",
			"0.0",
			"test cell type name",
			"DEA7EDDC-CE22-4CD1-8C5E-4764DD402A14",
			"0.0",
			"0.0",
			"0.0",
			"0.0",
			"1",
			"1",
			"1",
			"0.0",
			"0.0",
			"1",
			"1",
			"0.0",
			"test comments",
			"1606159715",
			"test lot number",
			"test quality control name",
			"DEA7EDDC-CE22-4CD1-8C5E-4764DD402A14",
			"1"
		]
	}
```
---
 #### Stop
 **Operation**: Stop\
 **Parameters**: [Empty]
```
 	{
		"operation": "Stop",
		"parameters": [
			""
		]
	}
```
---