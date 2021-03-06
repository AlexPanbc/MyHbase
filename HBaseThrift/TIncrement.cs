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
/// Used to perform Increment operations for a single row.
/// 
/// You can specify how this Increment should be written to the write-ahead Log (WAL)
/// by changing the durability. If you don't provide durability, it defaults to
/// column family's default setting for durability.
/// </summary>
#if !SILVERLIGHT
[Serializable]
#endif
public partial class TIncrement : TBase
{
  private byte[] _row;
  private List<TColumnIncrement> _columns;
  private Dictionary<byte[], byte[]> _attributes;
  private TDurability _durability;
  private TCellVisibility _cellVisibility;

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

  public List<TColumnIncrement> Columns
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

  public TCellVisibility CellVisibility
  {
    get
    {
      return _cellVisibility;
    }
    set
    {
      __isset.cellVisibility = true;
      this._cellVisibility = value;
    }
  }


  public Isset __isset;
  #if !SILVERLIGHT
  [Serializable]
  #endif
  public struct Isset {
    public bool row;
    public bool columns;
    public bool attributes;
    public bool durability;
    public bool cellVisibility;
  }

  public TIncrement() {
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
              Columns = new List<TColumnIncrement>();
              TList _list35 = iprot.ReadListBegin();
              for( int _i36 = 0; _i36 < _list35.Count; ++_i36)
              {
                TColumnIncrement _elem37 = new TColumnIncrement();
                _elem37 = new TColumnIncrement();
                _elem37.Read(iprot);
                Columns.Add(_elem37);
              }
              iprot.ReadListEnd();
            }
          } else { 
            TProtocolUtil.Skip(iprot, field.Type);
          }
          break;
        case 4:
          if (field.Type == TType.Map) {
            {
              Attributes = new Dictionary<byte[], byte[]>();
              TMap _map38 = iprot.ReadMapBegin();
              for( int _i39 = 0; _i39 < _map38.Count; ++_i39)
              {
                byte[] _key40;
                byte[] _val41;
                _key40 = iprot.ReadBinary();
                _val41 = iprot.ReadBinary();
                Attributes[_key40] = _val41;
              }
              iprot.ReadMapEnd();
            }
          } else { 
            TProtocolUtil.Skip(iprot, field.Type);
          }
          break;
        case 5:
          if (field.Type == TType.I32) {
            Durability = (TDurability)iprot.ReadI32();
          } else { 
            TProtocolUtil.Skip(iprot, field.Type);
          }
          break;
        case 6:
          if (field.Type == TType.Struct) {
            CellVisibility = new TCellVisibility();
            CellVisibility.Read(iprot);
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
    TStruct struc = new TStruct("TIncrement");
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
        foreach (TColumnIncrement _iter42 in Columns)
        {
          _iter42.Write(oprot);
        }
        oprot.WriteListEnd();
      }
      oprot.WriteFieldEnd();
    }
    if (Attributes != null && __isset.attributes) {
      field.Name = "attributes";
      field.Type = TType.Map;
      field.ID = 4;
      oprot.WriteFieldBegin(field);
      {
        oprot.WriteMapBegin(new TMap(TType.String, TType.String, Attributes.Count));
        foreach (byte[] _iter43 in Attributes.Keys)
        {
          oprot.WriteBinary(_iter43);
          oprot.WriteBinary(Attributes[_iter43]);
        }
        oprot.WriteMapEnd();
      }
      oprot.WriteFieldEnd();
    }
    if (__isset.durability) {
      field.Name = "durability";
      field.Type = TType.I32;
      field.ID = 5;
      oprot.WriteFieldBegin(field);
      oprot.WriteI32((int)Durability);
      oprot.WriteFieldEnd();
    }
    if (CellVisibility != null && __isset.cellVisibility) {
      field.Name = "cellVisibility";
      field.Type = TType.Struct;
      field.ID = 6;
      oprot.WriteFieldBegin(field);
      CellVisibility.Write(oprot);
      oprot.WriteFieldEnd();
    }
    oprot.WriteFieldStop();
    oprot.WriteStructEnd();
  }

  public override string ToString() {
    StringBuilder sb = new StringBuilder("TIncrement(");
    sb.Append("Row: ");
    sb.Append(Row);
    sb.Append(",Columns: ");
    sb.Append(Columns);
    sb.Append(",Attributes: ");
    sb.Append(Attributes);
    sb.Append(",Durability: ");
    sb.Append(Durability);
    sb.Append(",CellVisibility: ");
    sb.Append(CellVisibility== null ? "<null>" : CellVisibility.ToString());
    sb.Append(")");
    return sb.ToString();
  }

}

