using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CommonTool.Utility
{
    public static class HelperUtility
    {
        /// <summary>
        /// 转换成int类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt32(object value)
        {
            return value == null ? 0 : Convert.ToInt32(value);
        }
        /// <summary>
        /// 从系统字段类型转换为DbType类型
        /// </summary>
        /// <param name="colType">系统字段类型</param>
        /// <returns>DbType</returns>
        public static DbType ChangeToDbType(Type colType)
        {
            var dbType = DbType.Object;

            if (colType == null)
            {
                return dbType;
            }
            if (colType.IsGenericType)
            {
                if (colType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    colType = colType.GetGenericArguments().First();
                }
                else
                {
                    return dbType;
                }
            }

            #region

            if (colType == typeof(Boolean))
            {
                dbType = DbType.Boolean;
            }
            else if (colType == typeof(Byte))
            {
                dbType = DbType.Byte;
            }
            else if (colType == typeof(Char))
            {
                dbType = DbType.StringFixedLength;
            }
            else if (colType == typeof(DateTime))
            {
                dbType = DbType.DateTime;
            }
            else if (colType == typeof(Decimal))
            {
                dbType = DbType.Decimal;
            }
            else if (colType == typeof(Double))
            {
                dbType = DbType.Double;
            }
            else if (colType == typeof(Guid))
            {
                dbType = DbType.Guid;
            }
            else if (colType == typeof(Int16))
            {
                dbType = DbType.Int16;
            }
            else if (colType == typeof(Int32))
            {
                dbType = DbType.Int32;
            }
            else if (colType == typeof(Int64))
            {
                dbType = DbType.Int64;
            }
            else if (colType == typeof(Object))
            {
                dbType = DbType.Object;
            }
            else if (colType == typeof(SByte))
            {
                dbType = DbType.SByte;
            }
            else if (colType == typeof(Single))
            {
                dbType = DbType.Single;
            }
            else if (colType == typeof(String))
            {
                dbType = DbType.String;
            }
            else if (colType == typeof(UInt16))
            {
                dbType = DbType.UInt16;
            }
            else if (colType == typeof(UInt32))
            {
                dbType = DbType.UInt32;
            }
            else if (colType == typeof(UInt64))
            {
                dbType = DbType.UInt64;
            }
            else if (colType == typeof(XmlDocument) || colType == typeof(XDocument))
            {
                dbType = DbType.Xml;
            }
            else
            {
                if (colType.BaseType == typeof(Enum))
                {
                    dbType = DbType.Int16;
                }
            }

            #endregion

            return dbType;
        }

        /// <summary>
        /// 从数据库数据表字段类型转换为DbType类型
        /// </summary>
        /// <param name="colType">数据库数据表字段类型</param>
        /// <returns>DbType</returns>
        public static DbType ChangeToDbType(string colType)
        {
            var dbType = DbType.Object;//默认为Object
            switch (colType.ToLower())
            {
                case "int":
                case "int32":
                    dbType = DbType.Int32;
                    break;
                case "bigint":
                case "long":
                case "int64":
                    dbType = DbType.Int64;
                    break;
                case "smallint":
                case "tinyint":
                case "short":
                case "int16":
                    dbType = DbType.Int16;
                    break;
                case "bit":
                case "bool":
                case "boolean":
                    dbType = DbType.Boolean;
                    break;
                case "datetime":
                    dbType = DbType.DateTime;
                    break;
                case "time":
                    dbType = DbType.Time;
                    break;
                case "decimal":
                    dbType = DbType.Decimal;
                    break;
                case "float":
                    dbType = DbType.Double;
                    break;
                case "image":
                    dbType = DbType.Object;
                    break;
                case "money":
                    dbType = DbType.Decimal;
                    break;
                case "smalldatetime":
                    dbType = DbType.Date;
                    break;
                case "text":
                case "ntext":
                case "nvarchar":
                case "nchar":
                case "varchar":
                case "string":
                    dbType = DbType.String;
                    break;
                case "binary":
                    dbType = DbType.Binary;
                    break;
                case "char":
                    dbType = DbType.StringFixedLength;
                    break;
                case "numeric":
                    dbType = DbType.Decimal;
                    break;
                case "real":
                case "smallmoney":
                    dbType = DbType.Object;
                    break;
                case "sql_variant":
                    dbType = DbType.VarNumeric;
                    break;
                case "timestamp":
                    dbType = DbType.Time;
                    break;
                case "uniqueidentifier":
                    dbType = DbType.Guid;
                    break;
                case "varbinary":
                    dbType = DbType.Binary;
                    break;
                case "xml":
                    dbType = DbType.Xml;
                    break;
            }
            return dbType;
        }
        /// <summary>
        /// 根据数据表中的字段对应的类型格式化对应的值
        /// </summary>
        /// <param name="colValue">字段值</param>
        /// <param name="colType">字段类型</param>
        /// <returns></returns>
        public static object ChangeToDbValue(object colValue, DbType colType)
        {
            if (colValue == null || colValue == DBNull.Value)
            {
                return DBNull.Value;
            }
            if (colType == DbType.Xml)
            {
                return colValue.ToString();
            }
            colValue = ChangeToDbValue(colValue, colType.ToString());
            return colValue;
        }
        /// <summary>
        /// 根据数据表中的字段对应的类型格式化对应的值
        /// </summary>
        /// <param name="colValue">字段值</param>
        /// <param name="colType">字段类型</param>
        /// <returns></returns>
        public static object ChangeToDbValue(object colValue, string colType)
        {
            if (colValue == null)
            {
                return DBNull.Value;
            }
            var arrType = colType.Split('.');
            colType = arrType.Length == 2 ? arrType[1] : colType;
            try
            {
                object reValue = DBNull.Value;
                switch (colType.ToLower())
                {
                    case "uniqueidentifier":
                    case "guid":
                        {
                            reValue = new Guid(Convert.ToString(colValue));
                            break;
                        }
                    case "int16":
                        {
                            reValue = Convert.ToInt16(colValue);
                            break;
                        }
                    case "int":
                    case "int32":
                        {
                            reValue = Convert.ToInt32(colValue);
                            break;
                        }
                    case "bigint":
                    case "int64":
                        reValue = Convert.ToInt64(colValue);
                        break;
                    case "varchar":
                    case "string":
                        reValue = Convert.ToString(colValue);
                        break;
                    case "bit":
                    case "bool":
                    case "boolean":
                        {
                            if (colValue.ToString() == "")
                            {
                                reValue = DBNull.Value;
                            }
                            else
                            {
                                reValue = new[] { "1", "on", "true" }.Contains(Convert.ToString(colValue).ToLower());
                            }

                        }
                        break;
                    case "time":
                    case "datetime":
                        {
                            DateTime time;
                            if (DateTime.TryParse(colValue.ToString(), out time))
                            {
                                reValue = time;
                                break;
                            }
                            reValue = Convert.ToDateTime(colValue);
                            //不设置时间的默认值
                            if ((DateTime)reValue == DateTime.MinValue)
                            {
                                reValue = DBNull.Value;
                            }
                        }
                        break;
                    case "decimal":
                        reValue = Convert.ToDecimal(colValue);
                        break;
                    case "double":
                        reValue = Convert.ToDouble(colValue);
                        break;
                    case "float":
                        reValue = Convert.ToSingle(colValue);
                        break;
                    case "image":
                        reValue = Convert.ToByte(colValue);
                        break;
                    case "money":
                        reValue = Convert.ToDecimal(colValue);
                        break;
                    case "ntext":
                        reValue = Convert.ToString(colValue);
                        break;
                    case "nvarchar":
                        reValue = Convert.ToString(colValue);
                        break;
                    case "smalldatetime":
                        reValue = Convert.ToDateTime(colValue);
                        break;
                    case "smallint":
                        reValue = Convert.ToInt16(colValue);
                        break;
                    case "text":
                        reValue = Convert.ToString(colValue);
                        break;
                    case "binary":
                        reValue = Convert.ToByte(colValue);
                        break;
                    case "char":
                        reValue = Convert.ToChar(colValue);
                        break;
                    case "nchar":
                        reValue = Convert.ToChar(colValue);
                        break;
                    case "numeric":
                        reValue = Convert.ToDecimal(colValue);
                        break;
                    case "real":
                        reValue = Convert.ToInt16(colValue);
                        break;
                    case "smallmoney":
                        reValue = Convert.ToDecimal(colValue);
                        break;
                    case "timestamp":
                        reValue = Convert.ToDateTime(colValue);
                        break;
                    case "tinyint":
                        reValue = Convert.ToInt16(colValue);
                        break;
                    case "xdocument":
                        reValue = colValue.ToString();
                        break;
                }
                return reValue;
            }
            catch
            {
                return DBNull.Value;
            }
        }
        /// <summary>
        /// 获取实体的属性和值 
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="t">实体</param>
        /// <returns>属性和值的键值</returns>
        public static Dictionary<string, object> GetEntityFieldValue<T>(T t)
        {
            if (t.GetType().Name.Contains("Dictionary"))
            {
                object tObj = t;
                return (Dictionary<string, object>)tObj;
            }
            var dic = new Dictionary<string, object>();
            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0)
            {
                return dic;
            }
            foreach (var item in properties)
            {
                var value = ChangeType(item.GetValue(t, null), item.PropertyType);
                dic.Add(item.Name, value);
            }
            return dic;
        }
        /// <summary>
        /// 获取表指定行的键值
        /// </summary>
        /// <param name="table">表数据</param>
        /// <param name="rowIndex">指定行</param>
        /// <returns></returns>
        public static Dictionary<string, object> GetDataTableFieldValue(DataTable table, int rowIndex = 0)
        {
            var dct = new Dictionary<string, object>();
            if (table == null || table.Rows.Count <= 0 || rowIndex >= table.Rows.Count)
            {
                return dct;
            }
            var dr = table.Rows[rowIndex];
            foreach (DataColumn dc in table.Columns)
            {
                dct.Add(dc.ColumnName, dr[dc.Ordinal] == DBNull.Value ? null : dr[dc.Ordinal]);
            }
            return dct;
        }

        /// <summary>
        /// Data to Model转换函数，得到实体列表
        /// </summary>
        /// <returns></returns>
        public static List<T> GetEntityList<T>(IDataReader dr, int startIndex = -1, int endIndex = -1, bool isColseDataReader = true) where T : new()
        {
            var list = new List<T>();
            try
            {
                var tempIndex = -1;
                while (dr.Read())
                {
                    #region 数据读取控制

                    if (endIndex > -1 && tempIndex >= endIndex)
                    {
                        break;
                    }

                    if (startIndex > -1 && tempIndex++ < (startIndex - 1))
                    {
                        continue;
                    }

                    #endregion

                    var t = Activator.CreateInstance<T>();
                    for (var i = 0; i < dr.FieldCount; i++)
                    {
                        var prop = typeof(T).GetProperty(dr.GetName(i),
                                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                        if (prop == null) continue;
                        //找到对应属性时
                        var value = ChangeType(dr[i], prop.PropertyType);
                        prop.SetValue(t, value, null);
                    }
                    list.Add(t);
                }
            }
            finally
            {
                if (isColseDataReader)
                {
                    dr.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// Data to Model转换函数，得到单一实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="rowIndex"></param>
        /// <param name="isColseDataReader">是否关闭DataReader</param>
        /// <returns></returns>
        public static T GetEntity<T>(IDataReader dr, int rowIndex = 0, bool isColseDataReader = true) where T : new()
        {
            var entitys = GetEntityList<T>(dr, rowIndex, rowIndex, isColseDataReader);
            if (entitys != null && entitys.Any())
            {
                return entitys[0];
            }
            return default(T);
        }

        #region DataTable 转成实体列表或指定单一实体

        /// <summary>
        /// DataTable to Model转换函数，得到实体列表
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="table">数据表</param>
        /// <param name="entityType">实体类型</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns></returns>
        public static List<object> GetEntityList(DataTable table, Type entityType, int startIndex = -1, int endIndex = -1)
        {
            var list = new List<object>();
            if (table == null || table.Rows.Count == 0)
            {
                return list;
            }

            var tempIndex = -1;

            var dataColumns = table.Columns.Cast<DataColumn>().ToList();
            foreach (DataRow row in table.Rows)
            {
                #region 数据读取控制

                if (endIndex > -1 && tempIndex >= endIndex)
                {
                    break;
                }

                if (startIndex > -1 && tempIndex++ < (startIndex - 1))
                {
                    continue;
                }

                #endregion

                var t = Activator.CreateInstance(entityType);
                foreach (var pro in t.GetType().GetProperties())
                {
                    var dataColumn = dataColumns.FirstOrDefault(r => String.Equals(r.ColumnName, pro.Name, StringComparison.CurrentCultureIgnoreCase));
                    if (dataColumn == null) continue;
                    var val = ChangeType(row[dataColumn.Ordinal], pro.PropertyType);
                    if (val != null)
                    {
                        pro.SetValue(t, val, null);
                    }
                }
                list.Add(t);
            }
            return list;
        }
        /// <summary>
        /// DataTable to Model转换函数，得到实体列表
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="table">数据表</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns></returns>
        public static List<T> GetEntityList<T>(DataTable table, int startIndex = -1, int endIndex = -1)
        {
            var list = new List<T>();
            if (table == null || table.Rows.Count == 0)
            {
                return list;
            }

            var tempIndex = -1;

            var dataColumns = table.Columns.Cast<DataColumn>().ToList();
            foreach (DataRow row in table.Rows)
            {
                #region 数据读取控制

                if (endIndex > -1 && tempIndex >= endIndex)
                {
                    break;
                }

                if (startIndex > -1 && tempIndex++ < (startIndex - 1))
                {
                    continue;
                }

                #endregion
                var t = Activator.CreateInstance<T>();
                if (t.GetType().Name.Contains("Dictionary"))
                {
                    for (var i = 0; i < table.Columns.Count; i++)
                    {
                        var columnName = table.Columns[i].ColumnName;
                        var val = ChangeType(row[columnName], table.Columns[i].DataType);
                        ((IDictionary)t).Add(columnName, val);
                    }
                }
                else
                {
                    foreach (var pro in typeof(T).GetProperties())
                    {
                        var dataColumn = dataColumns.FirstOrDefault(r => String.Equals(r.ColumnName, pro.Name, StringComparison.CurrentCultureIgnoreCase));
                        if (dataColumn == null) continue;
                        var val = ChangeType(row[dataColumn.Ordinal], pro.PropertyType);
                        if (val != null)
                        {
                            pro.SetValue(t, val, null);
                        }
                    }
                }
                list.Add(t);
            }
            return list;
        }

        /// <summary>
        /// DataTable to Model转换函数，得到单一实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="table">数据表</param>
        /// <param name="rowIndex">指定行</param>
        /// <returns></returns>
        public static T GetEntity<T>(DataTable table, int rowIndex = 0)
        {
            var entitys = GetEntityList<T>(table, rowIndex, rowIndex);
            if (entitys != null && entitys.Any())
            {
                return entitys[0];
            }
            return default(T);
        }

        #endregion


        #region 复制实体或实体列表

        /// <summary>
        /// 复制相同属性值到新的对象中 两个对象属性名一定要相同
        /// 如果属性值不相同，可通过特殊处理
        /// </summary>
        /// <typeparam name="TIn">复制对象</typeparam>
        /// <typeparam name="TOut">需要赋值的对象</typeparam>
        /// <param name="thisObj">复制对象</param>
        /// <param name="ditKeyValue">属性名与值的键值</param>
        /// <returns></returns>
        public static TOut CopyToNewObject<TIn, TOut>(this TIn thisObj, Dictionary<string, object> ditKeyValue = null)
            where TIn : class
            where TOut : class
        {
            if (thisObj == null)
            {
                return default(TOut);
            }
            //新建对象
            var t = Activator.CreateInstance<TOut>();
            CopyObjct(thisObj, t);

            if (ditKeyValue == null || !ditKeyValue.Any())
            {
                return t;
            }
            var objInfo2 = typeof(TOut).GetProperties();
            foreach (var p2 in objInfo2)
            {
                //以属性键值为最新值
                if (!ditKeyValue.ContainsKey(p2.Name)) continue;
                var val = ChangeType(ditKeyValue[p2.Name], p2.PropertyType);
                p2.SetValue(t, val, null);
            }
            return t;
        }

        /// <summary>
        /// 列表数据转换成另外类型的数据列表 
        /// 需要确保两个实体中属性名是一致
        /// </summary>
        /// <typeparam name="TIn">被转换的类型</typeparam>
        /// <typeparam name="TOut">要转换的类型</typeparam>
        /// <param name="lstObj">被转换的列表</param>
        /// <returns></returns>
        public static List<TOut> CopyToNewObjectList<TIn, TOut>(this IEnumerable<TIn> lstObj)
            where TIn : class
            where TOut : class
        {
            try
            {
                return lstObj.Where(r => r != null)
                        .Select(obj => CopyToNewObject<TIn, TOut>(obj))
                        .Where(r => r != null)
                        .ToList();
            }
            catch
            {
                return new List<TOut>();
            }
        }

        /// <summary>
        ///  复制实体，根据相同属性名
        /// </summary>
        /// <param name="thisObj">复制对象</param>
        /// <param name="outObj">要得到对象</param>
        /// <param name="filterProperties">要过滤的属性名</param>
        public static void CopyObjct(this object thisObj, object outObj, List<string> filterProperties = null)
        {
            if (thisObj == null || outObj == null)
            {
                return;
            }

            var properties = thisObj.GetType().GetProperties();
            if (filterProperties != null && filterProperties.Any())
            {
                properties = properties.Where(r => !filterProperties.Contains(r.Name)).ToArray();
            }

            var outProperties = outObj.GetType().GetProperties();

            foreach (var p in properties)
            {
                var property =
                    outProperties.FirstOrDefault(
                        r => String.Equals(r.Name, p.Name, StringComparison.CurrentCultureIgnoreCase));

                if (property == null || !property.CanWrite)
                {
                    continue;
                }

                var value = ChangeType(p.GetValue(thisObj, null), p.PropertyType);
                property.SetValue(outObj, value, null);
            }

        }


        /// <summary>
        /// 列表数据转换成另外类型的数据列表 
        /// 需要确保两个实体中属性名是一致
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="lstObj">被转换的列表</param>
        /// <returns></returns>
        public static List<T> CopyToNewObjectList<T>(this object lstObj) where T : class
        {
            var lstObject = lstObj as IEnumerable;

            return lstObject != null
                ? lstObject.Cast<object>()
                    .Select(obj => obj.CopyToNewObject<T>())
                    .Where(entity => entity != null)
                    .ToList()
                : new List<T>();
        }
        /// <summary>
        /// 复制相同属性值到新的对象中 两个对象属性名一定要相同
        /// 如果属性值不相同，可通过特殊处理
        /// </summary>
        /// <typeparam name="T">需要赋值的对象</typeparam>
        /// <param name="thisObj">复制对象</param>
        /// <param name="ditKeyValue">属性名与值的键值</param>
        /// <returns></returns>
        public static T CopyToNewObject<T>(this object thisObj, Dictionary<string, object> ditKeyValue = null) where T : class
        {
            if (thisObj == null)
            {
                return default(T);
            }
            //新建对象
            var t = Activator.CreateInstance(typeof(T)) as T;
            if (t == null)
            {
                return default(T);
            }

            var objInfo1 = thisObj.GetType().GetProperties();

            var objInfo2 = t.GetType().GetProperties();

            foreach (var p in objInfo1)
            {
                foreach (var p2 in objInfo2)
                {
                    //匹配相同属性名
                    if (String.Equals(p.Name, p2.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        var val = ChangeType(p.GetValue(thisObj, null), p2.PropertyType);
                        if (val != null && p2.CanWrite)
                        {
                            p2.SetValue(t, val, null);
                        }
                    }
                    //以属性键值为最新值
                    if (ditKeyValue != null && ditKeyValue.ContainsKey(p2.Name))
                    {
                        var val = HelperUtility.ChangeType(ditKeyValue[p2.Name], p2.PropertyType);
                        if (val != null && p2.CanWrite)
                        {
                            p2.SetValue(t, val, null);
                        }
                    }
                }
            }
            return t;
        }

        #endregion

        /// <summary>
        /// 是否月末N小时
        /// </summary>
        /// <param name="hours">月末多少小时</param>
        /// <returns>返回是或否</returns>
        public static bool IsMonthLastByHours(int hours)
        {
            DateTime now = DateTime.Now.AddMonths(1);
            var nextdate = new DateTime(now.Year, now.Month, 1, 0, 0, 0);
            if (nextdate.AddHours(-hours) <= DateTime.Now)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 是否月末N小时
        /// </summary>
        /// <param name="days">倒数第N天</param>
        /// <returns>返回是或否</returns>
        public static bool IsMonthLastByDays(int days)
        {
            return IsMonthLastByHours(days * 24);
        }



        /// <summary>
        /// 把表数据转换成数据列表
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="table">数据表</param>
        /// <returns></returns>
        public static List<T> GetEntities<T>(DataTable table)
        {
            var list = new List<T>();
            //为null时
            if (table == null || table.Rows.Count == 0)
            {
                return list;
            }
            foreach (DataRow row in table.Rows)
            {
                var t = Activator.CreateInstance<T>();
                var propertypes = t.GetType().GetProperties();
                foreach (var pro in propertypes)
                {
                    if (!table.Columns.Contains(pro.Name.ToUpper()))
                    {
                        continue;
                    }
                    var val = ChangeType(row[pro.Name], pro.PropertyType);
                    if (val != null)
                    {
                        pro.SetValue(t, val, null);
                    }
                }
                list.Add(t);
            }
            return list;
        }
        /// <summary>
        /// 转换值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="conversionType">值类型</param>
        /// <returns></returns>
        public static object ChangeType(object value, Type conversionType)
        {
            try
            {
                //为null时
                if (value == null)
                {
                    return null;
                }
                string temp = value.ToString().Trim();
                if (conversionType == typeof(string))
                {
                    return temp;
                }

                //为null时
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return null;
                }
                if (conversionType == typeof(Guid))
                {
                    return new Guid(temp);
                }
                if (conversionType == typeof(int))
                {
                    return int.Parse(temp);
                }
                if (conversionType == typeof(decimal))
                {
                    return decimal.Parse(temp);
                }
                if (conversionType == typeof(bool))
                {
                    return (temp != "0" && (!new[] { "false", "off" }.Contains(temp.ToLower())));
                }
                if (conversionType == typeof(XDocument))
                {
                    if (value.GetType() == typeof(XDocument))
                    {
                        return value;
                    }
                    return XDocument.Parse(temp);
                }
                if (conversionType == typeof(DateTime))
                {
                    DateTime time;
                    if (DateTime.TryParse(temp, out time))
                    {
                        return time;
                    }
                    if (temp.Length == 14 && DateTime.TryParseExact(temp, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
                    {
                        return time;
                    }
                    if (temp.Length == 8 && DateTime.TryParseExact(temp, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
                    {
                        return time;
                    }
                }
                if (conversionType.IsGenericType &&
                    conversionType.GetGenericTypeDefinition() == (typeof(Nullable<>)))
                {
                    var nullableConverter = new NullableConverter(conversionType);
                    //枚举类型
                    if (conversionType.IsEnum)
                    {
                        int val;
                        if (int.TryParse(value.ToString(), out val))
                        {
                            return Enum.ToObject(nullableConverter.UnderlyingType, val);
                        }
                    }
                    if (nullableConverter.UnderlyingType.IsEnum)
                    {
                        int val;
                        if (int.TryParse(value.ToString(), out val))
                        {
                            return Enum.ToObject(nullableConverter.UnderlyingType, val);
                        }
                    }
                    return Convert.ChangeType(value, nullableConverter.UnderlyingType);
                }
                //枚举值转换
                if (conversionType.BaseType == typeof(Enum))
                {
                    return (int)Enum.Parse(conversionType, value.ToString());
                }
                return Convert.ChangeType(value, conversionType);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取实体对象属性值
        /// </summary>
        /// <param name="obj">实体</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="errorValue">获取错误时返回的值</param>
        /// <returns></returns>
        public static string GetEntityPropertyValueByName(object obj, string propertyName, string errorValue = "")
        {
            try
            {
                return obj.GetType().GetProperty(propertyName).GetValue(obj, null).ToString();
            }
            catch
            {
                return errorValue;
            }
        }

        /// <summary>
        /// 设置实体对象属性值
        /// </summary>
        /// <param name="obj">实体</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void SetEntityPropertyValue(object obj, string propertyName, object value)
        {
            var property = obj.GetType().GetProperty(propertyName);
            if (property != null && property.CanWrite)
            {
                property.SetValue(obj, value, null);
            }
        }
    }
}
