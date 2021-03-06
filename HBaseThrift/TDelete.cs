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


/// <summary>
/// Used to perform Delete operations on a single row.
/// 
/// The scope can be further narrowed down by specifying a list of
/// columns or column families as TColumns.
/// 
/// Specifying only a family in a TColumn will delete the whole family.
/// If a timestamp is specified all versions with a timestamp less than
/// or equal to this will be deleted. If no timestamp is specified the
/// current time will be used.
/// 
/// Specifying a family and a column qualifier in a TColumn will delete only
/// this qualifier. If a timestamp is specified only versions equal
/// to this timestamp will be deleted. If no timestamp is specified the
/// most recent version will be deleted.  To delete all previous versions,
/// specify the DELETE_COLUMNS TDeleteType.
/// 
/// The top level timestamp is only used if a complete row should be deleted
/// (i.e. no columns are passed) and if it is specified it works the same way
/// as if you had added a TColumn for every column family and this timestamp
/// (i.e. all versions older than or equal in all column families will be deleted)
/// 
/// You can specify how this Delete should be written to the write-ahead Log (WAL)
/// by changing the durability. If you don't provide durability, it defaults to
/// column family's default setting for durability.
/// </summary>
#if !SILVERLIGHT
[Serializable]
#endif
public partial class TDelete : TBase
{
  private byte[] _row;
  private List<TColumn> _columns;
  private long _timestamp;
  private TDeleteType _deleteType;
  private Dictionary<byte[], byte[]> _attributes;
  private TDurability _durability;

  public byte[] Row
  {
    get
    {
      return _row;
    }
    set
    {
      __isset.row = true;
      this._row = value;
    }
  }

  public List<TColumn> Columns
  {
    get
    {
      return _columns;
    }
    set
    {
      __isset.columns = true;
      this._columns = value;
    }
  }

  public long Timestamp
  {
    get
    {
      return _timestamp;
    }
    set
    {
      __isset.timestamp = true;
      this._timestamp = value;
    }
  }

  /// <summary>
  /// 
  /// <seealso cref="TDeleteType"/>
  /// </summary>
  public TDeleteType DeleteType
  {
    get
    {
      return _deleteType;
    }
    set
    {
      __isset.deleteType = true;
      this._deleteType = value;
    }
  }

  public Dictionary<byte[], byte[]> Attributes
  {
    get
    {
      return _attributes;
    }
    set
    {
      __isset.attributes = true;
      this._attributes = value;
    }
  }

  /// <summary>
  /// 
  /// <seealso cref="TDurability"/>
  /// </summary>
  public TDurability Durability
  {
    get
    {
      return _durability;
    }
    set
    {
      __isset.durability = true;
      this._durability = value;
    }
  }


  public Isset __isset;
  #if !SILVERLIGHT
  [Serializable]
  #endif
  public struct Isset {
    public bool row;
    public bool columns;
    public bool timestamp;
    public bool deleteType;
    public bool attributes;
    public bool durability;
  }

  public TDelete() {
    this._deleteType = (TDeleteType)1;
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
            Row = iprot.ReadBinary();
          } else { 
            TProtocolUtil.Skip(iprot, field.Type);
          }
          break;
        case 2:
          if (field.Type == TType.List) {
            {
              Columns = new List<TColumn>();
              TList _list26 = iprot.ReadListBegin();
              for( int _i27 = 0; _i27 < _list26.Count; ++_i27)
              {
                TColumn _elem28 = new TColumn();
                _elem28 = new TColumn();
                _elem28.Read(iprot);
                Columns.Add(_elem28);
              }
              iprot.ReadListEnd();
            }
          } else { 
            TProtocolUtil.Skip(iprot, field.Type);
          }
          break;
        case 3:
          if (field.Type == TType.I64) {
            Timestamp = iprot.ReadI64();
          } else { 
            TProtocolUtil.Skip(iprot, field.Type);
          }
          break;
        case 4:
          if (field.Type == TType.I32) {
            DeleteType = (TDeleteType)iprot.ReadI32();
          } else { 
            TProtocolUtil.Skip(iprot, field.Type);
          }
          break;
        case 6:
          if (field.Type == TType.Map) {
            {
              Attributes = new Dictionary<byte[], byte[]>();
              TMap _map29 = iprot.ReadMapBegin();
              for( int _i30 = 0; _i30 < _map29.Count; ++_i30)
              {
                byte[] _key31;
                byte[] _val32;
                _key31 = iprot.ReadBinary();
                _val32 = iprot.ReadBinary();
                Attributes[_key31] = _val32;
              }
              iprot.ReadMapEnd();
            }
          } else { 
            TProtocolUtil.Skip(iprot, field.Type);
          }
          break;
        case 7:
          if (field.Type == TType.I32) {
            Durability = (TDurability)iprot.ReadI32();
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
    TStruct struc = new TStruct("TDelete");
    oprot.WriteStructBegin(struc);
    TField field = new TField();
    if (Row != null && __isset.row) {
      field.Name = "row";
      field.Type = TType.String;
      field.ID = 1;
      oprot.WriteFieldBegin(field);
      oprot.WriteBinary(Row);
      oprot.WriteFieldEnd();
    }
    if (Columns != null && __isset.columns) {
      field.Name = "columns";
      field.Type = TType.List;
      field.ID = 2;
      oprot.WriteFieldBegin(field);
      {
        oprot.WriteListBegin(new TList(TType.Struct, Columns.Count));
        foreach (TColumn _iter33 in Columns)
        {
          _iter33.Write(oprot);
        }
        oprot.WriteListEnd();
      }
      oprot.WriteFieldEnd();
    }
    if (__isset.timestamp) {
      field.Name = "timestamp";
      field.Type = TType.I64;
      field.ID = 3;
      oprot.WriteFieldBegin(field);
      oprot.WriteI64(Timestamp);
      oprot.WriteFieldEnd();
    }
    if (__isset.deleteType) {
      field.Name = "deleteType";
      field.Type = TType.I32;
      field.ID = 4;
      oprot.WriteFieldBegin(field);
      oprot.WriteI32((int)DeleteType);
      oprot.WriteFieldEnd();
    }
    if (Attributes != null && __isset.attributes) {
      field.Name = "attributes";
      field.Type = TType.Map;
      field.ID = 6;
      oprot.WriteFieldBegin(field);
      {
        oprot.WriteMapBegin(new TMap(TType.String, TType.String, Attributes.Count));
        foreach (byte[] _iter34 in Attributes.Keys)
        {
          oprot.WriteBinary(_iter34);
          oprot.WriteBinary(Attributes[_iter34]);
        }
        oprot.WriteMapEnd();
      }
      oprot.WriteFieldEnd();
    }
    if (__isset.durability) {
      field.Name = "durability";
      field.Type = TType.I32;
      field.ID = 7;
      oprot.WriteFieldBegin(field);
      oprot.WriteI32((int)Durability);
      oprot.WriteFieldEnd();
    }
    oprot.WriteFieldStop();
    oprot.WriteStructEnd();
  }

  public override string ToString() {
    StringBuilder sb = new StringBuilder("TDelete(");
    sb.Append("Row: ");
    sb.Append(Row);
    sb.Append(",Columns: ");
    sb.Append(Columns);
    sb.Append(",Timestamp: ");
    sb.Append(Timestamp);
    sb.Append(",DeleteType: ");
    sb.Append(DeleteType);
    sb.Append(",Attributes: ");
    sb.Append(Attributes);
    sb.Append(",Durability: ");
    sb.Append(Durability);
    sb.Append(")");
    return sb.ToString();
  }

}

