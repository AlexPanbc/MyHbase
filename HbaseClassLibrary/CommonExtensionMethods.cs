using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
//using Newtonsoft.Json;

namespace HbaseClassLibrary
{
    /// <summary>
    /// 通用扩展方法
    /// </summary>
    public static class CommonExtensionMethods
    {
        ///// <summary>
        ///// Json序列化
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="obj"></param>
        ///// <param name="defaultVal">默认值</param>
        ///// <returns></returns>
        //public static string ToJson<T>(this T obj, string defaultVal = "")
        //{
        //    if (null == obj)
        //    {
        //        return defaultVal;
        //    }
        //    return JsonConvert.SerializeObject(obj);
        //}

        ///// <summary>
        ///// Json反序列化
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public static T FromJson<T>(this string obj)
        //{
        //    if (string.IsNullOrWhiteSpace(obj))
        //    {
        //        return default(T);
        //    }
        //    try
        //    {
        //        var result = JsonConvert.DeserializeObject<T>(obj);
        //        return result;
        //    }
        //    catch
        //    {
        //        return default(T);
        //    }
        //}

        public static T DictionaryValue<T>(this IDictionary dict, object key, T defaultVal = default(T))
        {
            if (!dict.Contains(key))
                return default(T);
            try
            {
                if (typeof(T).Name == "String")
                    return (T)Convert.ChangeType(dict[key].ToString().Trim('\"'), typeof(T));
                return (T)Convert.ChangeType(dict[key].ToString(), typeof(T));
            }
            catch (InvalidCastException)
            {
                return defaultVal;
            }
        }

        /// <summary>
        /// 获取10位时间戳
        /// </summary>
        /// <param name="currentDate">当前时间</param>
        /// <returns></returns>
        public static long GetTimeStamp(this DateTime currentDate)
        {
            return (long)(currentDate.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds);
        }

        public static T ConvertTo<T>(this string source, T defaultVal = default(T)) where T : IConvertible
        {
            try
            {
                return (T)Convert.ChangeType(source, typeof(T));
            }
            catch (InvalidCastException)
            {
                return defaultVal;
            }
        }

        /// <summary>
        /// 验证字符串是否为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 转换成毫秒级时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ToMillisecondTimestamp(this DateTime time)
        {
            return (long)(time - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
        }

        /// <summary>
        /// 转换成时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ToTimestamp(this DateTime time)
        {
            return (long)(time - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
        }

        /// <summary>
        /// 把时间戳转换成时间
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string timestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).Add(new TimeSpan(long.Parse(timestamp + "0000")));
        }


        public static T Deserialize<T>(this byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                try
                {
                    var serializer = new DataContractJsonSerializer(typeof(T));
                    return (T)serializer.ReadObject(stream);
                }
                catch
                {
                    return default(T);
                }
            }
        }

        public static byte[] Serializer(this object data)
        {
            var serializer = new DataContractJsonSerializer(data.GetType());
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, data);
                return stream.ToArray();
            }
        }
    }
}