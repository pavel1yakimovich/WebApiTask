using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.EF;

namespace WebApp.Controllers
{
    public class BookController : ApiController
    {
        private BookRepository repository = new BookRepository();

        public IHttpActionResult GetBook(int id)
        {
            Book book = repository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        public IHttpActionResult GetBooks()
        {
            var result = repository.GetAll().ToList();

            return Ok(result);
        }

        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook([FromBody]Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.Create(book);
            repository.Save();

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        public IHttpActionResult Put(int id,[FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            repository.Update(book);
            repository.Save();

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        public IHttpActionResult Delete(int id)
        {
            Book book = repository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            repository.Delete(id);
            repository.Save();

            return Ok(book);
        }
    }
}
