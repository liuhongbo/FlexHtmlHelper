using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlexHtmlHelperSample.Models;

namespace FlexHtmlHelperSample.Controllers
{
    public class InputController : Controller
    {
        public InputController()
        {
            var menu = new List<KeyValuePair<string, string>>();
            menu.Add(new KeyValuePair<string, string>("Label", "Label"));
            menu.Add(new KeyValuePair<string, string>("TextBox", "TextBox"));
            menu.Add(new KeyValuePair<string, string>("CheckBox", "CheckBox"));
            menu.Add(new KeyValuePair<string, string>("Radio", "Radio"));
            menu.Add(new KeyValuePair<string, string>("Select", "Select"));
            menu.Add(new KeyValuePair<string, string>("TextArea", "TextArea"));
            menu.Add(new KeyValuePair<string, string>("Style", "Style"));
            ViewBag.menu = menu;
        }

        public ActionResult Label()
        {
            var model = new Person()
            {
                FirstName = "",
                LastName = ""
            };

            foreach (var tzi in TimeZoneInfo.GetSystemTimeZones())
                model.AvailableTimeZones.Add(new SelectListItem() { Text = tzi.DisplayName, Value = tzi.Id, Selected = false });


            return View(model);
        }

        public ActionResult TextBox()
        {
            var model = new Person()
            {
                FirstName = "John",
                LastName = "Phillips",
                Password = "flex",
                PersonId = 1001
            };

            foreach (var tzi in TimeZoneInfo.GetSystemTimeZones())
                model.AvailableTimeZones.Add(new SelectListItem() { Text = tzi.DisplayName, Value = tzi.Id, Selected = false });


            return View(model);
        }

        public ActionResult CheckBox()
        {
            var model = new Person()
            {
                FirstName = "",
                LastName = "",
                Newsletter = true

            };

            foreach (var tzi in TimeZoneInfo.GetSystemTimeZones())
                model.AvailableTimeZones.Add(new SelectListItem() { Text = tzi.DisplayName, Value = tzi.Id, Selected = false });


            return View(model);
        }
        public ActionResult Radio()
        {
            var model = new Person()
            {
                FirstName = "",
                LastName = ""
            };

            foreach (var tzi in TimeZoneInfo.GetSystemTimeZones())
                model.AvailableTimeZones.Add(new SelectListItem() { Text = tzi.DisplayName, Value = tzi.Id, Selected = false });


            return View(model);
        }

        //
        // GET: /Input/
        public ActionResult Select()
        {
            var model = new Person()
            {
                FirstName = "",
                LastName = ""
            };

            foreach (var tzi in TimeZoneInfo.GetSystemTimeZones())
                model.AvailableTimeZones.Add(new SelectListItem() { Text = tzi.DisplayName, Value = tzi.Id, Selected = false });


            return View(model);           
        }

        public ActionResult TextArea()
        {
            var model = new Person()
            {
                FirstName = "",
                LastName = ""
            };

            foreach (var tzi in TimeZoneInfo.GetSystemTimeZones())
                model.AvailableTimeZones.Add(new SelectListItem() { Text = tzi.DisplayName, Value = tzi.Id, Selected = false });


            return View(model);
        }

        public ActionResult Style()
        {
            var model = new Person()
            {
                FirstName = "",
                LastName = ""
            };

            foreach (var tzi in TimeZoneInfo.GetSystemTimeZones())
                model.AvailableTimeZones.Add(new SelectListItem() { Text = tzi.DisplayName, Value = tzi.Id, Selected = false });


            return View(model);
        }
	}
}