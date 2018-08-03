using ProjectDemo.Entity.WeiXin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.Entity.WeiXin
{
    public class WeiXinDBContext:DbContext
    {
        public DbSet<Users> User { get; set; }

        public WeiXinDBContext():base("name=MyContext")
        {

        }
    }
}
