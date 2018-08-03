using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.Entity.WeiXin.Models
{
    [Table("users")]
    public class Users
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int UserId { get; set; }
        /// <summary>
        /// 账号（邮箱）
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Roles { get; set; }

        /// <summary>
        /// 部门id
        /// </summary>
        public int DepartmentId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [NotMapped]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 状态 0-删除 1-正常
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 入职时间
        /// </summary>
        public DateTime? TimeOfEntry { get; set; }
        /// <summary>
        /// 离职时间
        /// </summary>
        public DateTime? QuitTime { get; set; }
    }
}
