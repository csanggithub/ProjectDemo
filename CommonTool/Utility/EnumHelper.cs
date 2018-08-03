using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CommonTool.Utility
{
    /// <summary>
    /// 枚举辅助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举标记中说明文字<br/>
        /// (枚举成员的[Description("说明文字")],使用说明:EnumHelper.GetDescription())
        /// </summary>
        /// <param name="en">枚举成员</param>
        /// <returns>返回说明文字</returns>
        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }

    }

    /// <summary>
    /// 提供指定类型的枚举操作类
    /// </summary>
    /// <remarks>提供枚举的操作类</remarks>
    /// <example>
    /// EnumHelper.GetEnumDescription, 
    /// </example>
    public class EnumHelper<T>
    {
        /// <summary>
        /// 根据枚举的值得到枚举描述属性
        /// </summary>
        /// <param name="pValue">枚举的值</param>
        public static string GetEnumDescription(int pValue)
        {
            Type pEnumType = typeof(T);
            if (pEnumType.IsEnum != true)
            {
                throw new InvalidOperationException();
            }
            Type typeDescription = typeof(DescriptionAttribute);
            FieldInfo[] fields = pEnumType.GetFields();
            string text = string.Empty;
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    if ((int)pEnumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null) == pValue)
                    {
                        object[] arr = field.GetCustomAttributes(typeDescription, true);
                        if (arr.Length > 0)
                        {
                            var aa = (DescriptionAttribute)arr[0];
                            text = aa.Description;
                        }
                        else
                        {
                            text = field.Name;
                        }
                        break;
                    }
                }
            }
            return text;
        }

        /// <summary>
        /// 基于Description值获得具体的枚举值
        /// </summary>
        /// <param name="pDescription">Description特性值或者元素的名称</param>
        /// <returns>返回枚举值。如果没找到，返回传入的Description值</returns>
        public static T GetEnumValueByDescription(string pDescription)
        {
            if (typeof(T).IsEnum)
            {
                Type typeDescription = typeof(DescriptionAttribute);

                foreach (FieldInfo fi in typeof(T).GetFields())
                {
                    object[] arr = fi.GetCustomAttributes(typeDescription, true);
                    if (arr.Length > 0)
                    {
                        var descriptionAttribute = arr[0] as DescriptionAttribute;
                        if (descriptionAttribute != null && pDescription == descriptionAttribute.Description)
                        {
                            return GetEnumInstance(fi.Name);
                        }
                    }
                }
            }

            return default(T);
        }

        /// <summary>
        /// 基于枚举值名称获得具体的枚举实例
        /// </summary>
        /// <param name="pName"></param>
        /// <example>例:Enum1枚举有两个成员A=0,B=1,则传入"A"或"0"获取 Enum1.A 枚举类</example>
        /// <returns></returns>
        public static T GetEnumInstance(string pName)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), pName);
            }
            catch { return default(T); }
        }


        /// <summary>
        /// 将枚举的值和描述保存到Dictionary
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetNumberDictionary()
        {
            #region Old
            //var result = new Dictionary<string, string>();
            //Type enumType = typeof(T);

            //if (enumType.IsEnum)
            //{
            //    FieldInfo[] fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
            //    Array array = Enum.GetValues(typeof(T));

            //    for (int i = 0; i < fields.Length; i++)
            //    {
            //        FieldInfo field = fields[i];

            //        object[] attrs = field.GetCustomAttributes(typeof(DescriptionAttribute), true);

            //        var value = ((int)array.GetValue(i)).ToString();
            //        var description = attrs.Length > 0
            //            ? ((DescriptionAttribute)attrs[0]).Description
            //            : String.Empty;

            //        if (!result.ContainsKey(value))
            //        {
            //            result.Add(value, description);
            //        }
            //    }
            //}

            //return result;
            #endregion
            var result = new Dictionary<string, string>();
            Type enumType = typeof(T);
            if (enumType.IsEnum)
            {
                foreach (var value in Enum.GetValues(typeof(T)))
                {
                    object[] objAttrs = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                    var description = string.Empty;
                    var v = Convert.ToInt32(value).ToString();
                    if (objAttrs != null && objAttrs.Length > 0)
                    {
                        DescriptionAttribute descAttr = objAttrs[0] as DescriptionAttribute;
                        description = descAttr.Description;
                    }
                    if (!result.ContainsKey(v))
                    {
                        result.Add(Convert.ToInt32(value).ToString(), description);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 将枚举的字段和描述保存到Dictionary
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetDictionary()
        {
            var result = new Dictionary<string, string>();
            Type enumType = typeof(T);

            if (enumType.IsEnum)
            {
                FieldInfo[] fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);

                foreach (FieldInfo field in fields)
                {
                    object[] attrs = field.GetCustomAttributes(typeof(DescriptionAttribute), true);

                    string code = field.Name;
                    string description = attrs.Length > 0 ?
                        ((DescriptionAttribute)attrs[0]).Description : String.Empty;

                    if (!result.ContainsKey(code))
                    {
                        result.Add(code, description);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 将Enum转换成List
        /// </summary>
        /// <returns></returns>
        public static List<KeyValuePair<string, string>> GetListFromEnum()
        {
            Type pEnumType = typeof(T);
            Type typeDescription = typeof(DescriptionAttribute);
            System.Reflection.FieldInfo[] fields = pEnumType.GetFields();
            var list = new List<KeyValuePair<string, string>>();

            foreach (System.Reflection.FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    var value = (int)pEnumType.InvokeMember(field.Name, System.Reflection.BindingFlags.GetField, null, null, null);
                    object[] arr = field.GetCustomAttributes(typeDescription, true);
                    string text;
                    if (arr.Length > 0)
                    {
                        var aa = (DescriptionAttribute)arr[0];
                        text = aa.Description;
                    }
                    else
                    {
                        text = field.Name;
                    }
                    list.Add(new KeyValuePair<string, string>(value.ToString(), text));
                }
            }
            return list;

        }

        /// <summary>
        /// 获取枚举值下拉框列表
        /// </summary>
        /// <param name="isShowAllItem">是否显示全部选项</param>
        /// <param name="isIntType">是否获取枚举整型值</param>
        /// <param name="filterValues">过滤值列表</param>
        /// <returns></returns>
        public static SelectList GetEnumSelectList(bool isShowAllItem = true, bool isIntType = true, List<string> filterValues = null)
        {
            Type enumType = typeof(T);
            var lstItem = new List<SelectListItem>();
            //显示全部选项
            if (isShowAllItem)
            {
                lstItem.Add(new SelectListItem { Text = "全部", Value = "" });
            }
            var nameList = Enum.GetNames(enumType);
            lstItem.AddRange(
                nameList.Select(item => (Enum)Enum.Parse(enumType, item))
                    .Select(tempenum => new SelectListItem
                    {
                        Selected = false,
                        Text = EnumHelper.GetDescription(tempenum),
                        Value = isIntType ? (Convert.ToInt32(tempenum)).ToString() : tempenum.ToString()
                    }));
            if (filterValues != null && filterValues.Any())
            {
                lstItem = lstItem.Where(r => !filterValues.Contains(r.Value)).ToList();
            }
            return new SelectList(lstItem, "Value", "Text");
        }

    }
}
