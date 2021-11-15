using MVC.Data;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class BookService
    {
        private readonly Guid _userId;

        public BookService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateBook(BookAdd model)
        {
            var entity =
                new Book()
                {
                    OwnerId = _userId,
                    Name = model.Name,
                    Author = model.Author,
                    Customer = model.Customer
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Books.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public BookDetails GetBooksById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Books
                    .Single(e => e.BookId == id && e.OwnerId == _userId);
                return
                    new BookDetails
                    {
                        BookId = entity.BookId,
                        Name = entity.Name,
                        Author = entity.Author,
                        Customer = entity.Customer,
                    };
            }
        }
        public IEnumerable<BookListItem> GetBooks()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Books
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new BookListItem
                        {
                            BookId = e.BookId,
                            Name = e.Name,
                            Author = e.Author,
                            Customer = e.Customer
                        });
                return query.ToArray();
            }
        }
        public bool UpdateBook(BookEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx

                    .Books
                    .Single(e => e.BookId == model.ID && e.OwnerId == _userId);

                entity.Name = model.Name;
                entity.Author = model.Author;
                entity.Customer = model.Customer;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteBook(int bookId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx

                    .Books
                    .Single(e => e.BookId == bookId && e.OwnerId == _userId);
                ctx.Books.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
