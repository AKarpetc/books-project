using book_editor.service.AuthorsServices;
using book_editor.service.ViewModels;
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
        IAuthorsService _authorsService;
        public BooksController(IBookService bookService,IAuthorsService authorsService)
        {
            _bookService = bookService;
            _authorsService = authorsService;
        }

        private void AuthorsValidate(IEnumerable<AuthorViewModel> authors)
        {
            if (!authors.Any())
            {
                ModelState.AddModelError("Authors", "Дожен быть хотя бы один автор");
            }
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AuthorsValidate(model.Auctors);
            var book = _bookService.Create(model);
            return CreatedAtRoute("DefaultApi", new { book.Id }, new { Data = book, book.Id });

        }

        [HttpDelete]
        public void Delete(BookViewModel model)
        {
            _bookService.Delete(model);
           
        }

        [HttpPut]
        public IHttpActionResult Put(BookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AuthorsValidate(_authorsService.Get(model.Id));
            var book = _bookService.Update(model);
            return CreatedAtRoute("DefaultApi", new { book.Id }, new { Data = book, book.Id });

        }

    }
}
