using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectDemo.Web.Controllers.Areas.Admin
{
    public class AdminAreaRegistration:AreaRegistration
    {
        public override string AreaName => "Admin";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Areas_Admin_Default",
                url: "Admin/{controller}/{action}/{id}",
                defaults:new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces:new[] { "ProjectDemo.Web.Controllers.Areas.Admin" }
                );
        }
    }
}
    