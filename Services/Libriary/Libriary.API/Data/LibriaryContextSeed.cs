using Libriary.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Libriary.API.Data
{
    public class LibriaryContextSeed
    {
        public static void SeedData(IMongoCollection<Book> bookCollection)
        {
            bool existBook = bookCollection.Find(p => true).Any();
            if (!existBook)
            {
                bookCollection.InsertManyAsync(GetPreconfiguredBooks());
            }
        }

        private static IEnumerable<Book> GetPreconfiguredBooks()
        {
            return new List<Book>()
            {
                new Book()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "War and Peace",
                    Author = "L.N. Tolstoy",
                    PublishingHouse = "Peterburg",
                    Quantity = 3,
                    Category = "Russian classics"
                    
                },
                new Book()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "Fathers and Sons",
                    Author = "I.S. Turgenev",
                    PublishingHouse = "Moscow",
                    Quantity = 5,
                    Category = "Russian classics"
                    
                }
            };
        }
    }
}
