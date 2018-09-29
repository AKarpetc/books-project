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
        // GET: CoverShow
        //public FileResult GetCover()
        //{
        //    //var model = _db.PhotoReport.Find(fileId);

        //    //if (model == null)
        //    //{
        //    //    return null;
        //    //}

        //    //byte[] mas = model.File;
        //    //string file_type = "image/jpg";
        //    //string file_name = model.FileName;
        //    //return File(mas, file_type, file_name);
        //    return View();
        //}
        [HttpPost]
        public async Task<ActionResult> SaveCover(HttpPostedFileBase cover)
        {
            if (ModelState.IsValid)
            {
                var file = cover;
                if (file != null && file.ContentLength > 0)
                {

                    byte[] content;
                    using (BinaryReader br = new BinaryReader(file.InputStream))
                    {
                        content = br.ReadBytes(file.ContentLength);
                    }

                    //var model = new PhotoReport
                    //{
                    //    CarWeightingId = carWeightingId,
                    //    FileName = file.FileName,
                    //    File = content
                    //};

                    //_db.PhotoReport.Add(model);

                }
            }
            return HttpNotFound();
        }
        public ActionResult RemoveCover()
        {
            return View();
        }
    }
}