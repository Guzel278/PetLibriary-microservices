using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Libriary.API.Data;
using Libriary.API.Entities;
using MongoDB.Driver;

namespace Libriary.API.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ILibriaryContext context;

        public BookRepository(ILibriaryContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateBook(Book book)
        {
            await context.Books.InsertOneAsync(book);
        }

        public async Task<bool> DeleteBook(string id)
        {
            FilterDefinition<Book> filter = Builders<Book>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await context.Books
                                                      .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Book>> GetBook()
        {
            return await context
                   .Books
                   .Find(p => true)
                   .ToListAsync();
        }

        public async Task<Book> GetBook(string id)
        {
            return await context
                   .Books
                   .Find(p => p.Id == id)
                   .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Book>> GetBookByCategory(string categoryName)
        {
            FilterDefinition<Book> filter = Builders<Book>.Filter.Eq(p => p.Category, categoryName);
            return await context
                  .Books
                  .Find(filter)
                  .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBookByName(string name)
        {
            FilterDefinition<Book> filter = Builders<Book>.Filter.ElemMatch(p => p.Name, name);
            return await context
                  .Books
                  .Find(filter)
                  .ToListAsync();
        }

        public async Task<bool> UpdateBook(Book book)
        {
            var updateResult = await context
                                 .Books
                                 .ReplaceOneAsync(filter: g => g.Id == book.Id, replacement: book);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
