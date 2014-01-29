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
        public ActionResult Index()
        {
            var model = new LoginModel()
            {
                Username = "test",
                Password = "",
                RememberMe = false
            };

            return View(model);           
        }
	}
}