@echo off
setlocal

SET PATH=%PATH%;.\OpcUaModelCompiler;

echo Building ModelDesign...

IF EXIST %CD%\CodeGen\OpcUaModelCompiler\Opc.Ua.ModelCompiler.exe (
	%CD%\CodeGen\OpcUaModelCompiler\Opc.Ua.ModelCompiler.exe -d2 ".\ViCellBluOpcUaModelDesign\Model\ModelDesign.xml" -cg ".\ViCellBluOpcUaModelDesign\Model\ModelDesign.csv" -o2 ".\ViCellBluOpcUaModelDesign\Model"
) ELSE (
	%CD%\..\CodeGen\OpcUaModelCompiler\Opc.Ua.ModelCompiler.exe -d2 "..\ViCellBluOpcUaModelDesign\Model\ModelDesign.xml" -cg "..\ViCellBluOpcUaModelDesign\Model\ModelDesign.csv" -o2 "..\ViCellBluOpcUaModelDesign\Model"
)

REM Return code is always 0
echo Completed ModelDesign Build Attempt

rem pause
exit /b