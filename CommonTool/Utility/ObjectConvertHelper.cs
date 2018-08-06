using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTool.Utility
{
    public static class ObjectConvertHelper
    {
        public static DateTime MinTime
        {
            get
            {
                return new DateTime(1900, 1, 1);
            }
        }
        public static DateTime MaxTime
        {
            get
            {
                return new DateTime(2100, 12, 31);
            }
        }

        public static string ObjectToString(this object val, string defaultValue)
        {

            if (val == null)
            {
                return defaultValue;
            }
            if (Convert.IsDBNull(val))
            {
                return defaultValue;
            }

            try
            {
                return val.ToString().Trim();
            }
            catch
            {
                return defaultValue;
            }
        }
        public static string ObjectToString(this object val)
        {
            return ObjectToString(val, "");
        }

        public static int ObjectToInt(this object val, int defaultValue)
        {
            if (val == null)
                return defaultValue;

            try
            {
                return Convert.ToInt32(val);
            }
            catch
            {
                return defaultValue;
            }
        }
        public static int ObjectToInt(this object val)
        {
            return ObjectToInt(val, 0);
        }

        /// <summary>
        /// 对象转换为DateTime
        /// </summary>
        /// <param name="val">对象</param>
        /// <param name="format">DateTime格式</param>
        /// <returns>string</returns>
        public static string ObjectToDateTime(this object val, string format)
        {
            if (val == null)
            {
                return MinTime.ToString(format);
            }

            try
            {
                return Convert.ToDateTime(val).ToString(format);
            }
            catch
            {
                return MinTime.ToString(format);
            }

        }
        /// <summary>
        /// 对象转换为DateTime
        /// </summary>
        /// <param name="val">对象</param>
        /// <returns>string</returns>
        public static string ObjectToDateTime(this object val)
        {
            return ObjectToDateTime(val, "yyyy-MM-dd");
        }

        public static decimal ObjectToDecimal(this object val, decimal defaultValue)
        {
            if (val == null)
                return defaultValue;

            try
            {
                return Convert.ToDecimal(val);
            }
            catch
            {
                return defaultValue;
            }
        }
        public static decimal ObjectToDecimal(this object val)
        {
            return ObjectToDecimal(val, 0.00m);
        }
    }
}
