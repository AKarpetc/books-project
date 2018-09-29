using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace book_editor.web.Controllers
{
    public class CoverShowController : Controller
    {
        // GET: CoverShow
        public ActionResult Index()
        {
            //string file_path = Server.MapPath("я.png");
            //// Тип файла - content-type
            //string file_type = "application/pdf";
            //// Имя файла - необязательно
            //string file_name = "PDFIcon.pdf";
            return View();
        }
    }
}