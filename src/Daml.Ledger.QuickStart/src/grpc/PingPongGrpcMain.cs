// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Grpc.Core;

using ArchivePayload= Com.DigitalAsset.Daml_Lf_1_7.DamlLf.ArchivePayload;
using Package= Com.DigitalAsset.Daml_Lf_1_7.DamlLf1.Package;

using Identifier = Com.DigitalAsset.Ledger.Api.V1.Identifier;
using CommandSubmissionService = Com.DigitalAsset.Ledger.Api.V1.CommandSubmissionService;
using Record = Com.DigitalAsset.Ledger.Api.V1.Record;
using RecordField = Com.DigitalAsset.Ledger.Api.V1.RecordField;
using Command = Com.DigitalAsset.Ledger.Api.V1.Command;
using Value = Com.DigitalAsset.Ledger.Api.V1.Value;
using CreateCommand = Com.DigitalAsset.Ledger.Api.V1.CreateCommand;
using SubmitRequest = Com.DigitalAsset.Ledger.Api.V1.SubmitRequest;
using Commands = Com.DigitalAsset.Ledger.Api.V1.Commands;
using LedgerIdentityService = Com.DigitalAsset.Ledger.Api.V1.LedgerIdentityService;
using GetLedgerIdentityRequest = Com.DigitalAsset.Ledger.Api.V1.GetLedgerIdentityRequest;
using PackageService = Com.DigitalAsset.Ledger.Api.V1.PackageService;
using ListPackagesResponse = Com.DigitalAsset.Ledger.Api.V1.ListPackagesResponse;
using ListPackagesRequest = Com.DigitalAsset.Ledger.Api.V1.ListPackagesRequest;
using GetPackageRequest = Com.DigitalAsset.Ledger.Api.V1.GetPackageRequest;

namespace Daml.Ledger.QuickStart.Grpc
{
    public class PingPongGrpcMain 
    {
        // application id used for sending commands
        public static readonly string APP_ID = "PingPongApp";

        // constants for referring to the parties
        public static readonly string ALICE = "Alice";
        public static readonly string BOB = "Bob";

        public static async Task Main(string[] args)
        {
            // Extract host and port from arguments
            if (args.Length < 2)
                throw new ArgumentException("Incorrect number of arguments supplied : Usage: grpc HOST PORT [NUM_INITIAL_CONTRACTS]");

            Channel channel = null;
            PingPongProcessor aliceProcessor = null;
            PingPongProcessor bobProcessor = null;

            string host = args[0];
            int port = Int32.Parse(args[1]);

            // each party will create this number of initial Ping contracts
            int numInitialContracts = args.Length == 3 ? Int32.Parse(args[2]) : 10;


            try
            {
                channel = new Channel($"{host}:{port}", ChannelCredentials.Insecure);

                // fetch the ledger ID, which is used in subsequent requests sent to the ledger
                string ledgerId = FetchLedgerId(channel);

                // inspect the packages on the ledger and extract the package id of the package containing the PingPong module
                // this is helpful during development when the package id changes a lot due to likely frequent changes to the DAML code
                string packageId = DetectPingPongPackageId(channel, ledgerId);

                Identifier pingIdentifier = new Identifier { PackageId = packageId, ModuleName = "PingPong", EntityName = "Ping" };

                Identifier pongIdentifier = new Identifier { PackageId = packageId, ModuleName = "PingPong", EntityName = "Pong" };

                // initialize the ping pong processors for Alice and Bob
                aliceProcessor = new PingPongProcessor(ALICE, ledgerId, channel, pingIdentifier, pongIdentifier);
                bobProcessor = new PingPongProcessor(BOB, ledgerId, channel, pingIdentifier, pongIdentifier);

                // start the processors asynchronously
                aliceProcessor.RunIndefinitely();
                bobProcessor.RunIndefinitely();
            
                // send the initial commands for both parties
                CreateInitialContracts(channel, ledgerId, ALICE, BOB, pingIdentifier, numInitialContracts);
                CreateInitialContracts(channel, ledgerId, BOB, ALICE, pingIdentifier, numInitialContracts);

                // wait a couple of seconds for the processing to finish
                Thread.Sleep(5000);
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("Unexpected exception in Main", ex);
            }
            finally
            {
                aliceProcessor?.Shutdown();
                bobProcessor?.Shutdown();

                if (channel != null)
                    try 
                    {
                        await channel.ShutdownAsync();
                    } 
                    catch (Exception ex) 
                    {
                        Console.Error.WriteLine($"Unexpected exception closing channel {ex.Message}");
                    }
            }
        }

        /// <summary>
        /// Creates numContracts number of Ping contracts.The sender is used as the submitting party.
        /// </summary>
        /// <param name="channel">the gRPC channel to use for services</param>
        /// <param name="ledgerId">the previously fetched ledger id</param>
        /// <param name="sender">the party that sends the initial Ping contract</param>
        /// <param name="receiver">the party that receives the initial Ping contract</param>
        /// <param name="pingIdentifier">the PingPong.Ping template identifier</param>
        /// <param name="numContracts">the number of initial contracts to create</param>
        private static void CreateInitialContracts(Channel channel, string ledgerId, string sender, string receiver, Identifier pingIdentifier, int numContracts)
        {
            var submissionService = new CommandSubmissionService.CommandSubmissionServiceClient(channel);

            var record = new Record { RecordId = pingIdentifier };                                  // the identifier for a template's record is the same as the identifier for the template
            record.Fields.Add(new [] { new RecordField { Label = "sender", Value = new Value { Party = sender } },
                                       new RecordField { Label = "receiver", Value = new Value { Party = receiver } },
                                       new RecordField { Label = "count", Value = new Value { Int64 = 0 } } });

             var createCommand = new Command { Create = new CreateCommand { TemplateId = pingIdentifier, CreateArguments = record } };

            for (int i = 0; i < numContracts; i++)
            {
                var submitRequest = new SubmitRequest { Commands = new Commands { LedgerId = ledgerId,   
                                                                                  // set current time to ledger effective time (LET)
                                                                                  LedgerEffectiveTime = new Google.Protobuf.WellKnownTypes.Timestamp { Seconds =  0 }, 
                                                                                  // set maximum record time (MRT) to now + 5s
                                                                                  MaximumRecordTime = new Google.Protobuf.WellKnownTypes.Timestamp { Seconds = 5 },
                                                                                  CommandId = Guid.NewGuid().ToString(),
                                                                                  WorkflowId = $"Ping-{sender}-{i}",
                                                                                  Party = sender,
                                                                                  ApplicationId = APP_ID } };
                submitRequest.Commands.Commands_.Add(createCommand);

                // asynchronously send the commands
                submissionService.SubmitAsync(submitRequest);
            }
        }
    
        /// <summary>
        /// Fetches the ledger id via the Ledger Identity Service.
        /// </summary>
        /// <param name="channel">the gRPC channel to use for services</param>
        /// <returns>the ledger id as provided by the ledger</returns>
        private static string FetchLedgerId(Channel channel) 
        {
            var ledgerIdentityService = new LedgerIdentityService.LedgerIdentityServiceClient(channel);

            var identityResponse = ledgerIdentityService.GetLedgerIdentity(new GetLedgerIdentityRequest());

            return identityResponse.LedgerId;
        }

        /// <summary>
        /// Inspects all DAML packages that are registered on the ledger and returns the id of the package that contains the PingPong module.
        /// This is useful during development when the DAML model changes a lot, so that the package id doesn't need to be updated manually
        /// after each change.
        /// </summary>
        /// <param name="channel">the gRPC channel to use for services</param>
        /// <param name="ledgerId">the ledger id to use for requests</param>
        /// <returns>the package id of the example DAML module</returns>
        private static String DetectPingPongPackageId(Channel channel, String ledgerId) 
        {
            var packageService = new PackageService.PackageServiceClient(channel);

            // fetch a list of all package ids available on the ledger
            ListPackagesResponse packagesResponse = packageService.ListPackages(new ListPackagesRequest { LedgerId = ledgerId });

            // find the package that contains the PingPong module
            foreach (string packageId in packagesResponse.PackageIds) 
            {
                var packageResponse = packageService.GetPackage(new GetPackageRequest { LedgerId = ledgerId, PackageId = packageId });

                try {
                    // parse the archive payload
                    ArchivePayload payload = ArchivePayload.Parser.ParseFrom(packageResponse.ArchivePayload);
                    // get the DAML LF package
                    Package lfPackage = payload.DamlLf1;
                    // check if the PingPong module is in the current package package
                    var pingPongModule = lfPackage.Modules.ToList().Find(m => m.NameDname.Segments.Contains("PingPong"));

                    if (pingPongModule != null)
                        return packageId;

                } catch (Google.Protobuf.InvalidProtocolBufferException ex) {
                    throw new ApplicationException($"Querying Package Service payload", ex);
                }
            }

            // No package on the ledger contained the PingPong module
            throw new ApplicationException("Module PingPong is not available on the ledger");
        }
    }    
}

