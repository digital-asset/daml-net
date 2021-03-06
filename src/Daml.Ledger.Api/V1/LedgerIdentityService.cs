// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: com/daml/ledger/api/v1/ledger_identity_service.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Com.Daml.Ledger.Api.V1 {

  /// <summary>Holder for reflection information generated from com/daml/ledger/api/v1/ledger_identity_service.proto</summary>
  public static partial class LedgerIdentityServiceReflection {

    #region Descriptor
    /// <summary>File descriptor for com/daml/ledger/api/v1/ledger_identity_service.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static LedgerIdentityServiceReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CjRjb20vZGFtbC9sZWRnZXIvYXBpL3YxL2xlZGdlcl9pZGVudGl0eV9zZXJ2",
            "aWNlLnByb3RvEhZjb20uZGFtbC5sZWRnZXIuYXBpLnYxGipjb20vZGFtbC9s",
            "ZWRnZXIvYXBpL3YxL3RyYWNlX2NvbnRleHQucHJvdG8iWAoYR2V0TGVkZ2Vy",
            "SWRlbnRpdHlSZXF1ZXN0EjwKDXRyYWNlX2NvbnRleHQY6AcgASgLMiQuY29t",
            "LmRhbWwubGVkZ2VyLmFwaS52MS5UcmFjZUNvbnRleHQiLgoZR2V0TGVkZ2Vy",
            "SWRlbnRpdHlSZXNwb25zZRIRCglsZWRnZXJfaWQYASABKAkykQEKFUxlZGdl",
            "cklkZW50aXR5U2VydmljZRJ4ChFHZXRMZWRnZXJJZGVudGl0eRIwLmNvbS5k",
            "YW1sLmxlZGdlci5hcGkudjEuR2V0TGVkZ2VySWRlbnRpdHlSZXF1ZXN0GjEu",
            "Y29tLmRhbWwubGVkZ2VyLmFwaS52MS5HZXRMZWRnZXJJZGVudGl0eVJlc3Bv",
            "bnNlQlIKFmNvbS5kYW1sLmxlZGdlci5hcGkudjFCH0xlZGdlcklkZW50aXR5",
            "U2VydmljZU91dGVyQ2xhc3OqAhZDb20uRGFtbC5MZWRnZXIuQXBpLlYxYgZw",
            "cm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Com.Daml.Ledger.Api.V1.TraceContextReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Com.Daml.Ledger.Api.V1.GetLedgerIdentityRequest), global::Com.Daml.Ledger.Api.V1.GetLedgerIdentityRequest.Parser, new[]{ "TraceContext" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Com.Daml.Ledger.Api.V1.GetLedgerIdentityResponse), global::Com.Daml.Ledger.Api.V1.GetLedgerIdentityResponse.Parser, new[]{ "LedgerId" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class GetLedgerIdentityRequest : pb::IMessage<GetLedgerIdentityRequest>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<GetLedgerIdentityRequest> _parser = new pb::MessageParser<GetLedgerIdentityRequest>(() => new GetLedgerIdentityRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<GetLedgerIdentityRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Com.Daml.Ledger.Api.V1.LedgerIdentityServiceReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetLedgerIdentityRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetLedgerIdentityRequest(GetLedgerIdentityRequest other) : this() {
      traceContext_ = other.traceContext_ != null ? other.traceContext_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetLedgerIdentityRequest Clone() {
      return new GetLedgerIdentityRequest(this);
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
      return Equals(other as GetLedgerIdentityRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(GetLedgerIdentityRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(TraceContext, other.TraceContext)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
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
      if (traceContext_ != null) {
        size += 2 + pb::CodedOutputStream.ComputeMessageSize(TraceContext);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(GetLedgerIdentityRequest other) {
      if (other == null) {
        return;
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

  public sealed partial class GetLedgerIdentityResponse : pb::IMessage<GetLedgerIdentityResponse>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<GetLedgerIdentityResponse> _parser = new pb::MessageParser<GetLedgerIdentityResponse>(() => new GetLedgerIdentityResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<GetLedgerIdentityResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Com.Daml.Ledger.Api.V1.LedgerIdentityServiceReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetLedgerIdentityResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetLedgerIdentityResponse(GetLedgerIdentityResponse other) : this() {
      ledgerId_ = other.ledgerId_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetLedgerIdentityResponse Clone() {
      return new GetLedgerIdentityResponse(this);
    }

    /// <summary>Field number for the "ledger_id" field.</summary>
    public const int LedgerIdFieldNumber = 1;
    private string ledgerId_ = "";
    /// <summary>
    /// The ID of the ledger exposed by the server.
    /// Must be a valid LedgerString (as described in ``value.proto``).
    /// Required
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string LedgerId {
      get { return ledgerId_; }
      set {
        ledgerId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as GetLedgerIdentityResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(GetLedgerIdentityResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (LedgerId != other.LedgerId) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (LedgerId.Length != 0) hash ^= LedgerId.GetHashCode();
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
      if (LedgerId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(LedgerId);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (LedgerId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(LedgerId);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (LedgerId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(LedgerId);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(GetLedgerIdentityResponse other) {
      if (other == null) {
        return;
      }
      if (other.LedgerId.Length != 0) {
        LedgerId = other.LedgerId;
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
            LedgerId = input.ReadString();
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
            LedgerId = input.ReadString();
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
