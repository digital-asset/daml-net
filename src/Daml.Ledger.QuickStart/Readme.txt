This was an attempt to port the ex-java-examples project but only the grpc example has been ported due to porting the 
reactive interface being somewhat convoluted as the rx java library and rx.net libraries have diverged from the 
reactive spec.

The example should be run by starting the sandbox with 

daml start

and then running the c# with

dotnet run grpc localhost 6865

The project relies on two nuget packages which are pointed to by the nuget.config file - the daml archive should be 
checked out as a sibling to this project.