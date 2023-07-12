// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: player.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace ULTRANET.Core.Protobuf {

  /// <summary>Holder for reflection information generated from player.proto</summary>
  public static partial class PlayerReflection {

    #region Descriptor
    /// <summary>File descriptor for player.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PlayerReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgxwbGF5ZXIucHJvdG8SFlVMVFJBTkVULkNvcmUuUHJvdG9idWYiiQEKCVRy",
            "YW5zZm9ybRIMCgRwb3NYGAEgASgBEgwKBHBvc1kYAiABKAESDAoEcG9zWhgD",
            "IAEoARIMCgRyb3RYGAQgASgBEgwKBHJvdFkYBSABKAESDAoEcm90WhgGIAEo",
            "ARIMCgRzY2xYGAcgASgBEgwKBHNjbFkYCCABKAESDAoEc2NsWhgJIAEoASJm",
            "CgZQbGF5ZXISDAoEbmFtZRgBIAEoCRIMCgRyb29tGAIgASgJEgoKAmlkGAMg",
            "ASgNEjQKCXRyYW5zZm9ybRgEIAEoCzIhLlVMVFJBTkVULkNvcmUuUHJvdG9i",
            "dWYuVHJhbnNmb3JtYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::ULTRANET.Core.Protobuf.Transform), global::ULTRANET.Core.Protobuf.Transform.Parser, new[]{ "PosX", "PosY", "PosZ", "RotX", "RotY", "RotZ", "SclX", "SclY", "SclZ" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ULTRANET.Core.Protobuf.Player), global::ULTRANET.Core.Protobuf.Player.Parser, new[]{ "Name", "Room", "Id", "Transform" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Transform : pb::IMessage<Transform>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Transform> _parser = new pb::MessageParser<Transform>(() => new Transform());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<Transform> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ULTRANET.Core.Protobuf.PlayerReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Transform() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Transform(Transform other) : this() {
      posX_ = other.posX_;
      posY_ = other.posY_;
      posZ_ = other.posZ_;
      rotX_ = other.rotX_;
      rotY_ = other.rotY_;
      rotZ_ = other.rotZ_;
      sclX_ = other.sclX_;
      sclY_ = other.sclY_;
      sclZ_ = other.sclZ_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Transform Clone() {
      return new Transform(this);
    }

    /// <summary>Field number for the "posX" field.</summary>
    public const int PosXFieldNumber = 1;
    private double posX_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public double PosX {
      get { return posX_; }
      set {
        posX_ = value;
      }
    }

    /// <summary>Field number for the "posY" field.</summary>
    public const int PosYFieldNumber = 2;
    private double posY_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public double PosY {
      get { return posY_; }
      set {
        posY_ = value;
      }
    }

    /// <summary>Field number for the "posZ" field.</summary>
    public const int PosZFieldNumber = 3;
    private double posZ_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public double PosZ {
      get { return posZ_; }
      set {
        posZ_ = value;
      }
    }

    /// <summary>Field number for the "rotX" field.</summary>
    public const int RotXFieldNumber = 4;
    private double rotX_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public double RotX {
      get { return rotX_; }
      set {
        rotX_ = value;
      }
    }

    /// <summary>Field number for the "rotY" field.</summary>
    public const int RotYFieldNumber = 5;
    private double rotY_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public double RotY {
      get { return rotY_; }
      set {
        rotY_ = value;
      }
    }

    /// <summary>Field number for the "rotZ" field.</summary>
    public const int RotZFieldNumber = 6;
    private double rotZ_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public double RotZ {
      get { return rotZ_; }
      set {
        rotZ_ = value;
      }
    }

    /// <summary>Field number for the "sclX" field.</summary>
    public const int SclXFieldNumber = 7;
    private double sclX_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public double SclX {
      get { return sclX_; }
      set {
        sclX_ = value;
      }
    }

    /// <summary>Field number for the "sclY" field.</summary>
    public const int SclYFieldNumber = 8;
    private double sclY_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public double SclY {
      get { return sclY_; }
      set {
        sclY_ = value;
      }
    }

    /// <summary>Field number for the "sclZ" field.</summary>
    public const int SclZFieldNumber = 9;
    private double sclZ_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public double SclZ {
      get { return sclZ_; }
      set {
        sclZ_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as Transform);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(Transform other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(PosX, other.PosX)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(PosY, other.PosY)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(PosZ, other.PosZ)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(RotX, other.RotX)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(RotY, other.RotY)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(RotZ, other.RotZ)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(SclX, other.SclX)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(SclY, other.SclY)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(SclZ, other.SclZ)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (PosX != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(PosX);
      if (PosY != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(PosY);
      if (PosZ != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(PosZ);
      if (RotX != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(RotX);
      if (RotY != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(RotY);
      if (RotZ != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(RotZ);
      if (SclX != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(SclX);
      if (SclY != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(SclY);
      if (SclZ != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(SclZ);
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (PosX != 0D) {
        output.WriteRawTag(9);
        output.WriteDouble(PosX);
      }
      if (PosY != 0D) {
        output.WriteRawTag(17);
        output.WriteDouble(PosY);
      }
      if (PosZ != 0D) {
        output.WriteRawTag(25);
        output.WriteDouble(PosZ);
      }
      if (RotX != 0D) {
        output.WriteRawTag(33);
        output.WriteDouble(RotX);
      }
      if (RotY != 0D) {
        output.WriteRawTag(41);
        output.WriteDouble(RotY);
      }
      if (RotZ != 0D) {
        output.WriteRawTag(49);
        output.WriteDouble(RotZ);
      }
      if (SclX != 0D) {
        output.WriteRawTag(57);
        output.WriteDouble(SclX);
      }
      if (SclY != 0D) {
        output.WriteRawTag(65);
        output.WriteDouble(SclY);
      }
      if (SclZ != 0D) {
        output.WriteRawTag(73);
        output.WriteDouble(SclZ);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (PosX != 0D) {
        output.WriteRawTag(9);
        output.WriteDouble(PosX);
      }
      if (PosY != 0D) {
        output.WriteRawTag(17);
        output.WriteDouble(PosY);
      }
      if (PosZ != 0D) {
        output.WriteRawTag(25);
        output.WriteDouble(PosZ);
      }
      if (RotX != 0D) {
        output.WriteRawTag(33);
        output.WriteDouble(RotX);
      }
      if (RotY != 0D) {
        output.WriteRawTag(41);
        output.WriteDouble(RotY);
      }
      if (RotZ != 0D) {
        output.WriteRawTag(49);
        output.WriteDouble(RotZ);
      }
      if (SclX != 0D) {
        output.WriteRawTag(57);
        output.WriteDouble(SclX);
      }
      if (SclY != 0D) {
        output.WriteRawTag(65);
        output.WriteDouble(SclY);
      }
      if (SclZ != 0D) {
        output.WriteRawTag(73);
        output.WriteDouble(SclZ);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (PosX != 0D) {
        size += 1 + 8;
      }
      if (PosY != 0D) {
        size += 1 + 8;
      }
      if (PosZ != 0D) {
        size += 1 + 8;
      }
      if (RotX != 0D) {
        size += 1 + 8;
      }
      if (RotY != 0D) {
        size += 1 + 8;
      }
      if (RotZ != 0D) {
        size += 1 + 8;
      }
      if (SclX != 0D) {
        size += 1 + 8;
      }
      if (SclY != 0D) {
        size += 1 + 8;
      }
      if (SclZ != 0D) {
        size += 1 + 8;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(Transform other) {
      if (other == null) {
        return;
      }
      if (other.PosX != 0D) {
        PosX = other.PosX;
      }
      if (other.PosY != 0D) {
        PosY = other.PosY;
      }
      if (other.PosZ != 0D) {
        PosZ = other.PosZ;
      }
      if (other.RotX != 0D) {
        RotX = other.RotX;
      }
      if (other.RotY != 0D) {
        RotY = other.RotY;
      }
      if (other.RotZ != 0D) {
        RotZ = other.RotZ;
      }
      if (other.SclX != 0D) {
        SclX = other.SclX;
      }
      if (other.SclY != 0D) {
        SclY = other.SclY;
      }
      if (other.SclZ != 0D) {
        SclZ = other.SclZ;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
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
          case 9: {
            PosX = input.ReadDouble();
            break;
          }
          case 17: {
            PosY = input.ReadDouble();
            break;
          }
          case 25: {
            PosZ = input.ReadDouble();
            break;
          }
          case 33: {
            RotX = input.ReadDouble();
            break;
          }
          case 41: {
            RotY = input.ReadDouble();
            break;
          }
          case 49: {
            RotZ = input.ReadDouble();
            break;
          }
          case 57: {
            SclX = input.ReadDouble();
            break;
          }
          case 65: {
            SclY = input.ReadDouble();
            break;
          }
          case 73: {
            SclZ = input.ReadDouble();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 9: {
            PosX = input.ReadDouble();
            break;
          }
          case 17: {
            PosY = input.ReadDouble();
            break;
          }
          case 25: {
            PosZ = input.ReadDouble();
            break;
          }
          case 33: {
            RotX = input.ReadDouble();
            break;
          }
          case 41: {
            RotY = input.ReadDouble();
            break;
          }
          case 49: {
            RotZ = input.ReadDouble();
            break;
          }
          case 57: {
            SclX = input.ReadDouble();
            break;
          }
          case 65: {
            SclY = input.ReadDouble();
            break;
          }
          case 73: {
            SclZ = input.ReadDouble();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class Player : pb::IMessage<Player>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Player> _parser = new pb::MessageParser<Player>(() => new Player());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<Player> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ULTRANET.Core.Protobuf.PlayerReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Player() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Player(Player other) : this() {
      name_ = other.name_;
      room_ = other.room_;
      id_ = other.id_;
      transform_ = other.transform_ != null ? other.transform_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Player Clone() {
      return new Player(this);
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 1;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "room" field.</summary>
    public const int RoomFieldNumber = 2;
    private string room_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Room {
      get { return room_; }
      set {
        room_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 3;
    private uint id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "transform" field.</summary>
    public const int TransformFieldNumber = 4;
    private global::ULTRANET.Core.Protobuf.Transform transform_;
    /// <summary>
    /// Data Layout:
    ///transform = [
    ///Position.x, Position.y, Position.z,
    ///Rotation.x, Rotation.y, Rotation.z,
    ///Scale   .x, Scale   .y, Scale   .z
    ///]
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ULTRANET.Core.Protobuf.Transform Transform {
      get { return transform_; }
      set {
        transform_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as Player);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(Player other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      if (Room != other.Room) return false;
      if (Id != other.Id) return false;
      if (!object.Equals(Transform, other.Transform)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (Room.Length != 0) hash ^= Room.GetHashCode();
      if (Id != 0) hash ^= Id.GetHashCode();
      if (transform_ != null) hash ^= Transform.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      if (Room.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Room);
      }
      if (Id != 0) {
        output.WriteRawTag(24);
        output.WriteUInt32(Id);
      }
      if (transform_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(Transform);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      if (Room.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Room);
      }
      if (Id != 0) {
        output.WriteRawTag(24);
        output.WriteUInt32(Id);
      }
      if (transform_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(Transform);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Room.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Room);
      }
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Id);
      }
      if (transform_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Transform);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(Player other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.Room.Length != 0) {
        Room = other.Room;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      if (other.transform_ != null) {
        if (transform_ == null) {
          Transform = new global::ULTRANET.Core.Protobuf.Transform();
        }
        Transform.MergeFrom(other.Transform);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
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
            Name = input.ReadString();
            break;
          }
          case 18: {
            Room = input.ReadString();
            break;
          }
          case 24: {
            Id = input.ReadUInt32();
            break;
          }
          case 34: {
            if (transform_ == null) {
              Transform = new global::ULTRANET.Core.Protobuf.Transform();
            }
            input.ReadMessage(Transform);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            Name = input.ReadString();
            break;
          }
          case 18: {
            Room = input.ReadString();
            break;
          }
          case 24: {
            Id = input.ReadUInt32();
            break;
          }
          case 34: {
            if (transform_ == null) {
              Transform = new global::ULTRANET.Core.Protobuf.Transform();
            }
            input.ReadMessage(Transform);
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
