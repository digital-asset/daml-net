# .Net bindings for DAML

This repository hosts .Net bindings for the [DAML](https://www.daml.com) Ledger API and DAML-LF.

# Status

The library is considered experimental, so breaking changes are to be expected. It currently consists of a thin layer of interfaces and helper classes around the generated gRPC code. A minimal implementation of stateful and stateless automation bots is also provided.

The library currently targets `.Net Standard 2.0` so it can be used from projects targeting the .Net Framework, .Net Core or Mono.

The build process has been tested on 64bit Windows 10 (with WSl 1 for the bash shell), MacOS Mojave (v10.14.5) and Catalina (v10.15.2), and Ubuntu 18.04.3 (under Hyper-V).

# Support

This project is a community-driven effort and contributions are welcome. For questions or support with using the library please join the [DAML Community Slack channel](https://damldriven.slack.com/)

# Usage

The repo as cloned contains the C# source generated from the DAML Ledger API and DAML-LF gRPC files and so can build the bindings directly. The version of the DAML SDK that the gRPC files are from is contained in the file `Directory.Build.props`.

However a `generate-bindings` script is provided that can be used to install locally a different (or the same) version of the gRPC files and then regenerate the C# source from them.

Running this script has the added benefit of providing local browsability of the gRPC files and when using Visual Studio extra links to these files are provided for the Daml.Ledger.Api and Daml.Ledger.Fragment projects. In addition the
Visual Studio and Visual Studio Code IDEs can dynamically compile the gRPC files if they are changed.

The Daml.Ledger solution references the following projects:
```
  Daml.Ledger.Api               - the C# generated from the DAML Ledger API gRPC files
  Daml.Ledger.Fragment          - the C# generated from the DAML-LF gRPC files
  Daml.Ledger.Client            - a client wrapper around the Ledger API classes
  Daml.Ledger.Client.Reactive   - a rudimentary wrapper of Daml.Ledger.Client to provide a more reactive interface
  Daml.Ledger.Automation        - a minimal implementation of stateful and stateless automation bots
  Daml.Ledger.Quickstart        - an example project utilising the Test DAML template
```

Nuget packages are now created (but haven't been published) for the following:
```
  Daml.Ledger.Fragment.<ver>.nupkg
  Daml.Ledger.Api.<ver>.nupkg 
  Daml.Ledger.Client.<ver>.nupkg
  Daml.Ledger.Client.Reactive.<ver>.nupkg
  Daml.Ledger.Automation.<ver>.nupkg

  where <ver> is the version of the Ledger API the bindings are built for, as listed in the file `Directory.Build.props`.
```
Nuget packages are built for several of the projects and are placed in the `packages` folder, and a `nuget.config` will add this path to the nuget search process.

Note that when making source changes and rebuiding, or changing releases, then the previous versions of the nuget packages will likely be in the nuget cache and dependencies 
will be resolved from there in preference to the `packages` folder. 

Therefore you may have to flush the nuget cache (for example `nuget locals all -clear` to clear the whole cache, or on Ubuntu delete the cached packages from your ~/.nuget folder) in order to refresh the nuget cache.  

Release builds of all projects that have dependencies use nuget package references for the Release build, but project references for the Debug build to ease debugging.

When running the examples with `dotnet run` it appears that nuget linkages are used if available.

## Prerequisites

The relevant dotnet SDK should be installed (`2.2` for this release) in order to build the bindings.

Additionally a Bash shell and the command line version of `nuget` should be installed if the generate-bindings script is to be run.

## Platform Notes

### Mac

Most tools depend on the xcode command line tools, which can be installed with the command `xcode-select --install`.

Nuget can be installed using `Homebrew` but the `Mono` framework should be installed first to avoid problems with Mono library version mismatches when using
Visual Studio Code. See https://www.mono-project.com/download/stable/ where the Visual Studio channel release is probably the best choice.

Homebrew can be installed from https://brew.sh/ and https://docs.brew.sh/FAQ has instructions to uninstall it if problems are found with the Mono library versions
and you need to experiment with alternate releases of Mono.

You may have issues installing certain packages because of signing. In these cases it is often better to save the package and then right-click on the package in
the Download folder in the Finder and choose Open.

### Windows

The generate-bindings script assumes that it is being run under the bash shell of WSL. If another shell is used the environment detection logic will need
enhancing to detect the environment. See the script for where it performs tests with `uname -s`.

The generate-bindings script requires Unix line-endings (LF) in order to work in the bash shell, and an entry in the `.gitattributes` file ensures that
it will be checked out from `git` with these line endings whether it is cloned from either a WSL shell or a Windows shell. However if changes are made to the script then
care should be taken to not accidently introduce Windows line-endings (CRLF). `Notepad++` is a good editor for showing the line ending characters and being able to
convert them back to Unix line-endings if required.

The generate-bindings script assumes that nuget has been installed in the WSL environemt, as oppossed to using one installed in Windows.

Entries have not been added to the .gitattribute file to  maintain Unix line-endings for all other text files in case of unforeseen issues with Windows tools.

So therefore note that if you clone the repo under one type of shell (WSL or Windows) and then perform git operations in another type of shell, git may flag
up the line-ending differences as a change. So be consistent with which type of shell you perform your git operations from!

Also note that if Visual Studio Code is launched from the bash shell of WSL then it may complain that it requires the dotnet SDK to be installed in WSL in
order to build. This has not been tested so prefer to run VSC from a Windows shell.

## Building the library

To build the solution using `msbuild`:
```
nuget restore
msbuild Daml.Ledger.sln
```

To build the solution using `dotnet`:
```
dotnet build
```

## (Re) Generating the bindings

The script uses the `nuget`, `curl` and `tar` utilities.

To generate bindings for the Ledger API and DAML-LF for a specific DAML SDK version:
```
./generate-bindings $SDK_VERSION
```

For example:
```
./generate-bindings 0.13.38
```

This will download all required tools and the gRPC files and then generate the bindings for the Ledger API and DAML-LF, and then clean up afterwards.

The required tools are downloaded to the 'packages' folder and the gRPC files are downloaded to the 'protobuf' folder.

The DAML SDK version is also written to the file Directory.Build.props.

## Examples

Two examples are provided :
```
Daml.Ledger.Examples.Test     - implements an example based on the Test template used in `daml new`
Daml.Ledger.Examples.Bindings - a port of the  [ex-java-bindings](https://github.com/digital-asset/ex-java-bindings) example
