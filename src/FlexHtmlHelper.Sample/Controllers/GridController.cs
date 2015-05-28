using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlexHtmlHelperSample.Models;

namespace FlexHtmlHelperSample.Controllers
{
    public class GridController : Controller
    {
        public GridController()
        {
            var menu = new List<KeyValuePair<string, string>>();
            menu.Add(new KeyValuePair<string, string>("Row", "Row"));
            menu.Add(new KeyValuePair<string, string>("Column", "Column"));
            menu.Add(new KeyValuePair<string, string>("ColumnOffset", "Column Offset"));
            menu.Add(new KeyValuePair<string, string>("ColumnOrder", "Column Order"));
            menu.Add(new KeyValuePair<string, string>("ColumnVisible", "Column Visible"));
            ViewBag.menu = menu;
        }

        public ActionResult Row()
        {
            var model = new Person()
            {

            };
            foreach (var tzi in TimeZoneInfo.GetSystemTimeZones())
                model.AvailableTimeZones.Add(new SelectListItem() { Text = tzi.DisplayName, Value = tzi.Id, Selected = false });
            return View(model);
        }

        public ActionResult Column()
        {
            var model = new Person()
            {
                
            };

            return View(model);
        }

        

        public ActionResult ColumnOffset()
        {
            var model = new Person()
            {

            };
            foreach (var tzi in TimeZoneInfo.GetSystemTimeZones())
                model.AvailableTimeZones.Add(new SelectListItem() { Text = tzi.DisplayName, Value = tzi.Id, Selected = false });
            return View(model);
        }

        public ActionResult ColumnOrder()
        {
            var model = new Person()
            {

            };
            foreach (var tzi in TimeZoneInfo.GetSystemTimeZones())
                model.AvailableTimeZones.Add(new SelectListItem() { Text = tzi.DisplayName, Value = tzi.Id, Selected = false });
            return View(model);
        }

        public ActionResult ColumnVisible()
        {
            var model = new Person()
            {

            };
            foreach (var tzi in TimeZoneInfo.GetSystemTimeZones())
                model.AvailableTimeZones.Add(new SelectListItem() { Text = tzi.DisplayName, Value = tzi.Id, Selected = false });
            return View(model);
        }
    }
}