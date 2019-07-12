# Overview

This is the home of the .Net ecosystem around the Ledger API and DAML-LF. It is a community-supported effort, so contributions are welcome.

## Updating the generated bindings to a new SDK version

To generate both, Ledger API and DAML-LF bindings, for a new DAML SDK version, run the script:
```
./generate-bindings $SDK_VERSION
```

For example:
```
./generate-bindings 0.13.10
```

This will download all required tools, generate bindings for the Ledger API as well as DAML-LF, and clean up afterwards.

There is no script for Windows yet, contributions for this are welcome.
