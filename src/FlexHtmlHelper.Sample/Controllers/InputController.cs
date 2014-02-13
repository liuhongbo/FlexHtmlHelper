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
	}
}