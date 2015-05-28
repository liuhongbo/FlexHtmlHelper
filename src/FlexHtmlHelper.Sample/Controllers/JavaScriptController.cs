using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlexHtmlHelperSample.Models;

namespace FlexHtmlHelperSample.Controllers
{
    public class JavaScriptController : Controller
    {
        public JavaScriptController()
        {
            var menu = new List<KeyValuePair<string, string>>();
            menu.Add(new KeyValuePair<string, string>("Collapse", "Collapse"));
            menu.Add(new KeyValuePair<string, string>("Modal", "Modal"));
            ViewBag.menu = menu;
        }

        public ActionResult Collapse()
        {
            var model = new FindPerson();

            return View(model);
        }

        public ActionResult Modal()
        {
            var model = new Person()
            {
                Log = "Login @ 127.0.0.1"
            };
            foreach (var tzi in TimeZoneInfo.GetSystemTimeZones())
                model.AvailableTimeZones.Add(new SelectListItem() { Text = tzi.DisplayName, Value = tzi.Id, Selected = false });

            model.AvailableFavoriteMusicGenres.Add(new SelectListItem() { Text = "African", Value = "African", Selected = false });
            model.AvailableFavoriteMusicGenres.Add(new SelectListItem() { Text = "Blues", Value = "Blues", Selected = false });
            model.AvailableFavoriteMusicGenres.Add(new SelectListItem() { Text = "Country", Value = "Country", Selected = false });
            model.AvailableFavoriteMusicGenres.Add(new SelectListItem() { Text = "Asian", Value = "Asian", Selected = false });
            return View(model);
        }
	}
}