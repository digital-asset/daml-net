# Generates the Ledger API bindings for C#
# Takes the SDK version and optionally the LF version as input, eg `generate-bindings.ps [sdkVer [lfVer]
param (
    [Parameter(Mandatory=$true,HelpMessage="Please pass the SDK version as an argument")][string] $damlSdkVersion,
    [Parameter(Mandatory=$false)][string] $damlLfVersion
)

$ErrorActionPreference = "Stop"

$damlLfNamespace="digitalasset"

if (([string]::IsNullOrEmpty($damlLfVersion))) {
    $damlLfVersion = "1_8"
}
else {
    if (([System.Version] $damlLfVersion) -ge ([System.Version] "1.11")) {
        $damlLfNamespace = "daml"
    }
   
    $damlLfVersion = $damlLfVersion.Replace('.', '_')

    if ($damlLfVersion -ne "1_8") {
        Write-Host "Note that the namespaces in the Daml.Ledger.Fragment project and the daml-lf package linkages in the examples will need updating to the specified LF version" -ForegroundColor "yellow"
    }
}

# Get the proto files for this release, which will validate that the SDK verison is valid and >= 1.0.0 as the proto archive then moved to a new location

Remove-Item -Force -Recurse -ErrorAction Ignore protobuf
Remove-Item -Force -Recurse -ErrorAction Ignore tmp

New-Item -ItemType directory -ErrorAction Ignore -Path tmp | Out-Null

$ProtoFile="protobufs-$damlSdkVersion.zip"

New-Item -ItemType file "./tmp/$ProtoFile"

curl -L "https://github.com/digital-asset/daml/releases/download/v$damlSdkVersion/$ProtoFile" --output "./tmp/$ProtoFile"

Expand-Archive -Path "./tmp/$ProtoFile" -DestinationPath "./tmp" -Force

# Copy the daml lf archives - this will validate any passed in daml lf version

Copy-Item -Path "./tmp/protos-$damlSdkVersion/com/$damlLfNamespace/daml_lf_$damlLfVersion" -Destination "./protobuf/daml-lf/com/$damlLfNamespace/daml_lf_$damlLfVersion" -Recurse

$version = [System.Version] $damlSdkVersion

if (($version -ge ([System.Version] "1.8.0")) -and ($version -le ([System.Version] "1.10.0"))) {
   
    # The package folder structure is lost from the archives after 1.7.0 so try to restore it - hopefully fixed after 1.10.0
   
    $apiPath="protobuf/ledger-api/com/daml/ledger/api/v1"
   
    New-Item -ItemType directory -Path "$apiPath/admin"    | Out-Null
    New-Item -ItemType directory -Path "$apiPath/testing" | Out-Null   
    Copy-Item -Path "./tmp/protos-$damlSdkVersion/*.proto"       -Destination "$apiPath"
    Move-Item -Path "$apiPath/*_management_*.proto"              -Destination "$apiPath/admin/"
    Move-Item -Path "$apiPath/participant_pruning_service.proto" -Destination "$apiPath/admin/"
    Move-Item -Path "$apiPath/time_service.proto"                -Destination "$apiPath/testing/"
    Move-Item -Path "$apiPath/reset_service.proto"               -Destination "$apiPath/testing/"
}
else {
    Copy-Item -Path "./tmp/protos-$damlSdkVersion/com/daml/ledger" -Destination "./protobuf/ledger-api/com/daml/ledger" -Recurse
}

New-Item -ItemType directory -ErrorAction Ignore -p protobuf/ledger-api/google/rpc | Out-Null
New-Item -ItemType file protobuf/ledger-api/google/rpc/status.proto

curl -L https://raw.githubusercontent.com/googleapis/googleapis/master/google/rpc/status.proto --output protobuf/ledger-api/google/rpc/status.proto

$GrpcToolsVersion='2.35.0'
$GoogleProtobufToolsVersion='3.14.0'

# Install tooling (assumes nuget is installed and available)
nuget install Grpc.Tools -OutputDirectory packages -Version $GrpcToolsVersion
nuget install Google.Protobuf.Tools -OutputDirectory packages -Version $GoogleProtobufToolsVersion


# All seems valid so far so cleanup the old files before regnerating them

Remove-Item -Force -ErrorAction Ignore Directory.Build.props
Remove-Item -Force -ErrorAction Ignore src/Daml.Ledger.Api/V1/*.cs
Remove-Item -Force -ErrorAction Ignore src/Daml.Ledger.Api/V1/Admin/*.cs
Remove-Item -Force -ErrorAction Ignore src/Daml.Ledger.Api/V1/Testing/*.cs
Remove-Item -Force -ErrorAction Ignore src/Daml.Ledger.Fragment/DamlLf.cs
Remove-Item -Force -ErrorAction Ignore src/Daml.Ledger.Fragment/DamlLf0.cs
Remove-Item -Force -ErrorAction Ignore src/Daml.Ledger.Fragment/DamlLf1.cs

Set-Content -Path 'Directory.Build.props' -NoNewline -Value "<Project>`n  <PropertyGroup>`n    <Version>$damlSdkVersion</Version>`n  </PropertyGroup>`n</Project>`n"

Copy-Item -Path Directory.Build.props -Destination src/Daml.Ledger.Api/
Copy-Item -Path Directory.Build.props -Destination src/Daml.Ledger.Api.Data/
Copy-Item -Path Directory.Build.props -Destination src/Daml.Ledger.Automation/
Copy-Item -Path Directory.Build.props -Destination src/Daml.Ledger.Client/
Copy-Item -Path Directory.Build.props -Destination src/Daml.Ledger.Client.Reactive/
Copy-Item -Path Directory.Build.props -Destination src/Daml.Ledger.Examples.Bindings/
Copy-Item -Path Directory.Build.props -Destination src/Daml.Ledger.Examples.Test/
Copy-Item -Path Directory.Build.props -Destination src/Daml.Ledger.Fragment/

# Generate bindings

$ToolsProtoPath="packages/Google.Protobuf.Tools.$GoogleProtobufToolsVersion/tools/"

if ($IsLinux) {
    $ProtoC="packages/Grpc.Tools.$GrpcToolsVersion/tools/linux_x64/protoc --proto_path $ToolsProtoPath --plugin=protoc-gen-grpc=packages/Grpc.Tools.$GrpcToolsVersion/tools/linux_x64/grpc_csharp_plugin"
}
elseif ($IsMacOS) {
    $ProtoC="packages/Grpc.Tools.$GrpcToolsVersion/tools/macosx_x64/protoc --proto_path $ToolsProtoPath --plugin=protoc-gen-grpc=packages/Grpc.Tools.$GrpcToolsVersion/tools/macosx_x64/grpc_csharp_plugin"
}
else {
    $ProtoC="packages/Grpc.Tools.$GrpcToolsVersion/tools/windows_x64/protoc.exe --proto_path $ToolsProtoPath --plugin=protoc-gen-grpc=packages/Grpc.Tools.$GrpcToolsVersion/tools/windows_x64/grpc_csharp_plugin.exe"
}

Invoke-Expression "$ProtoC --proto_path protobuf/ledger-api/ --csharp_out ./src/Daml.Ledger.Api/V1         --grpc_out ./src/Daml.Ledger.Api/V1         protobuf/ledger-api/com/daml/ledger/api/v1/*.proto"
Invoke-Expression "$ProtoC --proto_path protobuf/ledger-api/ --csharp_out ./src/Daml.Ledger.Api/V1/Admin   --grpc_out ./src/Daml.Ledger.Api/V1/Admin   protobuf/ledger-api/com/daml/ledger/api/v1/admin/*.proto"
Invoke-Expression "$ProtoC --proto_path protobuf/ledger-api/ --csharp_out ./src/Daml.Ledger.Api/V1/Testing --grpc_out ./src/Daml.Ledger.Api/V1/Testing protobuf/ledger-api/com/daml/ledger/api/v1/testing/*.proto"
Invoke-Expression "$ProtoC --proto_path protobuf/daml-lf     --csharp_out ./src/Daml.Ledger.Fragment       --grpc_out ./src/Daml.Ledger.Fragment       protobuf/daml-lf/com/$damlLfNamespace/daml_lf_$damlLfVersion/*.proto"

# Cleanup
Remove-Item -Force -Recurse tmp
