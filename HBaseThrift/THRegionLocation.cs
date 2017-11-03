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
public partial class THRegionLocation : TBase
{
  private TServerName _serverName;
  private THRegionInfo _regionInfo;

  public TServerName ServerName
  {
    get
    {
      return _serverName;
    }
    set
    {
      __isset.serverName = true;
      this._serverName = value;
    }
  }

  public THRegionInfo RegionInfo
  {
    get
    {
      return _regionInfo;
    }
    set
    {
      __isset.regionInfo = true;
      this._regionInfo = value;
    }
  }


  public Isset __isset;
  #if !SILVERLIGHT
  [Serializable]
  #endif
  public struct Isset {
    public bool serverName;
    public bool regionInfo;
  }

  public THRegionLocation() {
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
          if (field.Type == TType.Struct) {
            ServerName = new TServerName();
            ServerName.Read(iprot);
          } else { 
            TProtocolUtil.Skip(iprot, field.Type);
          }
          break;
        case 2:
          if (field.Type == TType.Struct) {
            RegionInfo = new THRegionInfo();
            RegionInfo.Read(iprot);
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
    TStruct struc = new TStruct("THRegionLocation");
    oprot.WriteStructBegin(struc);
    TField field = new TField();
    if (ServerName != null && __isset.serverName) {
      field.Name = "serverName";
      field.Type = TType.Struct;
      field.ID = 1;
      oprot.WriteFieldBegin(field);
      ServerName.Write(oprot);
      oprot.WriteFieldEnd();
    }
    if (RegionInfo != null && __isset.regionInfo) {
      field.Name = "regionInfo";
      field.Type = TType.Struct;
      field.ID = 2;
      oprot.WriteFieldBegin(field);
      RegionInfo.Write(oprot);
      oprot.WriteFieldEnd();
    }
    oprot.WriteFieldStop();
    oprot.WriteStructEnd();
  }

  public override string ToString() {
    StringBuilder sb = new StringBuilder("THRegionLocation(");
    sb.Append("ServerName: ");
    sb.Append(ServerName== null ? "<null>" : ServerName.ToString());
    sb.Append(",RegionInfo: ");
    sb.Append(RegionInfo== null ? "<null>" : RegionInfo.ToString());
    sb.Append(")");
    return sb.ToString();
  }

}
