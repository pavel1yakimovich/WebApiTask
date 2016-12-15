using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

namespace DAL
{
    public class BookRepository : IDisposable
    {
        private BookContext ctx = new BookContext();

        public IQueryable<Book> GetAll() => ctx.Books;

        public Book GetBookById(int id) => ctx.Books.Find(id);
        public void Create(Book book)
        {
            ctx.Books.Add(book);
        }

        public void Update(Book book)
        {
            ctx.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Book book = ctx.Books.Find(id);
            if (book != null)
                ctx.Books.Remove(book);
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ctx.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
