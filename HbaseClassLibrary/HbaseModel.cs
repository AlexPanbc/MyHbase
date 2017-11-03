using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HbaseClassLibrary
{

    public class CellData : HColumn
    {
        public string Value { get; set; }
    }

    public class RowData
    {
        public string RowKey { get; set; }
        public List<CellData> RowValue { get; set; }
    }


    public class InsertCellData : HColumn
    {
        public object Value { get; set; }
    }

    public class InsertRowData
    {
        public string RowKey { get; set; }

        public List<InsertCellData> Columns { get; set; }
    }

    public class HColumn
    {
        public string Family { get; set; }
        public string Column { get; set; }
        public long Timestamp { get; set; }
    }
}
