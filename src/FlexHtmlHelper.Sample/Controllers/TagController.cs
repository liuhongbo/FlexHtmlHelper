using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlexHtmlHelperSample.Controllers
{
    public class TagController : Controller
    {
        public TagController()
        {
            var menu = new List<KeyValuePair<string, string>>();
            menu.Add(new KeyValuePair<string, string>("Tag", "Tag"));
            ViewBag.menu = menu;
        }

        public ActionResult Tag()
        {
            return View();
        }
	}
}