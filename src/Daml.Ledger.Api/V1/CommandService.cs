// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: com/daml/ledger/api/v1/command_service.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Com.Daml.Ledger.Api.V1 {

  /// <summary>Holder for reflection information generated from com/daml/ledger/api/v1/command_service.proto</summary>
  public static partial class CommandServiceReflection {

    #region Descriptor
    /// <summary>File descriptor for com/daml/ledger/api/v1/command_service.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CommandServiceReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cixjb20vZGFtbC9sZWRnZXIvYXBpL3YxL2NvbW1hbmRfc2VydmljZS5wcm90",
            "bxIWY29tLmRhbWwubGVkZ2VyLmFwaS52MRolY29tL2RhbWwvbGVkZ2VyL2Fw",
            "aS92MS9jb21tYW5kcy5wcm90bxoqY29tL2RhbWwvbGVkZ2VyL2FwaS92MS90",
            "cmFjZV9jb250ZXh0LnByb3RvGihjb20vZGFtbC9sZWRnZXIvYXBpL3YxL3Ry",
            "YW5zYWN0aW9uLnByb3RvGhtnb29nbGUvcHJvdG9idWYvZW1wdHkucHJvdG8i",
            "iAEKFFN1Ym1pdEFuZFdhaXRSZXF1ZXN0EjIKCGNvbW1hbmRzGAEgASgLMiAu",
            "Y29tLmRhbWwubGVkZ2VyLmFwaS52MS5Db21tYW5kcxI8Cg10cmFjZV9jb250",
            "ZXh0GOgHIAEoCzIkLmNvbS5kYW1sLmxlZGdlci5hcGkudjEuVHJhY2VDb250",
            "ZXh0Ij8KJVN1Ym1pdEFuZFdhaXRGb3JUcmFuc2FjdGlvbklkUmVzcG9uc2US",
            "FgoOdHJhbnNhY3Rpb25faWQYASABKAkiXwojU3VibWl0QW5kV2FpdEZvclRy",
            "YW5zYWN0aW9uUmVzcG9uc2USOAoLdHJhbnNhY3Rpb24YASABKAsyIy5jb20u",
            "ZGFtbC5sZWRnZXIuYXBpLnYxLlRyYW5zYWN0aW9uImcKJ1N1Ym1pdEFuZFdh",
            "aXRGb3JUcmFuc2FjdGlvblRyZWVSZXNwb25zZRI8Cgt0cmFuc2FjdGlvbhgB",
            "IAEoCzInLmNvbS5kYW1sLmxlZGdlci5hcGkudjEuVHJhbnNhY3Rpb25UcmVl",
            "MpQECg5Db21tYW5kU2VydmljZRJVCg1TdWJtaXRBbmRXYWl0EiwuY29tLmRh",
            "bWwubGVkZ2VyLmFwaS52MS5TdWJtaXRBbmRXYWl0UmVxdWVzdBoWLmdvb2ds",
            "ZS5wcm90b2J1Zi5FbXB0eRKMAQodU3VibWl0QW5kV2FpdEZvclRyYW5zYWN0",
            "aW9uSWQSLC5jb20uZGFtbC5sZWRnZXIuYXBpLnYxLlN1Ym1pdEFuZFdhaXRS",
            "ZXF1ZXN0Gj0uY29tLmRhbWwubGVkZ2VyLmFwaS52MS5TdWJtaXRBbmRXYWl0",
            "Rm9yVHJhbnNhY3Rpb25JZFJlc3BvbnNlEogBChtTdWJtaXRBbmRXYWl0Rm9y",
            "VHJhbnNhY3Rpb24SLC5jb20uZGFtbC5sZWRnZXIuYXBpLnYxLlN1Ym1pdEFu",
            "ZFdhaXRSZXF1ZXN0GjsuY29tLmRhbWwubGVkZ2VyLmFwaS52MS5TdWJtaXRB",
            "bmRXYWl0Rm9yVHJhbnNhY3Rpb25SZXNwb25zZRKQAQofU3VibWl0QW5kV2Fp",
            "dEZvclRyYW5zYWN0aW9uVHJlZRIsLmNvbS5kYW1sLmxlZGdlci5hcGkudjEu",
            "U3VibWl0QW5kV2FpdFJlcXVlc3QaPy5jb20uZGFtbC5sZWRnZXIuYXBpLnYx",
            "LlN1Ym1pdEFuZFdhaXRGb3JUcmFuc2FjdGlvblRyZWVSZXNwb25zZUJLChZj",
            "b20uZGFtbC5sZWRnZXIuYXBpLnYxQhhDb21tYW5kU2VydmljZU91dGVyQ2xh",
            "c3OqAhZDb20uRGFtbC5MZWRnZXIuQXBpLlYxYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Com.Daml.Ledger.Api.V1.CommandsReflection.Descriptor, global::Com.Daml.Ledger.Api.V1.TraceContextReflection.Descriptor, global::Com.Daml.Ledger.Api.V1.TransactionReflection.Descriptor, global::Google.Protobuf.WellKnownTypes.EmptyReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Com.Daml.Ledger.Api.V1.SubmitAndWaitRequest), global::Com.Daml.Ledger.Api.V1.SubmitAndWaitRequest.Parser, new[]{ "Commands", "TraceContext" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Com.Daml.Ledger.Api.V1.SubmitAndWaitForTransactionIdResponse), global::Com.Daml.Ledger.Api.V1.SubmitAndWaitForTransactionIdResponse.Parser, new[]{ "TransactionId" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Com.Daml.Ledger.Api.V1.SubmitAndWaitForTransactionResponse), global::Com.Daml.Ledger.Api.V1.SubmitAndWaitForTransactionResponse.Parser, new[]{ "Transaction" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Com.Daml.Ledger.Api.V1.SubmitAndWaitForTransactionTreeResponse), global::Com.Daml.Ledger.Api.V1.SubmitAndWaitForTransactionTreeResponse.Parser, new[]{ "Transaction" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// These commands are atomic, and will become transactions.
  /// </summary>
  public sealed partial class SubmitAndWaitRequest : pb::IMessage<SubmitAndWaitRequest>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<SubmitAndWaitRequest> _parser = new pb::MessageParser<SubmitAndWaitRequest>(() => new SubmitAndWaitRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SubmitAndWaitRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Com.Daml.Ledger.Api.V1.CommandServiceReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SubmitAndWaitRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SubmitAndWaitRequest(SubmitAndWaitRequest other) : this() {
      commands_ = other.commands_ != null ? other.commands_.Clone() : null;
      traceContext_ = other.traceContext_ != null ? other.traceContext_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SubmitAndWaitRequest Clone() {
      return new SubmitAndWaitRequest(this);
    }

    /// <summary>Field number for the "commands" field.</summary>
    public const int CommandsFieldNumber = 1;
    private global::Com.Daml.Ledger.Api.V1.Commands commands_;
    /// <summary>
    /// The commands to be submitted.
    /// Required
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Com.Daml.Ledger.Api.V1.Commands Commands {
      get { return commands_; }
      set {
        commands_ = value;
      }
    }

    /// <summary>Field number for the "trace_context" field.</summary>
    public const int TraceContextFieldNumber = 1000;
    private global::Com.Daml.Ledger.Api.V1.TraceContext traceContext_;
    /// <summary>
    /// Server side tracing will be registered as a child of the submitted context.
    /// This field is a future extension point and is currently not supported.
    /// Optional
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Com.Daml.Ledger.Api.V1.TraceContext TraceContext {
      get { return traceContext_; }
      set {
        traceContext_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SubmitAndWaitRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SubmitAndWaitRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Commands, other.Commands)) return false;
      if (!object.Equals(TraceContext, other.TraceContext)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (commands_ != null) hash ^= Commands.GetHashCode();
      if (traceContext_ != null) hash ^= TraceContext.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (commands_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Commands);
      }
      if (traceContext_ != null) {
        output.WriteRawTag(194, 62);
        output.WriteMessage(TraceContext);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (commands_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Commands);
      }
      if (traceContext_ != null) {
        output.WriteRawTag(194, 62);
        output.WriteMessage(TraceContext);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (commands_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Commands);
      }
      if (traceContext_ != null) {
        size += 2 + pb::CodedOutputStream.ComputeMessageSize(TraceContext);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SubmitAndWaitRequest other) {
      if (other == null) {
        return;
      }
      if (other.commands_ != null) {
        if (commands_ == null) {
          Commands = new global::Com.Daml.Ledger.Api.V1.Commands();
        }
        Commands.MergeFrom(other.Commands);
      }
      if (other.traceContext_ != null) {
        if (traceContext_ == null) {
          TraceContext = new global::Com.Daml.Ledger.Api.V1.TraceContext();
        }
        TraceContext.MergeFrom(other.TraceContext);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            if (commands_ == null) {
              Commands = new global::Com.Daml.Ledger.Api.V1.Commands();
            }
            input.ReadMessage(Commands);
            break;
          }
          case 8002: {
            if (traceContext_ == null) {
              TraceContext = new global::Com.Daml.Ledger.Api.V1.TraceContext();
            }
            input.ReadMessage(TraceContext);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            if (commands_ == null) {
              Commands = new global::Com.Daml.Ledger.Api.V1.Commands();
            }
            input.ReadMessage(Commands);
            break;
          }
          case 8002: {
            if (traceContext_ == null) {
              TraceContext = new global::Com.Daml.Ledger.Api.V1.TraceContext();
            }
            input.ReadMessage(TraceContext);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class SubmitAndWaitForTransactionIdResponse : pb::IMessage<SubmitAndWaitForTransactionIdResponse>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<SubmitAndWaitForTransactionIdResponse> _parser = new pb::MessageParser<SubmitAndWaitForTransactionIdResponse>(() => new SubmitAndWaitForTransactionIdResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SubmitAndWaitForTransactionIdResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Com.Daml.Ledger.Api.V1.CommandServiceReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SubmitAndWaitForTransactionIdResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SubmitAndWaitForTransactionIdResponse(SubmitAndWaitForTransactionIdResponse other) : this() {
      transactionId_ = other.transactionId_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SubmitAndWaitForTransactionIdResponse Clone() {
      return new SubmitAndWaitForTransactionIdResponse(this);
    }

    /// <summary>Field number for the "transaction_id" field.</summary>
    public const int TransactionIdFieldNumber = 1;
    private string transactionId_ = "";
    /// <summary>
    /// The id of the transaction that resulted from the submitted command.
    /// Must be a valid LedgerString (as described in ``value.proto``).
    /// Required
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string TransactionId {
      get { return transactionId_; }
      set {
        transactionId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SubmitAndWaitForTransactionIdResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SubmitAndWaitForTransactionIdResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (TransactionId != other.TransactionId) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (TransactionId.Length != 0) hash ^= TransactionId.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (TransactionId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(TransactionId);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (TransactionId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(TransactionId);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (TransactionId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(TransactionId);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SubmitAndWaitForTransactionIdResponse other) {
      if (other == null) {
        return;
      }
      if (other.TransactionId.Length != 0) {
        TransactionId = other.TransactionId;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            TransactionId = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            TransactionId = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class SubmitAndWaitForTransactionResponse : pb::IMessage<SubmitAndWaitForTransactionResponse>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<SubmitAndWaitForTransactionResponse> _parser = new pb::MessageParser<SubmitAndWaitForTransactionResponse>(() => new SubmitAndWaitForTransactionResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SubmitAndWaitForTransactionResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Com.Daml.Ledger.Api.V1.CommandServiceReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SubmitAndWaitForTransactionResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SubmitAndWaitForTransactionResponse(SubmitAndWaitForTransactionResponse other) : this() {
      transaction_ = other.transaction_ != null ? other.transaction_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SubmitAndWaitForTransactionResponse Clone() {
      return new SubmitAndWaitForTransactionResponse(this);
    }

    /// <summary>Field number for the "transaction" field.</summary>
    public const int TransactionFieldNumber = 1;
    private global::Com.Daml.Ledger.Api.V1.Transaction transaction_;
    /// <summary>
    /// The flat transaction that resulted from the submitted command.
    /// Required
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Com.Daml.Ledger.Api.V1.Transaction Transaction {
      get { return transaction_; }
      set {
        transaction_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SubmitAndWaitForTransactionResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SubmitAndWaitForTransactionResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Transaction, other.Transaction)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (transaction_ != null) hash ^= Transaction.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (transaction_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Transaction);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (transaction_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Transaction);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (transaction_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Transaction);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SubmitAndWaitForTransactionResponse other) {
      if (other == null) {
        return;
      }
      if (other.transaction_ != null) {
        if (transaction_ == null) {
          Transaction = new global::Com.Daml.Ledger.Api.V1.Transaction();
        }
        Transaction.MergeFrom(other.Transaction);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            if (transaction_ == null) {
              Transaction = new global::Com.Daml.Ledger.Api.V1.Transaction();
            }
            input.ReadMessage(Transaction);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            if (transaction_ == null) {
              Transaction = new global::Com.Daml.Ledger.Api.V1.Transaction();
            }
            input.ReadMessage(Transaction);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class SubmitAndWaitForTransactionTreeResponse : pb::IMessage<SubmitAndWaitForTransactionTreeResponse>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<SubmitAndWaitForTransactionTreeResponse> _parser = new pb::MessageParser<SubmitAndWaitForTransactionTreeResponse>(() => new SubmitAndWaitForTransactionTreeResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SubmitAndWaitForTransactionTreeResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Com.Daml.Ledger.Api.V1.CommandServiceReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SubmitAndWaitForTransactionTreeResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SubmitAndWaitForTransactionTreeResponse(SubmitAndWaitForTransactionTreeResponse other) : this() {
      transaction_ = other.transaction_ != null ? other.transaction_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SubmitAndWaitForTransactionTreeResponse Clone() {
      return new SubmitAndWaitForTransactionTreeResponse(this);
    }

    /// <summary>Field number for the "transaction" field.</summary>
    public const int TransactionFieldNumber = 1;
    private global::Com.Daml.Ledger.Api.V1.TransactionTree transaction_;
    /// <summary>
    /// The transaction tree that resulted from the submitted command.
    /// Required
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Com.Daml.Ledger.Api.V1.TransactionTree Transaction {
      get { return transaction_; }
      set {
        transaction_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SubmitAndWaitForTransactionTreeResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SubmitAndWaitForTransactionTreeResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Transaction, other.Transaction)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (transaction_ != null) hash ^= Transaction.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (transaction_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Transaction);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (transaction_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Transaction);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (transaction_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Transaction);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SubmitAndWaitForTransactionTreeResponse other) {
      if (other == null) {
        return;
      }
      if (other.transaction_ != null) {
        if (transaction_ == null) {
          Transaction = new global::Com.Daml.Ledger.Api.V1.TransactionTree();
        }
        Transaction.MergeFrom(other.Transaction);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            if (transaction_ == null) {
              Transaction = new global::Com.Daml.Ledger.Api.V1.TransactionTree();
            }
            input.ReadMessage(Transaction);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            if (transaction_ == null) {
              Transaction = new global::Com.Daml.Ledger.Api.V1.TransactionTree();
            }
            input.ReadMessage(Transaction);
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
