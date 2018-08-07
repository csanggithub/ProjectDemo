using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.Entity.Test.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AccountName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public bool IsUse { get; set; }

        public string CardCode { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreatetimeUserId { get; set; }

    }
}
