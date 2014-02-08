using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlexHtmlHelperSample.Models;


namespace FlexHtmlHelperSample.Controllers
{
    public class FormController : Controller
    {
        //
        // GET: /Form/
        public ActionResult Inline()
        {
            var model = new Person()
            {
                FirstName = "",
                LastName = ""
            };

            return View(model);
        }

        public ActionResult Horizontal()
        {
            var model = new Person()
            {
                FirstName = "",
                LastName = ""
            };

            return View(model);
        }

        public ActionResult Default()
        {
            var model = new Person()
            {
                FirstName = "",
                LastName = ""
            };

            return View(model);
        }
	}
}