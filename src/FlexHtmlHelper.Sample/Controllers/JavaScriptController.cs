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
        
        public ActionResult Collapse()
        {
            var model = new FindPerson();

            return View(model);
        }
	}
}