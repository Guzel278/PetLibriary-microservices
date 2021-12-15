using Libriary.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libriary.API.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBook();
        Task<Book> GetBook(string id);
        Task<IEnumerable<Book>> GetBookByName(string name);
        Task<IEnumerable<Book>> GetBookByCategory(string categoryName);

        Task CreateBook(Book book);
        Task<bool> UpdateBook(Book book);
        Task<bool> DeleteBook(string id);
    }
}
