// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: mediapipe/framework/mediapipe_options.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Mediapipe {

  /// <summary>Holder for reflection information generated from mediapipe/framework/mediapipe_options.proto</summary>
  public static partial class MediapipeOptionsReflection {

    #region Descriptor
    /// <summary>File descriptor for mediapipe/framework/mediapipe_options.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static MediapipeOptionsReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CittZWRpYXBpcGUvZnJhbWV3b3JrL21lZGlhcGlwZV9vcHRpb25zLnByb3Rv",
            "EgltZWRpYXBpcGUiHgoQTWVkaWFQaXBlT3B0aW9ucyoKCKCcARCAgICAAkIz",
            "Chpjb20uZ29vZ2xlLm1lZGlhcGlwZS5wcm90b0IVTWVkaWFQaXBlT3B0aW9u",
            "c1Byb3Rv"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Mediapipe.MediaPipeOptions), global::Mediapipe.MediaPipeOptions.Parser, null, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// Options used by a MediaPipe object.
  /// </summary>
  public sealed partial class MediaPipeOptions : pb::IExtendableMessage<MediaPipeOptions>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<MediaPipeOptions> _parser = new pb::MessageParser<MediaPipeOptions>(() => new MediaPipeOptions());
    private pb::UnknownFieldSet _unknownFields;
    private pb::ExtensionSet<MediaPipeOptions> _extensions;
    private pb::ExtensionSet<MediaPipeOptions> _Extensions { get { return _extensions; } }
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<MediaPipeOptions> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Mediapipe.MediapipeOptionsReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MediaPipeOptions() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MediaPipeOptions(MediaPipeOptions other) : this() {
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
      _extensions = pb::ExtensionSet.Clone(other._extensions);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MediaPipeOptions Clone() {
      return new MediaPipeOptions(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as MediaPipeOptions);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(MediaPipeOptions other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!Equals(_extensions, other._extensions)) {
        return false;
      }
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (_extensions != null) {
        hash ^= _extensions.GetHashCode();
      }
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
      if (_extensions != null) {
        _extensions.WriteTo(output);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (_extensions != null) {
        _extensions.WriteTo(ref output);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (_extensions != null) {
        size += _extensions.CalculateSize();
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(MediaPipeOptions other) {
      if (other == null) {
        return;
      }
      pb::ExtensionSet.MergeFrom(ref _extensions, other._extensions);
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
            if (!pb::ExtensionSet.TryMergeFieldFrom(ref _extensions, input)) {
              _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            }
            break;
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
            if (!pb::ExtensionSet.TryMergeFieldFrom(ref _extensions, ref input)) {
              _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            }
            break;
        }
      }
    }
    #endif

    public TValue GetExtension<TValue>(pb::Extension<MediaPipeOptions, TValue> extension) {
      return pb::ExtensionSet.Get(ref _extensions, extension);
    }
    public pbc::RepeatedField<TValue> GetExtension<TValue>(pb::RepeatedExtension<MediaPipeOptions, TValue> extension) {
      return pb::ExtensionSet.Get(ref _extensions, extension);
    }
    public pbc::RepeatedField<TValue> GetOrInitializeExtension<TValue>(pb::RepeatedExtension<MediaPipeOptions, TValue> extension) {
      return pb::ExtensionSet.GetOrInitialize(ref _extensions, extension);
    }
    public void SetExtension<TValue>(pb::Extension<MediaPipeOptions, TValue> extension, TValue value) {
      pb::ExtensionSet.Set(ref _extensions, extension, value);
    }
    public bool HasExtension<TValue>(pb::Extension<MediaPipeOptions, TValue> extension) {
      return pb::ExtensionSet.Has(ref _extensions, extension);
    }
    public void ClearExtension<TValue>(pb::Extension<MediaPipeOptions, TValue> extension) {
      pb::ExtensionSet.Clear(ref _extensions, extension);
    }
    public void ClearExtension<TValue>(pb::RepeatedExtension<MediaPipeOptions, TValue> extension) {
      pb::ExtensionSet.Clear(ref _extensions, extension);
    }

  }

  #endregion

}

#endregion Designer generated code
