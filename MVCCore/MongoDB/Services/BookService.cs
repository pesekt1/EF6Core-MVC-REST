﻿using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MVCCore.MongoDB.Models;

namespace MVCCore.MongoDB.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MONGO_DB"));
            _books = database.GetCollection<Book>("Books");
        }

        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) => 
            _books.DeleteOne(book => book.Id == id);
    }
}