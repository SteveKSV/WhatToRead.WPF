using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatToRead.EntityFramework.Dtos
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public string Language { get; set; }
        public string Publisher { get; set; }
    }
}
