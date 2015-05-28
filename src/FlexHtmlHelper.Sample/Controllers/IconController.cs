using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlexHtmlHelperSample.Controllers
{
    public class IconController : Controller
    {
        public IconController()
        {
            var menu = new List<KeyValuePair<string, string>>();
            menu.Add(new KeyValuePair<string, string>("Icon", "Icon"));
            menu.Add(new KeyValuePair<string, string>("Button", "Button Icon"));
            ViewBag.menu = menu;
        }

        public ActionResult Icon()
        {
            return View();
        }

        public ActionResult Button()
        {
            return View();
        }
	}
}