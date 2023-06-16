using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToRead.Domain.Commands;
using WhatToRead.EntityFramework.Dtos;

namespace WhatToRead.EntityFramework.Commands
{
    public class DeleteBookCommand : IDeleteBookCommand
    {
        private readonly WhatToReadDbContextFactory _contextFactory;

        public DeleteBookCommand(WhatToReadDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Guid id)
        {
            using (WhatToReadDbContext context = _contextFactory.Create())
            {
                BookDto bookDto = new BookDto()
                { 
                    Id = id,
                };

                context.Books.Remove(bookDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
