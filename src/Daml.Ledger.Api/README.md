## Generating the DAML-LF bindings

Navigate to the project root:
```
cd ../..
```
or
```
cd daml-net
```

Install the `Grpc.Tools` Nuget package:
```
nuget install Grpc.Tools -o packages
```
Check that the correct version of the `Grpc.Tools` package got installed. Below command assumed version 1.22.0.

Run the `protoc` tool:
```
../../packages/Grpc.Tools.1.22.0/tools/macosx_x64/protoc --proto_path ./protobuf --csharp_out . --grpc_out . ./protobuf/*.proto ./protobuf/da/*.proto --plugin=protoc-gen-grpc=../../packages/Grpc.Tools.1.22.0/tools/macosx_x64/grpc_csharp_plugin
../../packages/Grpc.Tools.1.22.0/tools/macosx_x64/protoc --proto_path ./protobuf/api/v1 --csharp_out ./V1 --grpc_out ./V1 --plugin=protoc-gen-grpc=../../packages/Grpc.Tools.1.12.0/tools/macosx_x64/grpc_csharp_plugin

```
