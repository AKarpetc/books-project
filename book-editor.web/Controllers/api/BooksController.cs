using book_rditor.service.BookServices;
using book_rditor.service.ViewModels;
using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace book_editor.web.Controllers.api
{
    public class BooksController : ApiController
    {
        public readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        [Route("api/Books/kendods")]
        public IHttpActionResult GetBooks(DataSourceRequest request)
        {
            return Ok(_bookService.Get(request));
        }

        [HttpPost]
        public IHttpActionResult Post(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = _bookService.Create(model);
                return CreatedAtRoute("DefaultApi", new { book.Id }, new { Data = book, book.Id });
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public IHttpActionResult Delete(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                _bookService.Delete(model);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IHttpActionResult Put(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = _bookService.Update(model);
                return CreatedAtRoute("DefaultApi", new { book.Id }, new { Data = book, book.Id });
            }
            return BadRequest(ModelState);
        }

    }
}
