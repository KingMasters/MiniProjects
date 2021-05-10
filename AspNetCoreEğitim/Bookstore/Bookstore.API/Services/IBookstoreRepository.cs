using Bookstore.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Services
{
    public interface IBookstoreRepository
    {
        bool AuthorExists(int authorId);

        IEnumerable<Author> GetAuthors();

        Author GetAuthor(int authorId);

        IEnumerable<Book> GetBooksForAuthor(int authorId);

        Book GetBookForAuthor(int authorId, int bookId);

        void Delete(Book book);

        bool Save();
    }
}
