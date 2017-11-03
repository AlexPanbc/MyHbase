using DmtMax.Infrastructure.HBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HbaseClassLibrary;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace MyHbase.Controllers
{
    [RoutePrefix("api/Default")]
    public class DefaultController : ApiController
    {
        [Route("Get"), HttpGet, ResponseType(typeof(object))]
        public async Task<IHttpActionResult> Get()
        {
            HBaseHelper hbs = new HBaseHelper();
            //var data = hbs.GetRows("t", "00002337922337055503", 10);
            var data = hbs.GetRows("t", "000023379223370555035661462", 1);
            return Ok(data);
        }

        [Route("Inst"), HttpGet, ResponseType(typeof(object))]
        public async Task<IHttpActionResult> Inst()
        {
            CsHelper cs = new CsHelper();
            var rowkey = await cs.CreateRowkey(2337);
            var hbasemsg = new List<InsertCellData>
                {
                    new InsertCellData
                    {
                        Family = "m",
                        Column = "u",
                        Value = "你"
                    },
                    new InsertCellData
                    {
                        Family = "m",
                        Column = "s",
                        Value = "是"
                    },
                    new InsertCellData
                    {
                        Family = "m",
                        Column = "t",
                        Value = "她他它"
                    },
                    new InsertCellData
                    {
                        Family = "m",
                        Column = "r",
                        Value = "日"
                    },
                    new InsertCellData
                    {
                        Family = "m",
                        Column = "c",
                        Value = "哈哈哈！"
                    }
                };
            HBaseHelper hbs = new HBaseHelper();
            hbs.Insert("t", rowkey, hbasemsg);
            return Ok(true);
        }

        [Route("Insts"), HttpGet, ResponseType(typeof(object))]
        public async Task<IHttpActionResult> Insts()
       {
            CsHelper cs = new CsHelper();
            List<InsertRowData> lstr = new List<InsertRowData>();
            for (int i = 0; i < 10; i++)
            {
                InsertRowData ir = new InsertRowData();
                ir.RowKey = await cs.CreateRowkey(2337);
                ir.Columns = new List<InsertCellData>
                {
                    new InsertCellData
                    {
                        Family = "m",
                        Column = "u",
                        Value = DateTime.Now
                    },
                    new InsertCellData
                    {
                        Family = "m",
                        Column = "s",
                        Value = DateTime.Now
                    },
                    new InsertCellData
                    {
                        Family = "m",
                        Column = "t",
                        Value =DateTime.Now
                    },
                    new InsertCellData
                    {
                        Family = "m",
                        Column = "r",
                        Value = DateTime.Now
                    },
                    new InsertCellData
                    {
                        Family = "m",
                        Column = "c",
                        Value = DateTime.Now
                    }
                };
                lstr.Add(ir);
                if (i > 0 && i / 2 == 0)
                {
                    HBaseHelper hbs = new HBaseHelper();
                    hbs.Insert("t", lstr);
                    lstr.Clear();
                }
            }

            return Ok(true);
        }
    }
}
