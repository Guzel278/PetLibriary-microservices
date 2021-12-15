using Libriary.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace Libriary.API.Data
{
    public class LibriaryContext :ILibriaryContext
    {
        public LibriaryContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Books = database.GetCollection<Book>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

            LibriaryContextSeed.SeedData(Books);
        }

        public IMongoCollection<Book> Books { get; }
    }
}
