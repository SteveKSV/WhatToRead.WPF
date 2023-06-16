﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToRead.Domain.Commands;
using WhatToRead.Domain.Model;
using WhatToRead.EntityFramework.Dtos;

namespace WhatToRead.EntityFramework.Commands
{
    public class UpdateBookCommand : IUpdateBookCommand
    {
        private readonly WhatToReadDbContextFactory _contextFactory;

        public UpdateBookCommand(WhatToReadDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Book book)
        {
            using (WhatToReadDbContext context = _contextFactory.Create())
            {
                BookDto bookDto = new BookDto()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Pages = book.Pages,
                    Language = book.Language,
                    Publisher = book.Publisher,
                };

                context.Books.Update(bookDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
