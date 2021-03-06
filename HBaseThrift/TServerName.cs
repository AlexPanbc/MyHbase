/**
 * Autogenerated by Thrift Compiler (0.9.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;


#if !SILVERLIGHT
[Serializable]
#endif
public partial class TServerName : TBase
{
  private string _hostName;
  private int _port;
  private long _startCode;

  public string HostName
  {
    get
    {
      return _hostName;
    }
    set
    {
      __isset.hostName = true;
      this._hostName = value;
    }
  }

  public int Port
  {
    get
    {
      return _port;
    }
    set
    {
      __isset.port = true;
      this._port = value;
    }
  }

  public long StartCode
  {
    get
    {
      return _startCode;
    }
    set
    {
      __isset.startCode = true;
      this._startCode = value;
    }
  }


  public Isset __isset;
  #if !SILVERLIGHT
  [Serializable]
  #endif
  public struct Isset {
    public bool hostName;
    public bool port;
    public bool startCode;
  }

  public TServerName() {
  }

  public void Read (TProtocol iprot)
  {
    TField field;
    iprot.ReadStructBegin();
    while (true)
    {
      field = iprot.ReadFieldBegin();
      if (field.Type == TType.Stop) { 
        break;
      }
      switch (field.ID)
      {
        case 1:
          if (field.Type == TType.String) {
            HostName = iprot.ReadString();
          } else { 
            TProtocolUtil.Skip(iprot, field.Type);
          }
          break;
        case 2:
          if (field.Type == TType.I32) {
            Port = iprot.ReadI32();
          } else { 
            TProtocolUtil.Skip(iprot, field.Type);
          }
          break;
        case 3:
          if (field.Type == TType.I64) {
            StartCode = iprot.ReadI64();
          } else { 
            TProtocolUtil.Skip(iprot, field.Type);
          }
          break;
        default: 
          TProtocolUtil.Skip(iprot, field.Type);
          break;
      }
      iprot.ReadFieldEnd();
    }
    iprot.ReadStructEnd();
  }

  public void Write(TProtocol oprot) {
    TStruct struc = new TStruct("TServerName");
    oprot.WriteStructBegin(struc);
    TField field = new TField();
    if (HostName != null && __isset.hostName) {
      field.Name = "hostName";
      field.Type = TType.String;
      field.ID = 1;
      oprot.WriteFieldBegin(field);
      oprot.WriteString(HostName);
      oprot.WriteFieldEnd();
    }
    if (__isset.port) {
      field.Name = "port";
      field.Type = TType.I32;
      field.ID = 2;
      oprot.WriteFieldBegin(field);
      oprot.WriteI32(Port);
      oprot.WriteFieldEnd();
    }
    if (__isset.startCode) {
      field.Name = "startCode";
      field.Type = TType.I64;
      field.ID = 3;
      oprot.WriteFieldBegin(field);
      oprot.WriteI64(StartCode);
      oprot.WriteFieldEnd();
    }
    oprot.WriteFieldStop();
    oprot.WriteStructEnd();
  }

  public override string ToString() {
    StringBuilder sb = new StringBuilder("TServerName(");
    sb.Append("HostName: ");
    sb.Append(HostName);
    sb.Append(",Port: ");
    sb.Append(Port);
    sb.Append(",StartCode: ");
    sb.Append(StartCode);
    sb.Append(")");
    return sb.ToString();
  }

}

