// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: com/daml/ledger/api/v1/ledger_configuration_service.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Com.Daml.Ledger.Api.V1 {

  /// <summary>Holder for reflection information generated from com/daml/ledger/api/v1/ledger_configuration_service.proto</summary>
  public static partial class LedgerConfigurationServiceReflection {

    #region Descriptor
    /// <summary>File descriptor for com/daml/ledger/api/v1/ledger_configuration_service.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static LedgerConfigurationServiceReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cjljb20vZGFtbC9sZWRnZXIvYXBpL3YxL2xlZGdlcl9jb25maWd1cmF0aW9u",
            "X3NlcnZpY2UucHJvdG8SFmNvbS5kYW1sLmxlZGdlci5hcGkudjEaKmNvbS9k",
            "YW1sL2xlZGdlci9hcGkvdjEvdHJhY2VfY29udGV4dC5wcm90bxoeZ29vZ2xl",
            "L3Byb3RvYnVmL2R1cmF0aW9uLnByb3RvInAKHUdldExlZGdlckNvbmZpZ3Vy",
            "YXRpb25SZXF1ZXN0EhEKCWxlZGdlcl9pZBgBIAEoCRI8Cg10cmFjZV9jb250",
            "ZXh0GOgHIAEoCzIkLmNvbS5kYW1sLmxlZGdlci5hcGkudjEuVHJhY2VDb250",
            "ZXh0ImsKHkdldExlZGdlckNvbmZpZ3VyYXRpb25SZXNwb25zZRJJChRsZWRn",
            "ZXJfY29uZmlndXJhdGlvbhgBIAEoCzIrLmNvbS5kYW1sLmxlZGdlci5hcGku",
            "djEuTGVkZ2VyQ29uZmlndXJhdGlvbiJcChNMZWRnZXJDb25maWd1cmF0aW9u",
            "EjkKFm1heF9kZWR1cGxpY2F0aW9uX3RpbWUYAyABKAsyGS5nb29nbGUucHJv",
            "dG9idWYuRHVyYXRpb25KBAgBEAJKBAgCEAMyqAEKGkxlZGdlckNvbmZpZ3Vy",
            "YXRpb25TZXJ2aWNlEokBChZHZXRMZWRnZXJDb25maWd1cmF0aW9uEjUuY29t",
            "LmRhbWwubGVkZ2VyLmFwaS52MS5HZXRMZWRnZXJDb25maWd1cmF0aW9uUmVx",
            "dWVzdBo2LmNvbS5kYW1sLmxlZGdlci5hcGkudjEuR2V0TGVkZ2VyQ29uZmln",
            "dXJhdGlvblJlc3BvbnNlMAFCVwoWY29tLmRhbWwubGVkZ2VyLmFwaS52MUIk",
            "TGVkZ2VyQ29uZmlndXJhdGlvblNlcnZpY2VPdXRlckNsYXNzqgIWQ29tLkRh",
            "bWwuTGVkZ2VyLkFwaS5WMWIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Com.Daml.Ledger.Api.V1.TraceContextReflection.Descriptor, global::Google.Protobuf.WellKnownTypes.DurationReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Com.Daml.Ledger.Api.V1.GetLedgerConfigurationRequest), global::Com.Daml.Ledger.Api.V1.GetLedgerConfigurationRequest.Parser, new[]{ "LedgerId", "TraceContext" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Com.Daml.Ledger.Api.V1.GetLedgerConfigurationResponse), global::Com.Daml.Ledger.Api.V1.GetLedgerConfigurationResponse.Parser, new[]{ "LedgerConfiguration" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Com.Daml.Ledger.Api.V1.LedgerConfiguration), global::Com.Daml.Ledger.Api.V1.LedgerConfiguration.Parser, new[]{ "MaxDeduplicationTime" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class GetLedgerConfigurationRequest : pb::IMessage<GetLedgerConfigurationRequest>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<GetLedgerConfigurationRequest> _parser = new pb::MessageParser<GetLedgerConfigurationRequest>(() => new GetLedgerConfigurationRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<GetLedgerConfigurationRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Com.Daml.Ledger.Api.V1.LedgerConfigurationServiceReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetLedgerConfigurationRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetLedgerConfigurationRequest(GetLedgerConfigurationRequest other) : this() {
      ledgerId_ = other.ledgerId_;
      traceContext_ = other.traceContext_ != null ? other.traceContext_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetLedgerConfigurationRequest Clone() {
      return new GetLedgerConfigurationRequest(this);
    }

    /// <summary>Field number for the "ledger_id" field.</summary>
    public const int LedgerIdFieldNumber = 1;
    private string ledgerId_ = "";
    /// <summary>
    /// Must correspond to the ledger ID reported by the Ledger Identification Service.
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
      return Equals(other as GetLedgerConfigurationRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(GetLedgerConfigurationRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (LedgerId != other.LedgerId) return false;
      if (!object.Equals(TraceContext, other.TraceContext)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (LedgerId.Length != 0) hash ^= LedgerId.GetHashCode();
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
      if (LedgerId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(LedgerId);
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
      if (LedgerId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(LedgerId);
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
      if (LedgerId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(LedgerId);
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
    public void MergeFrom(GetLedgerConfigurationRequest other) {
      if (other == null) {
        return;
      }
      if (other.LedgerId.Length != 0) {
        LedgerId = other.LedgerId;
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
            LedgerId = input.ReadString();
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
            LedgerId = input.ReadString();
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

  public sealed partial class GetLedgerConfigurationResponse : pb::IMessage<GetLedgerConfigurationResponse>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<GetLedgerConfigurationResponse> _parser = new pb::MessageParser<GetLedgerConfigurationResponse>(() => new GetLedgerConfigurationResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<GetLedgerConfigurationResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Com.Daml.Ledger.Api.V1.LedgerConfigurationServiceReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetLedgerConfigurationResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetLedgerConfigurationResponse(GetLedgerConfigurationResponse other) : this() {
      ledgerConfiguration_ = other.ledgerConfiguration_ != null ? other.ledgerConfiguration_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetLedgerConfigurationResponse Clone() {
      return new GetLedgerConfigurationResponse(this);
    }

    /// <summary>Field number for the "ledger_configuration" field.</summary>
    public const int LedgerConfigurationFieldNumber = 1;
    private global::Com.Daml.Ledger.Api.V1.LedgerConfiguration ledgerConfiguration_;
    /// <summary>
    /// The latest ledger configuration.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Com.Daml.Ledger.Api.V1.LedgerConfiguration LedgerConfiguration {
      get { return ledgerConfiguration_; }
      set {
        ledgerConfiguration_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as GetLedgerConfigurationResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(GetLedgerConfigurationResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(LedgerConfiguration, other.LedgerConfiguration)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (ledgerConfiguration_ != null) hash ^= LedgerConfiguration.GetHashCode();
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
      if (ledgerConfiguration_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(LedgerConfiguration);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (ledgerConfiguration_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(LedgerConfiguration);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (ledgerConfiguration_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(LedgerConfiguration);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(GetLedgerConfigurationResponse other) {
      if (other == null) {
        return;
      }
      if (other.ledgerConfiguration_ != null) {
        if (ledgerConfiguration_ == null) {
          LedgerConfiguration = new global::Com.Daml.Ledger.Api.V1.LedgerConfiguration();
        }
        LedgerConfiguration.MergeFrom(other.LedgerConfiguration);
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
            if (ledgerConfiguration_ == null) {
              LedgerConfiguration = new global::Com.Daml.Ledger.Api.V1.LedgerConfiguration();
            }
            input.ReadMessage(LedgerConfiguration);
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
            if (ledgerConfiguration_ == null) {
              LedgerConfiguration = new global::Com.Daml.Ledger.Api.V1.LedgerConfiguration();
            }
            input.ReadMessage(LedgerConfiguration);
            break;
          }
        }
      }
    }
    #endif

  }

  /// <summary>
  /// LedgerConfiguration contains parameters of the ledger instance that may be useful to clients.
  /// </summary>
  public sealed partial class LedgerConfiguration : pb::IMessage<LedgerConfiguration>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<LedgerConfiguration> _parser = new pb::MessageParser<LedgerConfiguration>(() => new LedgerConfiguration());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<LedgerConfiguration> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Com.Daml.Ledger.Api.V1.LedgerConfigurationServiceReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public LedgerConfiguration() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public LedgerConfiguration(LedgerConfiguration other) : this() {
      maxDeduplicationTime_ = other.maxDeduplicationTime_ != null ? other.maxDeduplicationTime_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public LedgerConfiguration Clone() {
      return new LedgerConfiguration(this);
    }

    /// <summary>Field number for the "max_deduplication_time" field.</summary>
    public const int MaxDeduplicationTimeFieldNumber = 3;
    private global::Google.Protobuf.WellKnownTypes.Duration maxDeduplicationTime_;
    /// <summary>
    /// The maximum value for the ``deduplication_time`` parameter of command submissions
    /// (as described in ``commands.proto``). This defines the maximum time window during which
    /// commands can be deduplicated.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Google.Protobuf.WellKnownTypes.Duration MaxDeduplicationTime {
      get { return maxDeduplicationTime_; }
      set {
        maxDeduplicationTime_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as LedgerConfiguration);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(LedgerConfiguration other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(MaxDeduplicationTime, other.MaxDeduplicationTime)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (maxDeduplicationTime_ != null) hash ^= MaxDeduplicationTime.GetHashCode();
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
      if (maxDeduplicationTime_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(MaxDeduplicationTime);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (maxDeduplicationTime_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(MaxDeduplicationTime);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (maxDeduplicationTime_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(MaxDeduplicationTime);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(LedgerConfiguration other) {
      if (other == null) {
        return;
      }
      if (other.maxDeduplicationTime_ != null) {
        if (maxDeduplicationTime_ == null) {
          MaxDeduplicationTime = new global::Google.Protobuf.WellKnownTypes.Duration();
        }
        MaxDeduplicationTime.MergeFrom(other.MaxDeduplicationTime);
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
          case 26: {
            if (maxDeduplicationTime_ == null) {
              MaxDeduplicationTime = new global::Google.Protobuf.WellKnownTypes.Duration();
            }
            input.ReadMessage(MaxDeduplicationTime);
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
          case 26: {
            if (maxDeduplicationTime_ == null) {
              MaxDeduplicationTime = new global::Google.Protobuf.WellKnownTypes.Duration();
            }
            input.ReadMessage(MaxDeduplicationTime);
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
