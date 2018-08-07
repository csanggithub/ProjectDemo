using ProjectDemo.Web.Controllers.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectDemo.Web.Controllers.Controllers.Test
{
    [Authentication("Admin",false)]
    public class HomeController: Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
