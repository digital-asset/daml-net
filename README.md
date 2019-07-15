﻿# .Net bindings for DAML

This repository hosts .Net bindings for the [DAML](https://www.daml.com) Ledger API and DAML-LF.

# Status

The library is considered experimental, so breaking changes are to be expected. It currently consists of a thin layer of interfaces and helper classes around the generated gRPC code. A minimal implementation of stateful and stateless automation bots is also provided.

# Support

This project is a community-driven effort and contributions are welcome. For questions or support with using the library please join the [DAML Community Slack channel](https://damldriven.slack.com/)

# Usage

## Prerequisites

- `nuget`
- `msbuild`

## Building the library

The library targets `.Net Standard 2.0` so it can be used from projects targeting the .Net Framework, .Net Core or Mono.  Note that the local build process has only been tested on MacOS Mojave (v10.14.5).

To build the solution using `msbuild`:
```
nuget restore
msbuild Daml.Ledger.sln
```

To build the solution using `dotnet`:
```
dotnet build
```

Nuget packages are not yet published for this library, so you'll have to reference the generated DLL directly.

## Generating the bindings 

The script uses the `nuget`, `curl` and `tar` utilities.

Note: the `generate-bindings` script has only been tested on MacOS Mojave (v10.14.5). 

To generate bindings for the Ledger API and DAML-LF for a specific DAML SDK version:
```
./generate-bindings $SDK_VERSION
```

For example:
```
./generate-bindings 0.13.10
```

This will download all required tools, generate bindings for the Ledger API as well as DAML-LF, and clean up afterwards.
