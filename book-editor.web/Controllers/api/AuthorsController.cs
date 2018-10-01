using book_editor.service.AuthorsServices;
using book_editor.service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace book_editor.web.Controllers.api
{
    public class AuthorsController : ApiController
    {
        #region ctor

        IAuthorsService _authorsService;
        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        #endregion


        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(_authorsService.Get(id));
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]AuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            model = _authorsService.Create(model);
            return Ok(model);
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]AuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_authorsService.Update(model));
        }

        [HttpDelete]
        public void Delete([FromBody]AuthorViewModel model)
        {
            _authorsService.Delete(model);

        }
    }
}
