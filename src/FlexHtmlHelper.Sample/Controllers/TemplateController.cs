using FlexHtmlHelperSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlexHtmlHelperSample.Controllers
{
    public class TemplateController : Controller
    {

        public TemplateController()
        {
            var menu = new List<KeyValuePair<string, string>>();
            menu.Add(new KeyValuePair<string, string>("Form", "Form"));            
            ViewBag.menu = menu;
        }
        
        public ActionResult Form()
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