using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.Entity.Domain.Models.Menu
{
    /// <summary>
    /// 系统菜单信息
    /// </summary>
    [Table("SysMenuInfo")]
    public class SysMenuInfo
    {
        /// <summary>
        /// 菜单ID Sys_01
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public string ParentMenuId { get; set; }

        /// <summary>
        /// 菜单类别
        /// </summary>
        public string MenuType { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单级别
        /// </summary>
        public int MenuGrade { get; set; }

        /// <summary>
        /// 序列
        /// </summary>
        public int OrderNo { get; set; }

        /// <summary>
        /// 图标样式
        /// </summary>
        public string IcoClass { get; set; }

        /// <summary>
        /// 链接URL
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 展开方式
        /// </summary>
        public string OpenTarget { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        ///  是否公开，为true时，对所有用户有效，与菜单权限无关
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// 是否节点
        /// </summary>
        public int IsLeaf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
