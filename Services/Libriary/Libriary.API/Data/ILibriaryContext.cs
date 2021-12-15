using Libriary.API.Entities;
using MongoDB.Driver;

namespace Libriary.API.Data
{
    public interface ILibriaryContext
    {
        IMongoCollection<Book> Books { get; }
    }
}
