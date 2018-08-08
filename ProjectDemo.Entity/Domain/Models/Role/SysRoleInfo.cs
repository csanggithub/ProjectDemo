using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.Entity.Domain.Models.Role
{
    /// <summary>
    /// 系统角色表
    /// </summary>
    [Table("SysRoleInfo")]
    public class SysRoleInfo
    {
        //RoleId RoleName Permission State Remark CreateUserId CreateTime ModifyUserId ModifyTime
        /// <summary>
        /// 角色Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 权限编码
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
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
