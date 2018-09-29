using book_rditor.service.BookServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace book_editor.web.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
           return View();
        }
    }
}
