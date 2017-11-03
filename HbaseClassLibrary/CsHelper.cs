using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HbaseClassLibrary
{
    public class CsHelper
    {

        /// <summary>
        /// 生成消息行键
        /// </summary>
        /// <param name="wxuserid"></param>
        /// <returns></returns>
        public async
            Task<string> CreateRowkey(int wxuserid)
        {
            return
                wxuserid.ToString().PadLeft(8, '0') +
                (long.MaxValue - DateTime.Now.ToMillisecondTimestamp()).ToString().PadLeft(19, '0');
        }

        /// <summary>
        /// 生成消息的结束行键
        /// </summary>
        /// <param name="wxuserid"></param>
        /// <returns></returns>
        public async Task<string> CreateEndRowkey(int wxuserid)
        {
            return wxuserid.ToString().PadLeft(8, '0') +
                   (long.MaxValue - new DateTime(2016, 9, 19).ToMillisecondTimestamp()).ToString().PadLeft(19, '0');
        }

    }
}
