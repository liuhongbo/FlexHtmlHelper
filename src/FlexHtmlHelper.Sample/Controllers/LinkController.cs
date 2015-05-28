using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlexHtmlHelperSample.Models;

namespace FlexHtmlHelperSample.Controllers
{
    public class LinkController : Controller
    {
        public LinkController()
        {
            var menu = new List<KeyValuePair<string, string>>();
            menu.Add(new KeyValuePair<string, string>("Link", "Link"));
            menu.Add(new KeyValuePair<string, string>("Button", "Link Button"));
            menu.Add(new KeyValuePair<string, string>("Paging", "Paging Link"));
            menu.Add(new KeyValuePair<string, string>("Ajax", "Ajax Link"));
            ViewBag.menu = menu;
        }

        private static List<Person> _personList;

        public ActionResult Link()
        {
            return View();
        }

        public ActionResult Button()
        {
            return View();
        }

        public ActionResult Paging(int page = 1)
        {
            var model = new PagingModel()
            {
                TotalItemCount = 1009,
                PageNumber = page,
                PageSize = 20,
                MaxPagingLinks = 15
            };
            return View(model);
        }

        [NonAction]
        private List<Person> GetModel()
        {
            if (_personList == null)
            {
                _personList = new List<Person>();

                _personList.Add(new Person() { PersonId = 1, FirstName = "John", LastName = "Lennon", Email = "john@flexhtmlhelper.com" });
                _personList.Add(new Person() { PersonId = 2, FirstName = "Tom", LastName = "Cruise", Email = "tom@flexhtmlhelper.com" });
                _personList.Add(new Person() { PersonId = 3, FirstName = "Don", LastName = "Flatt", Email = "don@flexhtmlhelper.com" });
            }

            return _personList;
        }

        public ActionResult Ajax()
        {

            return View(GetModel());
        }

        [HttpGet]
        public ActionResult AjaxEdit(int id)
        {           
            return PartialView("_AjaxEdit",GetModel()[id-1]);
        }

        [HttpPost]
        public ActionResult AjaxEdit(Person person)
        {
            if (this.ModelState.IsValid)
            {
                ViewData["ajax-save-result"] = "success";
                GetModel()[person.PersonId - 1] = person;
            }
            else
            {
                ViewData["ajax-save-result"] = "error";
            }
            return View("_AjaxEdit",person);
        }
	}
}