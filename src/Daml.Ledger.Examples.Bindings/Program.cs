// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

internal class Program
{
    static void Main(string[] args)
    {
        const string usage = "Usage : [option] [host] [port] where option is one of { grpc }";

        try
        {
            if (args.Length < 1)
                throw new ArgumentException($"Incorrect number of parameters supplied : {usage}");
            
            string option = args[0].ToLower();
            var adjustedArgs = new List<string>(args.Skip(1)).ToArray();

            switch (option)
            {
                case "grpc":
                    Task.Run(() => Daml.Ledger.Examples.Bindings.Grpc.PingPongGrpcMain.Main(adjustedArgs)).Wait();
                    break;
                case "reactive":
                    // Task.Run(() => Daml.Ledger.Examples.Bindings.Grpc.PingPongGrpcMain.Main(adjustedArgs)).Wait();
                    break;
                default:
                   throw new ArgumentException($"Invalid option supplied {args[0]} : {usage}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
            Environment.Exit(-1);
        }
    }
}
