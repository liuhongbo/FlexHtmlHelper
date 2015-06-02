using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlexHtmlHelperSample.Models;

namespace FlexHtmlHelperSample.Controllers
{
    public class TextController : Controller
    {
        public TextController()
        {
            var menu = new List<KeyValuePair<string, string>>();
            menu.Add(new KeyValuePair<string, string>("Alignment", "Alignment"));
            menu.Add(new KeyValuePair<string, string>("Transformation", "Transformation"));
            menu.Add(new KeyValuePair<string, string>("ContextualColor", "ContextualColor"));
            menu.Add(new KeyValuePair<string, string>("ContextualBackground", "ContextualBackground"));
            
            ViewBag.menu = menu;
        }

        // GET: Text
        public ActionResult Index()
        {
            return RedirectToAction("Alignment");
        }

        public ActionResult Alignment()
        {
            var model = new Person()
            {
                 FirstName = "John",
                 Description = "The text-align property specifies the horizontal alignment of text in an element. Aligns the text to the left. Aligns the text to the right. Centers the text. Stretches the lines so that each line has equal width (like in newspapers and magazines)."
            };

            return View(model);
        }

        public ActionResult Transformation()
        {
            var model = new Person()
            {
                FirstName = "John",
                Description = "Bootstrap is the most popular HTML, CSS, and JS framework for developing responsive, mobile first projects on the web."
            };

            return View(model);
        }

        public ActionResult ContextualColor()
        {
            var model = new Person()
            {
                FirstName = "John",
                Description = "Bootstrap is the most popular HTML, CSS, and JS framework for developing responsive, mobile first projects on the web."
            };

            return View(model);
        }

        public ActionResult ContextualBackground()
        {
            var model = new Person()
            {
                FirstName = "John",
                Description = "Bootstrap is the most popular HTML, CSS, and JS framework for developing responsive, mobile first projects on the web."
            };

            return View(model);
        }
    }
}