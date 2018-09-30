using book_editor.service.CoverServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace book_editor.web.Controllers
{
    public class CoverController : Controller
    {
        private readonly ICoverService _coverServie;
        public CoverController(ICoverService coverServie)
        {
            _coverServie = coverServie;
        }

        public ActionResult GetCover(int id)
        {
            var cover = _coverServie.Get(id);
            string fileType = "image/jpg";
            string fileName = "";

            if (cover == null)
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(@"~/Files/emptyCover.jpg");
                fileName = "defaultCover.jpg";
                return File(fileBytes, fileType, fileName);
            }

            byte[] mas = cover.File;
            fileName = cover.FileName;
            return File(mas, fileType, fileName);
        }

        [HttpPost]
        public int SaveCover(int bookId, HttpPostedFileBase cover)
        {
            if (!ModelState.IsValid)
            {
                return 0;
            }
            int Id = _coverServie.Save(bookId, cover);
            return Id;

        }

        [HttpPost]
        public ActionResult RemoveCover(int id)
        {
            _coverServie.Delete(id);
            return Json(true);
        }
    }
}