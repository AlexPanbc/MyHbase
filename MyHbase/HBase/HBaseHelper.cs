using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using Thrift.Protocol;
using Thrift.Transport;
using HbaseClassLibrary;

namespace DmtMax.Infrastructure.HBase
{
    public class HBaseHelper : IDisposable
    {
        private readonly Lazy<TTransport> _transport;
        private readonly Lazy<TBinaryProtocol> _tBinaryProtocol;
        private readonly Lazy<THBaseService.Client> _client;


        public HBaseHelper()
        {
            string hosturl = ConfigurationManager.AppSettings["HbaseHost"].ToString();
            int port = int.Parse(ConfigurationManager.AppSettings["HbasePort"].ToString());
            _transport = new Lazy<TTransport>(() => new TSocket(hosturl, port));
            _tBinaryProtocol = new Lazy<TBinaryProtocol>(() => new TBinaryProtocol(_transport.Value));
            _client =
                new Lazy<THBaseService.Client>(() =>
                {
                    _transport.Value.Open();
                    return new THBaseService.Client(_tBinaryProtocol.Value);
                });
        }

        #region 行操作

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="rowkey">行键</param>
        public void DeleteRow(string tablename, string rowkey)
        {
            _client.Value.deleteSingle(Encoding.UTF8.GetBytes(tablename), new TDelete
            {
                Row = Encoding.UTF8.GetBytes(rowkey)
            });
        }


        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="rowkey">行键</param>
        /// <param name="timestamp">时间戳</param>
        public void DeleteRow(string tablename, string rowkey, long timestamp)
        {
            _client.Value.deleteSingle(Encoding.UTF8.GetBytes(tablename), new TDelete
            {
                Row = Encoding.UTF8.GetBytes(rowkey),
                Timestamp = timestamp
            });
        }

        /// <summary>
        /// 删除一个单元的数据
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="rowkey">行键</param>
        /// <param name="family"></param>
        /// <param name="columnname">列名</param>
        public void DeleteCell(string tablename, string rowkey, string family, string columnname)
        {
            _client.Value.deleteSingle(Encoding.UTF8.GetBytes(tablename), new TDelete
            {
                Row = Encoding.UTF8.GetBytes(rowkey),
                Columns = new List<TColumn>
                {
                    new TColumn
                    {
                        Family = Encoding.UTF8.GetBytes(family),
                        Qualifier = Encoding.UTF8.GetBytes(columnname)
                    }
                }
            });
        }

        /// <summary>
        /// 删除一个单元的数据
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="rowkey">行键</param>
        /// <param name="family"></param>
        /// <param name="columnname">列名</param>
        /// <param name="timestamp">时间戳</param>
        public void DeleteCell(string tablename, string rowkey, string family, string columnname, long timestamp)
        {
            _client.Value.deleteSingle(Encoding.UTF8.GetBytes(tablename), new TDelete
            {
                Row = Encoding.UTF8.GetBytes(rowkey),
                Columns = new List<TColumn>
                {
                    new TColumn
                    {
                        Family = Encoding.UTF8.GetBytes(family),
                        Qualifier = Encoding.UTF8.GetBytes(columnname),
                        Timestamp = timestamp
                    }
                }
            });
        }


        /// <summary>
        /// 删除多个单元格数据
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="rowkey"></param>
        /// <param name="columns"></param>
        public void DeleteCells(string tablename, string rowkey, IEnumerable<HColumn> columns)
        {
            _client.Value.deleteSingle(Encoding.UTF8.GetBytes(tablename), new TDelete
            {
                Row = Encoding.UTF8.GetBytes(rowkey),
                Columns = columns.Select(p => new TColumn
                {
                    Family = Encoding.UTF8.GetBytes(p.Family),
                    Qualifier = Encoding.UTF8.GetBytes(p.Column),
                    Timestamp = p.Timestamp
                }).ToList()
            });
        }

        /// <summary>
        /// 检查行是否存在
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="rowkey"></param>
        /// <returns></returns>
        public bool Exist(string tablename, string rowkey)
        {
            return _client.Value.exists(Encoding.UTF8.GetBytes(tablename), new TGet
            {
                Row = Encoding.UTF8.GetBytes(rowkey)
            });
        }

        /// <summary>
        /// 检查某行的某些列是否存在
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="rowkey"></param>
        /// <param name="columns">一种有一列存在就返回true</param>
        /// <returns></returns>
        public bool Exist(string tablename, string rowkey, IEnumerable<HColumn> columns)
        {
            return _client.Value.exists(Encoding.UTF8.GetBytes(tablename), new TGet
            {
                Row = Encoding.UTF8.GetBytes(rowkey),
                Columns = columns.Select(p => new TColumn
                {
                    Family = Encoding.UTF8.GetBytes(p.Family),
                    Qualifier = Encoding.UTF8.GetBytes(p.Column),
                    Timestamp = p.Timestamp
                }).ToList()
            });
        }


        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="rowkey">行键</param>
        /// <param name="family"></param>
        /// <param name="columnname">列名</param>
        /// <param name="value">值</param>
        public void Insert(string tablename, string rowkey, string family, string columnname, object value)
        {
            _client.Value.put(Encoding.UTF8.GetBytes(tablename), new TPut
            {
                Row = Encoding.UTF8.GetBytes(rowkey),
                ColumnValues = new List<TColumnValue>
                {
                    new TColumnValue
                    {
                        Family = Encoding.UTF8.GetBytes(family),
                        Qualifier = Encoding.UTF8.GetBytes(columnname),
                        Value = Serializer(value)
                    }
                }
            });
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="rowkey">行键</param>
        /// <param name="family"></param>
        /// <param name="columnname">列名</param>
        /// <param name="value">值</param>
        /// <param name="timestamp"></param>
        public void Insert(string tablename, string rowkey, string family, string columnname, object value,
            long timestamp)
        {
            _client.Value.put(Encoding.UTF8.GetBytes(tablename), new TPut
            {
                Row = Encoding.UTF8.GetBytes(rowkey),
                ColumnValues = new List<TColumnValue>
                {
                    new TColumnValue
                    {
                        Family = Encoding.UTF8.GetBytes(family),
                        Qualifier = Encoding.UTF8.GetBytes(columnname),
                        Value = Serializer(value),
                        Timestamp = timestamp
                    }
                }
            });
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="rowkey">行键</param>
        /// <param name="cellsData">单元格的数据</param>
        public void Insert(string tablename, string rowkey, params InsertCellData[] cellsData)
        {
            Insert(tablename, rowkey, cellsData.ToList());
        }


        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="rowkey">行键</param>
        /// <param name="cellsData">单元格的数据</param>
        public void Insert(string tablename, string rowkey, IEnumerable<InsertCellData> cellsData)
        {
            _client.Value.put(Encoding.UTF8.GetBytes(tablename), new TPut
            {
                Row = Encoding.UTF8.GetBytes(rowkey),
                ColumnValues = cellsData.Select(p => new TColumnValue
                {
                    Family = Encoding.UTF8.GetBytes(p.Family),
                    Qualifier = Encoding.UTF8.GetBytes(p.Column),
                    Value = Serializer(p.Value)
                }).ToList()
            });
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="rowsdata">多行数据</param>
        public void Insert(string tablename, params InsertRowData[] rowsdata)
        {
            Insert(tablename, rowsdata.ToList());
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="rowsdata">多行数据</param>
        public void Insert(string tablename, IEnumerable<InsertRowData> rowsdata)
        {
            _client.Value.putMultiple(Encoding.UTF8.GetBytes(tablename), rowsdata.Select(p => new TPut
            {
                Row = Encoding.UTF8.GetBytes(p.RowKey),
                ColumnValues = p.Columns.Select(r => new TColumnValue
                {
                    Family = Encoding.UTF8.GetBytes(r.Family),
                    Qualifier = Encoding.UTF8.GetBytes(r.Column),
                    Value = Serializer(r.Value)
                }).ToList()
            }).ToList());
        }

        #endregion

        #region 数据获取

        /// <summary>
        /// 查询单行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tablename">表名</param>
        /// <param name="rowkey">行键</param>
        /// <returns></returns>
        public List<CellData> GetRow(string tablename, string rowkey)
        {
            var data = _client.Value.get(Encoding.UTF8.GetBytes(tablename), new TGet
            {
                Row = Encoding.UTF8.GetBytes(rowkey)
            });
            return data?.ColumnValues?.Select(p => new CellData
            {
                Column = Encoding.UTF8.GetString(p.Qualifier),
                Family = Encoding.UTF8.GetString(p.Family),
                Timestamp = p.Timestamp,
                Value = Deserialize<string>(p.Value)
            }).ToList();
        }

        /// <summary>
        /// 查询单行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tablename">表名</param>
        /// <param name="rowkey">行键</param>
        /// <param name="timestamp">时间戳</param>
        /// <returns></returns>
        public List<CellData> GetRow(string tablename, string rowkey, long timestamp)
        {
            var data = _client.Value.get(Encoding.UTF8.GetBytes(tablename), new TGet
            {
                Row = Encoding.UTF8.GetBytes(rowkey),
                Timestamp = timestamp
            });
            return data?.ColumnValues?.Select(p => new CellData
            {
                Column = Encoding.UTF8.GetString(p.Qualifier),
                Family = Encoding.UTF8.GetString(p.Family),
                Timestamp = p.Timestamp,
                Value = Deserialize<string>(p.Value)
            }).ToList();
        }

        /// <summary>
        /// 查询单行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tablename">表名</param>
        /// <param name="rowkey">行键</param>
        /// <param name="startTimestamp"></param>
        /// <param name="endTimestamp"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public List<CellData> GetRow(string tablename, string rowkey, long startTimestamp, long endTimestamp,
            IEnumerable<HColumn> columns)
        {
            var data = _client.Value.get(Encoding.UTF8.GetBytes(tablename), new TGet
            {
                Row = Encoding.UTF8.GetBytes(rowkey),
                TimeRange = new TTimeRange
                {
                    MinStamp = endTimestamp,
                    MaxStamp = startTimestamp
                },
                Columns = columns.Select(p => new TColumn
                {
                    Family = Encoding.UTF8.GetBytes(p.Family),
                    Qualifier = Encoding.UTF8.GetBytes(p.Column)
                }).ToList()
            });
            return data?.ColumnValues?.Select(p => new CellData
            {
                Column = Encoding.UTF8.GetString(p.Qualifier),
                Family = Encoding.UTF8.GetString(p.Family),
                Timestamp = p.Timestamp,
                Value = Deserialize<string>(p.Value)
            }).ToList();
        }

        public List<CellData> GetRow(string tablename, string rowkey, int versions,
            IEnumerable<HColumn> columns)
        {
            var data = _client.Value.get(Encoding.UTF8.GetBytes(tablename), new TGet
            {
                Row = Encoding.UTF8.GetBytes(rowkey),
                MaxVersions = versions,
                Columns = columns.Select(p => new TColumn
                {
                    Family = Encoding.UTF8.GetBytes(p.Family),
                    Qualifier = Encoding.UTF8.GetBytes(p.Column)
                }).ToList()
            });
            return data?.ColumnValues?.Select(p => new CellData
            {
                Column = Encoding.UTF8.GetString(p.Qualifier),
                Family = Encoding.UTF8.GetString(p.Family),
                Timestamp = p.Timestamp,
                Value = Deserialize<string>(p.Value)
            }).ToList();
        }

        /// <summary>
        /// 查询单行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tablename">表名</param>
        /// <param name="rowkey">行键</param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public List<CellData> GetRow(string tablename, string rowkey, IEnumerable<HColumn> columns)
        {
            var data = _client.Value.get(Encoding.UTF8.GetBytes(tablename), new TGet
            {
                Row = Encoding.UTF8.GetBytes(rowkey),
                Columns = columns.Select(p => new TColumn
                {
                    Family = Encoding.UTF8.GetBytes(p.Family),
                    Qualifier = Encoding.UTF8.GetBytes(p.Column),
                    Timestamp = p.Timestamp
                }).ToList()
            });
            return data?.ColumnValues?.Select(p => new CellData
            {
                Column = Encoding.UTF8.GetString(p.Qualifier),
                Family = Encoding.UTF8.GetString(p.Family),
                Timestamp = p.Timestamp,
                Value = Deserialize<string>(p.Value)
            }).ToList();
        }


        /// <summary>
        /// 获取多行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tablename">表名</param>
        /// <param name="rowkeys">行键</param>
        /// <returns></returns>
        public List<RowData> GetRows(string tablename, params string[] rowkeys)
        {
            var data = _client.Value.getMultiple(Encoding.UTF8.GetBytes(tablename), rowkeys.Select(p => new TGet
            {
                Row = Encoding.UTF8.GetBytes(p)
            }).ToList());
            return data?.Select(row => new RowData
            {
                RowKey = Encoding.UTF8.GetString(row.Row),
                RowValue = row.ColumnValues?.Select(p => new CellData
                {
                    Column = Encoding.UTF8.GetString(p.Qualifier),
                    Value = Deserialize<string>(p.Value),
                    Timestamp = p.Timestamp,
                    Family = Encoding.UTF8.GetString(p.Family)
                }).ToList()
            }).ToList();
        }


        /// <summary>
        /// 获取多行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tablename">表名</param>
        /// <param name="columns">需要返回的列</param>
        /// <param name="rowkeys">行键</param>
        /// <returns></returns>
        public List<RowData> GetRows(string tablename, IEnumerable<HColumn> columns, params string[] rowkeys)
        {
            var data = _client.Value.getMultiple(Encoding.UTF8.GetBytes(tablename), rowkeys.Select(p => new TGet
            {
                Row = Encoding.UTF8.GetBytes(p),
                Columns = columns.Select(c => new TColumn
                {
                    Family = Encoding.UTF8.GetBytes(c.Family),
                    Qualifier = Encoding.UTF8.GetBytes(c.Column),
                    Timestamp = c.Timestamp
                }).ToList()
            }).ToList());
            return data?.Select(row => new RowData
            {
                RowKey = Encoding.UTF8.GetString(row.Row),
                RowValue = row.ColumnValues?.Select(p => new CellData
                {
                    Column = Encoding.UTF8.GetString(p.Qualifier),
                    Value = Deserialize<string>(p.Value),
                    Timestamp = p.Timestamp,
                    Family = Encoding.UTF8.GetString(p.Family)
                }).ToList()
            }).ToList();
        }


        public List<RowData> GetRows(string tablename, string startRowkey, int count)
        {
            var data = _client.Value.getScannerResults(Encoding.UTF8.GetBytes(tablename), new TScan
            {
                StartRow = Encoding.UTF8.GetBytes(startRowkey)
            }, count);

            return data?.Select(row => new RowData
            {
                RowKey = Encoding.UTF8.GetString(row.Row),
                RowValue = row.ColumnValues?.Select(p => new CellData
                {
                    Column = Encoding.UTF8.GetString(p.Qualifier),
                    Value = Deserialize<string>(p.Value),
                    Timestamp = p.Timestamp,
                    Family = Encoding.UTF8.GetString(p.Family)
                }).ToList()
            }).ToList();
        }

        /// <summary>
        /// 获取多行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tablename">表名</param>
        /// <param name="startRowkey">开始行键</param>
        /// <param name="endRowkey">结束行键</param>
        /// <param name="count">查询行数</param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public List<RowData> GetRows(string tablename, string startRowkey, string endRowkey, int count,
            IEnumerable<HColumn> columns)
        {
            var data = _client.Value.getScannerResults(Encoding.UTF8.GetBytes(tablename), new TScan
            {
                StartRow = Encoding.UTF8.GetBytes(startRowkey),
                StopRow = Encoding.UTF8.GetBytes(endRowkey),
                Columns = columns.Select(p => new TColumn
                {
                    Qualifier = Encoding.UTF8.GetBytes(p.Column),
                    Family = Encoding.UTF8.GetBytes(p.Family),
                    Timestamp = p.Timestamp
                }).ToList()
            }, count);

            return data?.Select(row => new RowData
            {
                RowKey = Encoding.UTF8.GetString(row.Row),
                RowValue = row.ColumnValues?.Select(p => new CellData
                {
                    Column = Encoding.UTF8.GetString(p.Qualifier),
                    Value = Deserialize<string>(p.Value),
                    Timestamp = p.Timestamp,
                    Family = Encoding.UTF8.GetString(p.Family)
                }).ToList()
            }).ToList();
        }

        /// <summary>
        /// 根据开始rowkey扫码指定数量的行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tablename">表名</param>
        /// <param name="startRowkey">开始rowkey</param>
        /// <param name="count">行数</param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public List<RowData> GetRows(string tablename, string startRowkey, int count, IEnumerable<HColumn> columns)
        {
            var data = _client.Value.getScannerResults(Encoding.UTF8.GetBytes(tablename), new TScan
            {
                StartRow = Encoding.UTF8.GetBytes(startRowkey),
                Columns = columns.Select(p => new TColumn
                {
                    Qualifier = Encoding.UTF8.GetBytes(p.Column),
                    Family = Encoding.UTF8.GetBytes(p.Family),
                    Timestamp = p.Timestamp
                }).ToList()
            }, count);
            return data?.Select(row => new RowData
            {
                RowKey = Encoding.UTF8.GetString(row.Row),
                RowValue = row.ColumnValues?.Select(p => new CellData
                {
                    Column = Encoding.UTF8.GetString(p.Qualifier),
                    Value = Deserialize<string>(p.Value),
                    Timestamp = p.Timestamp,
                    Family = Encoding.UTF8.GetString(p.Family)
                }).ToList()
            }).ToList();
        }

        #endregion

        private T Deserialize<T>(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(stream);
            }
        }

        private byte[] Serializer<T>(T data)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(data.GetType());
                serializer.WriteObject(stream, data);
                return stream.ToArray();
            }
        }

        public void Dispose()
        {
            if (_tBinaryProtocol.IsValueCreated)
                _tBinaryProtocol.Value.Dispose();

            if (!_transport.IsValueCreated) return;
            _transport.Value.Close();
            _transport.Value.Dispose();
        }
    }
}