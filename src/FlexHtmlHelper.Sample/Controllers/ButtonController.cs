using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlexHtmlHelperSample.Controllers
{
    public class ButtonController : Controller
    {
        public ButtonController()
        {
            var menu = new List<KeyValuePair<string, string>>();
            menu.Add(new KeyValuePair<string, string>("Button", "Button"));
            menu.Add(new KeyValuePair<string, string>("Group", "Button Group"));
            menu.Add(new KeyValuePair<string, string>("Toolbar", "Button Toolbar"));
            ViewBag.menu = menu;
        }
        //
        // GET: /Button/
        public ActionResult Button()
        {
            return View();
        }

	}
}