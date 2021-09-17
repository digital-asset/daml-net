# .Net bindings for DAML

This repository hosts .Net bindings for the [DAML](https://www.daml.com) Ledger API and DAML-LF.

The DAML SDK that these bindings target is contained in the file **Directory.Build.props**.

# Status

The library is considered experimental, so breaking changes are to be expected. It currently consists of a thin layer of interfaces and helper classes around the generated gRPC code. A minimal implementation of stateful and stateless automation bots is also provided.

The library targets .Net 5 so .Net Framework and Mono support has been abandoned.

The build process has been tested on 64bit Windows 10, MacOS Catalina (v10.15.2), and Ubuntu 18.0.03 LTS (under Hyper-V).

# Support

This project is a community-driven effort and contributions are welcome. If you are in need of support, have questions or just want to engage in friendly conversation anything Daml, contact us on our [Daml Community Forum](https://discuss.daml.com).

# Usage

The repo as cloned contains the C# source generated from the DAML Ledger API and DAML-LF gRPC files and so can build the bindings directly. The version of the DAML SDK that the gRPC files are from is contained in the file **Directory.Build.props**.

However a **generate-bindings.ps1** script is provided that can be used to install locally a different (or the same) version of the gRPC files and then regenerate the C# source from them.

Running this script has the added benefit of providing local browsability of the gRPC files and when using Visual Studio extra links to these files are provided for the **Daml.Ledger.Api** and **Daml.Ledger.Fragment** projects. In addition the Visual Studio and Visual Studio Code IDEs can dynamically compile the gRPC files if they are changed.

The Daml.Ledger solution references the following projects:
```
  Daml.Ledger.Api               - the C# generated from the DAML Ledger API gRPC files
  Daml.Ledger.Fragment          - the C# generated from the DAML-LF gRPC files
  Daml.Ledger.Client            - a client wrapper around the Ledger API classes
  Daml.Ledger.Automation        - a minimal implementation of stateful and stateless automation bots
  Daml.Ledger.Api.Data          - immutable wrappers around the generated gRPC types that are more suitable for use with reactive streams
  Daml.Ledger.Client.Reactive   - a rudimentary wrapper of Daml.Ledger.Client to provide a more reactive interface
  Daml.Ledger.Examples.Test     - an example project utilising the Test DAML template
  Daml.Ledger.Examples.Bindings - an example project demonstrating some of the Bindings - based on the ex-java-bindings example project
```

Nuget packages can now be created using the build configuration **ReleaseNuget** (but haven't been published) for the following:
```
  Daml.Ledger.Fragment.<ver>.nupkg
  Daml.Ledger.Api.<ver>.nupkg 
  Daml.Ledger.Client.<ver>.nupkg
  Daml.Ledger.Client.Reactive.<ver>.nupkg
  Daml.Ledger.Automation.<ver>.nupkg

  where <ver> is the version of the Ledger API the bindings are built for, as listed in the file `Directory.Build.props`.
```
Nuget packages are built for several of the projects and are placed in the **packages** folder, and a **nuget.config** will add this path to the nuget search process.

Note that when making source changes and rebuiding, or changing releases, then the previous versions of the nuget packages will likely be in the nuget cache and dependencies will be resolved from there in preference to the **packages** folder. 

Therefore you may have to flush the nuget cache (for example `nuget locals all -clear` to clear the whole cache, or on Ubuntu delete the cached packages from your **~/.nuget folder**) in order to refresh the nuget cache.  

Builds of all projects that have dependencies use nuget package references for the **ReleaseNuget** build configuration, but project references for the Debug build to ease debugging.

## Prerequisites

The relevant dotnet SDK should be installed (**5** for this release) in order to build the bindings.

If the **generate-bindings** script is to be run then **Powershell Version 7** and the command line version of **nuget** must be installed.

## Platform Notes

### Mac

Many development tools depend on the xcode command line tools, which can be installed with the command `xcode-select --install`.

The **Homebrew** package manager is also used for many of the development tools on the Mac.

However before **Homebrew** is installed the **Mono** framework should be installed to avoid problems with Mono library version mismatches when using Visual Studio Code. See https://www.mono-project.com/download/stable/ where the Visual Studio channel release is probably the best choice.

**Homebrew** can then be installed from https://brew.sh/, and https://docs.brew.sh/FAQ has instructions to uninstall it if problems are found with the Mono library versions and you need to experiment with alternate releases of Mono.

**Powershell** for Mac can be installed by following instructions at https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell-core-on-macos

**Nuget** can then be installed using `Homebrew`.

You may have issues installing certain packages because of signing. In these cases it is often better to save the package and then right-click on the package in the Download folder in the Finder and choose Open.

### Windows

The **generate-bindings** script executes `nuget.exe` and so the Windows command line version of nuget shuld be installed from https://www.nuget.org/downloads

The default version of **Powershell** on Windows 10 is 5, so version 7 should be installed from the Windows Store.

Note that **Windows Terminal** will still default to using Powershell 5, so to change this select the dropdown to the right of the terminal tab, select **Settings** to open the json settings file, locate the entry for **Powershell 7** and copy the **guid** to replace the **defaultProfile** value at the top of the file. 

### Ubuntu

The dotnet 5.0 SDK can be installed by following the instructions for your platform at https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu

**Powershell** for Linux can be installed by following instructions at https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell-core-on-linux

Before installing **nuget** the mono repository should be added to the system.

See the notes at https://www.mono-project.com/download/preview/#download-lin-ubuntu.

For 18.04 the following commands can be run:

```
sudo apt install gnupg ca-certificates
sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
echo "deb https://download.mono-project.com/repo/ubuntu preview-bionic main" | sudo tee /etc/apt/sources.list.d/mono-official-preview.list
sudo apt update
sudo apt-get install nuget
```

## Building the libraries/Examples

The **Debug** and **Release** configuration of the projects can be built using the **Daml.Ledger.sln** solution file, or the **Daml.Ledger.Builder.csproj** file:
```
dotnet build Daml.Ledger.sln -c Debug|Release
```
or 
```
dotnet build Daml.Ledger.Builder.csproj -c Debug|Release
```
The **ReleaseNuget** configuration of the projects should only be built using the **Daml.Ledger.Builder.csproj** file as this will enforce a strict ordering on the build which will ensure that the required nuget packages are available at the correct stages of the build:
```
dotnet build Daml.Ledger.Builder.csproj -c ReleaseNuget
```
Building the **ReleaseNuget** configuration with either **Daml.Ledger.sln** or Visual Studio may or may not work depending on the presence or not of the base nuget packages (e.g. **Daml.Ledger.Api**), and in any case Visual Studio appears to be inconsistent in being able to resolve the project interdependencies.

## (Re) Generating the bindings

The **generate-bindings** script requires **Powershell** to run, see the Platform Notes for Mac and Ubuntu.

This will download all required tools and the gRPC files and then generate the bindings for the Ledger API and DAML-LF, and then clean up afterwards.

The required tools are downloaded to the **packages** folder and the gRPC files are downloaded to the **protobuf** folder.

The DAML SDK version is also written to the file **Directory.Build.props** in the current directory and all project directories that reference the SDK version.


To generate bindings for the Ledger API and DAML-LF for a specific DAML SDK version:
```
./generate-bindings.ps1 $SDK_VERSION [$DAML_LF_VERSION]
```

For example:
```
./generate-bindings.ps1 1.10.0
```

This has been tested for DAML SDK versions 1.0.0 through 1.10.0.

Optionally the DAML LF version can also be specified. This currently defaults to **1.8** but as of SDK release **1.10.0** the LF version **1.11** is stable. However, the namespaces in the **Daml.Ledger.Fragment** project and the package linkages in the example projects will need updating.

## Examples

Two examples are provided :
```
Daml.Ledger.Examples.Test     - implements an example based on the Test template used in `daml new`
Daml.Ledger.Examples.Bindings - a port of the  [ex-java-bindings](https://github.com/digital-asset/ex-java-bindings) example
