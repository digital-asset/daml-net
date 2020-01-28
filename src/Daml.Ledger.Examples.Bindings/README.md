# Bindings

This project is a C# version of the [ex-java-bindings](https://github.com/digital-asset/ex-java-bindings) project but with this release only the grpc example has been implemented. See the readme in that repo for more information.

This is an example of how a C# application would use the C# Binding library to connect to and exercise a DAML model running on a ledger. Since there are three levels of interface available, this example builds a similar application with all three levels.

The application is a simple PingPong application, which consists of:

    a DAML model with two contract templates, Ping and Pong
    two parties, Alice and Bob

The logic of the application is the following:

    The application injects a contract of type Ping for Alice.
    Alice sees this contract and exercises the consuming choice RespondPong to create a contract of type Pong for Bob.
    Bob sees this contract and exercises the consuming choice RespondPing to create a contract of type Ping for Alice.
    Points 1 and 2 are repeated until the maximum number of contracts defined in the DAML is reached.

To run the project open a shell in the Daml.Ledger.Examples.Bindings folder and build the DAR package:
```
daml build
```
Then start the Sandbox and Navigator with:
```
daml start
```
Open another shell in the Daml.Ledger.Examples.Bindings folder and run the project with:
```
dotnet run grpc localhost 6865
```
The resulting output shows the interaction between Bob and Alice:
```
Alice starts reading transactions.
Bob starts reading transactions.
Alice is exercising RespondPong on #0:0 in workflow Ping-Bob-8 at count 0
Bob is exercising RespondPong on #2:0 in workflow Ping-Alice-7 at count 0
Bob is exercising RespondPong on #3:0 in workflow Ping-Alice-4 at count 0
Alice is exercising RespondPong on #1:0 in workflow Ping-Bob-9 at count 0
Bob is exercising RespondPong on #4:0 in workflow Ping-Alice-0 at count 0
Alice is exercising RespondPong on #6:0 in workflow Ping-Bob-4 at count 0
Bob is exercising RespondPong on #5:0 in workflow Ping-Alice-8 at count 0
Alice is exercising RespondPong on #7:0 in workflow Ping-Bob-1 at count 0
Alice is exercising RespondPong on #8:0 in workflow Ping-Bob-6 at count 0
Bob is exercising RespondPong on #9:0 in workflow Ping-Alice-9 at count 0
Alice is exercising RespondPong on #10:0 in workflow Ping-Bob-3 at count 0
Bob is exercising RespondPong on #11:0 in workflow Ping-Alice-6 at count 0
Alice is exercising RespondPong on #12:0 in workflow Ping-Bob-2 at count 0
Bob is exercising RespondPong on #13:0 in workflow Ping-Alice-5 at count 0
Bob is exercising RespondPong on #14:0 in workflow Ping-Alice-1 at count 0
Alice is exercising RespondPong on #16:0 in workflow Ping-Bob-5 at count 0
Bob is exercising RespondPong on #15:0 in workflow Ping-Alice-2 at count 0
Alice is exercising RespondPong on #17:0 in workflow Ping-Bob-0 at count 0
Bob is exercising RespondPong on #19:0 in workflow Ping-Alice-3 at count 0
Alice is exercising RespondPong on #18:0 in workflow Ping-Bob-7 at count 0
Bob is exercising RespondPing on #21:1 in workflow Ping-Bob-8 at count 1
Alice is exercising RespondPing on #20:1 in workflow Ping-Alice-7 at count 1
Bob is exercising RespondPing on #23:1 in workflow Ping-Bob-9 at count 1
Alice is exercising RespondPing on #22:1 in workflow Ping-Alice-4 at count 1
Bob is exercising RespondPing on #25:1 in workflow Ping-Bob-4 at count 1
Alice is exercising RespondPing on #24:1 in workflow Ping-Alice-0 at count 1
Bob is exercising RespondPing on #27:1 in workflow Ping-Bob-1 at count 1
Alice is exercising RespondPing on #26:1 in workflow Ping-Alice-8 at count 1
Bob is exercising RespondPing on #28:1 in workflow Ping-Bob-6 at count 1
Alice is exercising RespondPing on #29:1 in workflow Ping-Alice-9 at count 1
Bob is exercising RespondPing on #30:1 in workflow Ping-Bob-3 at count 1
Alice is exercising RespondPing on #31:1 in workflow Ping-Alice-6 at count 1
Bob is exercising RespondPing on #32:1 in workflow Ping-Bob-2 at count 1
Alice is exercising RespondPing on #33:1 in workflow Ping-Alice-5 at count 1
Alice is exercising RespondPing on #34:1 in workflow Ping-Alice-1 at count 1
Bob is exercising RespondPing on #35:1 in workflow Ping-Bob-5 at count 1
Alice is exercising RespondPing on #36:1 in workflow Ping-Alice-2 at count 1
Bob is exercising RespondPing on #37:1 in workflow Ping-Bob-0 at count 1
Alice is exercising RespondPing on #38:1 in workflow Ping-Alice-3 at count 1
Bob is exercising RespondPing on #39:1 in workflow Ping-Bob-7 at count 1
Alice is exercising RespondPong on #40:1 in workflow Ping-Bob-8 at count 2
Bob is exercising RespondPong on #41:1 in workflow Ping-Alice-7 at count 2
Alice is exercising RespondPong on #42:1 in workflow Ping-Bob-9 at count 2
Bob is exercising RespondPong on #43:1 in workflow Ping-Alice-4 at count 2
Alice is exercising RespondPong on #44:1 in workflow Ping-Bob-4 at count 2
...
...
```
Log into the Navigator as Bob or Alice to see the archived Ledger Contracts.

See the readme in the [ex-java-bindings](https://github.com/digital-asset/ex-java-bindings) repo for more information.