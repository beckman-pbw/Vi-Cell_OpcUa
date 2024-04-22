function Find-InTextFile {
    <#
    .SYNOPSIS
        Performs a find (or replace) on a string in a text file or files.
    .EXAMPLE
        PS> Find-InTextFile -FilePath 'C:\MyFile.txt' -Find 'water' -Replace 'wine'
    
        Replaces all instances of the string 'water' into the string 'wine' in
        'C:\MyFile.txt'.
    .EXAMPLE
        PS> Find-InTextFile -FilePath 'C:\MyFile.txt' -Find 'water'
    
        Finds all instances of the string 'water' in the file 'C:\MyFile.txt'.
    .PARAMETER FilePath
        The file path of the text file you'd like to perform a find/replace on.
    .PARAMETER Find
        The string you'd like to replace.
    .PARAMETER Replace
        The string you'd like to replace your 'Find' string with.
    .PARAMETER NewFilePath
        If a new file with the replaced the string needs to be created instead of replacing
        the contents of the existing file use this param to create a new file.
    .PARAMETER Force
        If the NewFilePath param is used using this param will overwrite any file that
        exists in NewFilePath.
    #>
    [CmdletBinding(DefaultParameterSetName = 'NewFile')]
    [OutputType()]
    param (
        [Parameter(Mandatory = $true)]
        [ValidateScript({Test-Path -Path $_ -PathType 'Leaf'})]
        [string[]]$FilePath,
        [Parameter(Mandatory = $true)]
        [string]$Find,
        [Parameter()]
        [string]$Replace,
        [Parameter(ParameterSetName = 'NewFile')]
        [ValidateScript({ Test-Path -Path ($_ | Split-Path -Parent) -PathType 'Container' })]
        [string]$NewFilePath,
        [Parameter(ParameterSetName = 'NewFile')]
        [switch]$Force
    )
    begin {
        $Find = [regex]::Escape($Find)
    }
    process {
        try {
            foreach ($File in $FilePath) {
                if ($Replace) {
                    if ($NewFilePath) {
                        if ((Test-Path -Path $NewFilePath -PathType 'Leaf') -and $Force.IsPresent) {
                            Remove-Item -Path $NewFilePath -Force
                            (Get-Content $File) -replace $Find, $Replace | Add-Content -Path $NewFilePath -Force
                        } elseif ((Test-Path -Path $NewFilePath -PathType 'Leaf') -and !$Force.IsPresent) {
                            Write-Warning "The file at '$NewFilePath' already exists and the -Force param was not used"
                        } else {
                            (Get-Content $File) -replace $Find, $Replace | Add-Content -Path $NewFilePath -Force
                        }
                    } else {
                        (Get-Content $File) -replace $Find, $Replace | Add-Content -Path "$File.tmp" -Force
                        Remove-Item -Path $File
                        Move-Item -Path "$File.tmp" -Destination $File
                    }
                } else {
                    Select-String -Path $File -Pattern $Find
                }
            }
        } catch {
            Write-Error $_.Exception.Message
        }
    }
}

$scriptDir = Split-Path $script:MyInvocation.MyCommand.Path

write-host "updating the ViCellOpcUaServer.csproj file to display command prompt window..."
$file = "$scriptDir\..\ViCellOpcUaServer\ViCellOpcUaServer.csproj"
Find-InTextFile -FilePath $file -Find "<OutputType>WinExe</OutputType>" -Replace "<OutputType>Exe</OutputType>"

write-host "updating the csproj files to use local gRPC dlls..."

if (Test-Path "$scriptDir\..\..\Hawkeye_gRpc\GrpcClient\bin\Debug\net48\Protos.dll") {
	$clientStrOld = "..\..\target\dependencies\lib\GrpcClient.dll"
	$serverStrOld = "..\..\target\dependencies\lib\GrpcServer.dll"
	$protosStrOld = "..\..\target\dependencies\lib\Protos.dll"

	$clientStrNew = "..\..\..\Hawkeye_gRpc\GrpcClient\bin\Debug\net48\GrpcClient.dll"
	$serverStrNew = "..\..\..\Hawkeye_gRpc\GrpcServer\bin\Debug\net48\GrpcServer.dll"
	$protosStrNew = "..\..\..\Hawkeye_gRpc\GrpcClient\bin\Debug\net48\Protos.dll"

	if ((Test-Path "$scriptDir\..\ViCellBluOpcUaModelDesign\ViCellBluOpcUaModelDesign.csproj") -eq $true -And
		(Test-Path "$scriptDir\..\ViCellBluOpcUaModelDesignTests\ViCellBluOpcUaModelDesignTests.csproj") -eq $true  -And
		(Test-Path "$scriptDir\..\ViCellOpcUaServer\ViCellOpcUaServer.csproj") -eq $true  -And
		(Test-Path "$scriptDir\..\OpcUaIntegrationTest\OpcUaIntegrationTest.csproj") -eq $true) {
		
		$path = @(
			(Resolve-Path "$scriptDir\..\ViCellBluOpcUaModelDesign\ViCellBluOpcUaModelDesign.csproj"),
			(Resolve-Path "$scriptDir\..\ViCellBluOpcUaModelDesignTests\ViCellBluOpcUaModelDesignTests.csproj"),
			(Resolve-Path "$scriptDir\..\ViCellOpcUaServer\ViCellOpcUaServer.csproj"),
			(Resolve-Path "$scriptDir\..\OpcUaIntegrationTest\OpcUaIntegrationTest.csproj")
		)

		Find-InTextFile -FilePath $path -Find $clientStrOld -Replace $clientStrNew
		Find-InTextFile -FilePath $path -Find $serverStrOld -Replace $serverStrNew
		Find-InTextFile -FilePath $path -Find $protosStrOld -Replace $protosStrNew

		$clientStrOld = "..\target\dependencies\lib\GrpcClient.dll"
		$serverStrOld = "..\target\dependencies\lib\GrpcServer.dll"
		$protosStrOld = "..\target\dependencies\lib\Protos.dll"

		$clientStrNew = "..\..\Hawkeye_gRpc\GrpcClient\bin\Debug\net48\GrpcClient.dll"
		$serverStrNew = "..\..\Hawkeye_gRpc\GrpcServer\bin\Debug\net48\GrpcServer.dll"
		$protosStrNew = "..\..\Hawkeye_gRpc\GrpcClient\bin\Debug\net48\Protos.dll"

		Find-InTextFile -FilePath $path -Find $clientStrOld -Replace $clientStrNew
		Find-InTextFile -FilePath $path -Find $serverStrOld -Replace $serverStrNew
		Find-InTextFile -FilePath $path -Find $protosStrOld -Replace $protosStrNew
	} else {
		Write-Host "`tWARNING: Cannot find the expected csproj files. Csproj files not updated" -ForegroundColor Yellow
	}
} else {
	Write-Host "`tWARNING: Cannot find Protos.dll with expected relative path. Csproj files not updated" -ForegroundColor Yellow
}