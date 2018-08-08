using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.Entity.Domain.Models.Menu
{
    /// <summary>
    /// 系统用户权限
    /// </summary>
    [Table("SysUserOfRight")]
    public class SysUserOfRight
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        public string RightCode { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public int ModifyUserId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }
    }
}
