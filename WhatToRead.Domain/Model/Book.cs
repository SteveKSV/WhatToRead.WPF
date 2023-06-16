using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatToRead.Domain.Model
{
    public class Book
    {
        public Guid Id { get; }
        public string Title { get;  }
        public string Author { get; }
        public int Pages { get; }
        public string Language { get; }
        public string Publisher { get; }

        public Book(Guid id, string title, string author, int pages, string language, string publisher)
        {
            Id = id;
            Title = title;
            Author = author;
            Pages = pages;
            Language = language;
            Publisher = publisher;
        }
    }
}
