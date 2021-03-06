﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlexHtmlHelperSample.Models;


namespace FlexHtmlHelperSample.Controllers
{
    public class FormController : Controller
    {
        public FormController()
        {
            var menu = new List<KeyValuePair<string, string>>();
            menu.Add(new KeyValuePair<string, string>("Default", "Default"));
            menu.Add(new KeyValuePair<string, string>("Horizontal", "Horizontal"));
            menu.Add(new KeyValuePair<string, string>("Inline", "Inline"));
            menu.Add(new KeyValuePair<string, string>("Partial", "Partial"));
            menu.Add(new KeyValuePair<string, string>("Ajax", "Ajax"));
            menu.Add(new KeyValuePair<string, string>("Tag", "Tag"));
            ViewBag.menu = menu;
        }
        //
        // GET: /Form/
        public ActionResult Inline()
        {
            var model = new Person()
            {
                FirstName = "John",
                LastName = "",
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

        public ActionResult Horizontal()
        {
            var model = new Person()
            {
                FirstName = "John",
                LastName = "",
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

        public ActionResult Default()
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

        public ActionResult Partial()
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

        [HttpGet]
        public ActionResult Ajax()
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

        [HttpPost]
        [ActionName("Ajax")]
        public ActionResult AjaxUpdate(Person person)
        {
            return PartialView("_AjaxEdit",person);
        }

        public ActionResult Tag()
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