using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.API.Services
{
    public class BookstoreRepository : IBookstoreRepository
    {
        private BookstoreDbContext _context;

        public BookstoreRepository(BookstoreDbContext context)
        {
            _context = context;
        }

        public bool AuthorExists(int authorId)
        {
            return _context.Authors.Any(o => o.Id == authorId);
        }

        public void Delete(Book book)
        {
            _context.Books.Remove(book);
        }

        public Author GetAuthor(int authorId)
        {
            var author = _context.Authors
                .FirstOrDefault(o => o.Id == authorId);

            return author;
        }

        public IEnumerable<Author> GetAuthors()
        {
            var authors = _context.Authors
                //.Include(o => o.Books)
                .OrderBy(o => o.Name)
                .ToList();

            return authors;
        }

        public Book GetBookForAuthor(int authorId, int bookId)
        {
            var book = _context.Books
                .FirstOrDefault(o => o.AuthorId == authorId 
                && o.Id == bookId);

            return book;
        }

        public IEnumerable<Book> GetBooksForAuthor(int authorId)
        {
            var books = _context.Books.Where(o => o.AuthorId == authorId);
            return books;
        }

        public bool Save()
        {
            return (_context.SaveChanges() > -1);
        }
    }
}
