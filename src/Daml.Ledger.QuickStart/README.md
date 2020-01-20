# Quick Start

The Quickstart project shows how to read contracts and transactions from the ledger and how to send create and exercise commands. It assumes the standard skeleton DAML model created with `daml new`.

First, open a shell and create a new project (anywhere):
```
daml new test
```

Then, change to the test folder and build the DAR package:
```
daml build
```
Inspect the DAR to retrieve the package identifier of the compiled package:
```
daml damlc inspect-dar .daml/dist/test-<ver>.dar
```
where ver is the version reported from the daml-build output.

This will print out information about the package:
```
DAR archive contains the following files:

META-INF/MANIFEST.MF
test.dalf
test/Main.daml
test/Main.hi
test/Main.hie
daml-prim.dalf
daml-stdlib.dalf
data/test.conf

DAR archive contains the following packages:

daml-prim-662f4f2e953020537a8b47ebad880ebdd3be8a9110fc9f4cd45863b54d6c7d5c "662f4f2e953020537a8b47ebad880ebdd3be8a9110fc9f4cd45863b54d6c7d5c"
daml-stdlib-33eff689d79d36ff70855fea91cbacdf41e75d61d28b67809fcf9f6845b942d7 "33eff689d79d36ff70855fea91cbacdf41e75d61d28b67809fcf9f6845b942d7"
test-<ver>-91d382196ba52dfc4af0e0cce7e52154b5117c75c83877ba600381a86169b4f7 "91d382196ba52dfc4af0e0cce7e52154b5117c75c83877ba600381a86169b4f7"
```
Note down the package identifier for package `test-<ver>` (here 91d382196ba52dfc4af0e0cce7e52154b5117c75c83877ba600381a86169b4f7) as you will pass it as a command line argument to the quickstart project.

Start the DAML Sandbox:
```
daml start
```

Open another shell in the `src/Daml.Ledger.QuickStart` folder.

Now you can start the quickstart program passing in the package identifier from above:
```
dotnet run localhost 6865 91d382196ba52dfc4af0e0cce7e52154b5117c75c83877ba600381a86169b4f7 Alice Bob

(use dotnet run -c Release to run the release configuration which uses nuget references - so ensure that the whole Daml.Ledger solution has first been built with `dotnet build -c Release`)
```

The resulting output shows information about the ledger and its transactions:
```
Ledger identity: sandbox-a8eff22a-6ca7-4346-a637-660fcae664c5
Participant identitiy: sandbox-participant
Package detail: { "packageId": "3d08a012eddfc136e8961845dbb60f48eabd5715b700b0339fd25295ac13d4ac", "packageSize": "17816", "knownSince": "1970-01-01T00:00:00Z" }
Package detail: { "packageId": "662f4f2e953020537a8b47ebad880ebdd3be8a9110fc9f4cd45863b54d6c7d5c", "packageSize": "940451", "knownSince": "1970-01-01T00:00:00Z" }
Package detail: { "packageId": "33eff689d79d36ff70855fea91cbacdf41e75d61d28b67809fcf9f6845b942d7", "packageSize": "980078", "knownSince": "1970-01-01T00:00:00Z" }
Party detail: { "party": "Alice", "isLocal": true }
Party detail: { "party": "Bob", "isLocal": true }
ActiveContract: { "eventId": "#2:1", "contractId": "#2:1", "templateId": { "packageId": "3d08a012eddfc136e8961845dbb60f48eabd5715b700b0339fd25295ac13d4ac", "moduleName": "Main", "entityName": "Asset" }, "createArguments": { "recordId": { "packageId": "3d08a012eddfc136e8961845dbb60f48eabd5715b700b0339fd25295ac13d4ac", "moduleName": "Main", "entityName": "Asset" }, "fields": [ { "label": "issuer", "value": { "party": "Alice" } }, { "label": "owner", "value": { "party": "Alice" } }, { "label": "name", "value": { "text": "TV" } } ] }, "witnessParties": [ "Alice" ] }
Transaction: { "transactionId": "scenario-transaction-0", "commandId": "scenario-transaction-0", "workflowId": "scenario-workflow-0", "effectiveAt": "1970-01-01T00:00:00Z", "events": [ { "created": { "eventId": "#0:0", "contractId": "#0:0", "templateId": { "packageId": "3d08a012eddfc136e8961845dbb60f48eabd5715b700b0339fd25295ac13d4ac", "moduleName": "Main", "entityName": "Asset" }, "createArguments": { "recordId": { "packageId": "3d08a012eddfc136e8961845dbb60f48eabd5715b700b0339fd25295ac13d4ac", "moduleName": "Main", "entityName": "Asset" }, "fields": [ { "label": "issuer", "value": { "party": "Alice" } }, { "label": "owner", "value": { "party": "Alice" } }, { "label": "name", "value": { "text": "TV" } } ] }, "witnessParties": [ "Alice" ], "agreementText": "", "signatories": [ "Alice" ] } } ], "offset": "1" }
Transaction: { "transactionId": "scenario-transaction-1", "commandId": "scenario-transaction-1", "workflowId": "scenario-workflow-1", "effectiveAt": "1970-01-01T00:00:00Z", "events": [ { "archived": { "eventId": "#1:0", "contractId": "#0:0", "templateId": { "packageId": "3d08a012eddfc136e8961845dbb60f48eabd5715b700b0339fd25295ac13d4ac", "moduleName": "Main", "entityName": "Asset" }, "witnessParties": [ "Alice" ] } }, { "created": { "eventId": "#1:1", "contractId": "#1:1", "templateId": { "packageId": "3d08a012eddfc136e8961845dbb60f48eabd5715b700b0339fd25295ac13d4ac", "moduleName": "Main", "entityName": "Asset" }, "createArguments": { "recordId": { "packageId": "3d08a012eddfc136e8961845dbb60f48eabd5715b700b0339fd25295ac13d4ac", "moduleName": "Main", "entityName": "Asset" }, "fields": [ { "label": "issuer", "value": { "party": "Alice" } }, { "label": "owner", "value": { "party": "Bob" } }, { "label": "name", "value": { "text": "TV" } } ] }, "witnessParties": [ "Alice" ], "agreementText": "", "signatories": [ "Alice" ], "observers": [ "Bob" ] } } ], "offset": "2" }
Transaction: { "transactionId": "scenario-transaction-2", "workflowId": "scenario-workflow-2", "effectiveAt": "1970-01-01T00:00:00Z", "events": [ { "archived": { "eventId": "#2:0", "contractId": "#1:1", "templateId": { "packageId": "3d08a012eddfc136e8961845dbb60f48eabd5715b700b0339fd25295ac13d4ac", "moduleName": "Main", "entityName": "Asset" }, "witnessParties": [ "Alice" ] } }, { "created": { "eventId": "#2:1", "contractId": "#2:1", "templateId": { "packageId": "3d08a012eddfc136e8961845dbb60f48eabd5715b700b0339fd25295ac13d4ac", "moduleName": "Main", "entityName": "Asset" }, "createArguments": { "recordId": { "packageId": "3d08a012eddfc136e8961845dbb60f48eabd5715b700b0339fd25295ac13d4ac", "moduleName": "Main", "entityName": "Asset" }, "fields": [ { "label": "issuer", "value": { "party": "Alice" } }, { "label": "owner", "value": { "party": "Alice" } }, { "label": "name", "value": { "text": "TV" } } ] }, "witnessParties": [ "Alice" ], "agreementText": "", "signatories": [ "Alice" ] } } ], "offset": "3" }
Command: { "create": { "templateId": { "packageId": "3d08a012eddfc136e8961845dbb60f48eabd5715b700b0339fd25295ac13d4ac", "moduleName": "Main", "entityName": "Asset" }, "createArguments": { "fields": [ { "label": "issuer", "value": { "party": "Alice" } }, { "label": "owner", "value": { "party": "Alice" } }, { "label": "name", "value": { "text": "TV" } } ] } } }
Transaction: { "transactionId": "3", "commandId": "e47bb161-f804-4f0f-ae7f-43f486a0d02e", "workflowId": "myWorkflowId", "effectiveAt": "1970-01-01T00:00:00Z", "events": [ { "created": { "eventId": "#3:0", "contractId": "#3:0", "templateId": { "packageId": "3d08a012eddfc136e8961845dbb60f48eabd5715b700b0339fd25295ac13d4ac", "moduleName": "Main", "entityName": "Asset" }, "createArguments": { "recordId": { "packageId": "3d08a012eddfc136e8961845dbb60f48eabd5715b700b0339fd25295ac13d4ac", "moduleName": "Main", "entityName": "Asset" }, "fields": [ { "label": "issuer", "value": { "party": "Alice" } }, { "label": "owner", "value": { "party": "Alice" } }, { "label": "name", "value": { "text": "TV" } } ] }, "witnessParties": [ "Alice" ], "agreementText": "", "signatories": [ "Alice" ] } } ], "offset": "4" }
Command: { "exercise": { "templateId": { "packageId": "3d08a012eddfc136e8961845dbb60f48eabd5715b700b0339fd25295ac13d4ac", "moduleName": "Main", "entityName": "Asset" }, "contractId": "#3:0", "choice": "Give", "choiceArgument": { "record": { "fields": [ { "label": "newOwner", "value": { "party": "Bob" } } ] } } } }
Transaction: { "transactionId": "4", "commandId": "6e3ef957-7864-4ad5-9d97-bc77b047d36c", "workflowId": "myWorkflowId", "effectiveAt": "1970-01-01T00:00:00Z", "events": [ { "archived": { "eventId": "#4:0", "contractId": "#3:0", "templateId": { "packageId": "3d08a012eddfc136e8961845dbb60f48eabd5715b700b0339fd25295ac13d4ac", "moduleName": "Main", "entityName": "Asset" }, "witnessParties": [ "Alice" ] } }, { "created": { "eventId": "#4:1", "contractId": "#4:1", "templateId": { "packageId": "3d08a012eddfc136e8961845dbb60f48eabd5715b700b0339fd25295ac13d4ac", "moduleName": "Main", "entityName": "Asset" }, "createArguments": { "recordId": { "packageId": "3d08a012eddfc136e8961845dbb60f48eabd5715b700b0339fd25295ac13d4ac", "moduleName": "Main", "entityName": "Asset" }, "fields": [ { "label": "issuer", "value": { "party": "Alice" } }, { "label": "owner", "value": { "party": "Bob" } }, { "label": "name", "value": { "text": "TV" } } ] }, "witnessParties": [ "Alice" ], "agreementText": "", "signatories": [ "Alice" ], "observers": [ "Bob" ] } } ], "offset": "5" }
```
