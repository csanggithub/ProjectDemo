using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.Entity.Domain.Models.Menu
{
    /// <summary>
    /// 系统菜单权限
    /// </summary>
    [Table("SysRightOfMenu")]
    public class SysRightOfMenu
    {
        /// <summary>
        /// 权限编码
        /// </summary>
        public string RightCode { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public string MenuID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
