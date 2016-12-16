using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Http;
using System.Web.Http.Description;
using ClientApp.Models;

namespace ClientApp.Controllers
{
    public class BookController : ApiController
    {
        private const string APP_PATH = "http://localhost:52679";

        public IHttpActionResult GetBook(int id)
        {
            HttpResponseMessage response;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    response = client.GetAsync(APP_PATH + $"/api/book/{id}").Result;

                    response.EnsureSuccessStatusCode();
                }
                catch
                {
                    return BadRequest();
                }
            }

            var jsonTask = response.Content.ReadAsAsync<Book>();
            jsonTask.Wait();

            return Ok(jsonTask.Result);
        }

        public IHttpActionResult GetBooks()
        {
            HttpResponseMessage response;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    response = client.GetAsync(APP_PATH + "/api/book/").Result;

                    response.EnsureSuccessStatusCode();
                }
                catch
                {
                    return BadRequest();
                }
            }

            var jsonTask = response.Content.ReadAsAsync<List<Book>>();
            jsonTask.Wait();

            return Ok(jsonTask.Result);
        }

        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook([FromBody]Book book)
        {
            HttpResponseMessage response;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));

                    response = client.PostAsJsonAsync(APP_PATH + "/api/book/", book).Result;

                }
                catch
                {
                    return BadRequest();
                }
            }

            return Ok(response.StatusCode);
        }

        public IHttpActionResult Put(int id, [FromBody] Book book)
        {
            HttpResponseMessage response;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));

                    response = client.PutAsJsonAsync(APP_PATH + $"/api/book/{id}", book).Result;

                }
                catch
                {
                    return BadRequest();
                }
            }

            return Ok(response.StatusCode);
        }

        public IHttpActionResult Delete(int id)
        {
            HttpResponseMessage response;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    response = client.DeleteAsync(APP_PATH + $"/api/book/{id}").Result;
                }
                catch
                {
                    return BadRequest();
                }
            }

            return Ok(response.StatusCode);
        }
    }
}
