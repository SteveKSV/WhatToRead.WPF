using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToRead.Domain.Model;
using WhatToRead.Domain.Queries;
using WhatToRead.EntityFramework.Dtos;

namespace WhatToRead.EntityFramework.Queries
{
    public class GetAllBooksQuery : IGetAllBooksQuery
    {
        private readonly WhatToReadDbContextFactory _contextFactory;

        public GetAllBooksQuery(WhatToReadDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Book>> Execute()
        {
            using (WhatToReadDbContext context = _contextFactory.Create())
            {
                IEnumerable<BookDto> booksDto = await context.Books.ToListAsync();

                return booksDto.Select(y=> new Book(y.Id, y.Title, y.Author, y.Pages, y.Language, y.Publisher));
            }
        }
    }
}
