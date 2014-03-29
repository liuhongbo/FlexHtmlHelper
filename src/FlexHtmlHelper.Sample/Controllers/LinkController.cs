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
	}
}