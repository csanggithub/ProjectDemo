using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectDemo.Web.Controllers.Areas.Test
{
    public class TestAreaRegistration:AreaRegistration
    {
        public override string AreaName
        {
            get { return "Test"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Areas_Test_Default",
                url: "Test/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces:new[] { "ProjectDemo.Web.Controllers.Areas.Test" }
                );
        }
    }
}
