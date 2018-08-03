using ProjectDemo.Web.Controllers.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectDemo.Web.Controllers.Areas.Test
{
    public class HomeController: Controller
    {
        [Test]
        public ActionResult Index()
        {
            return View();
        }
    }
}
