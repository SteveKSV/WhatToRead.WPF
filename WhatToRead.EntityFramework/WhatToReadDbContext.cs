using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToRead.EntityFramework.Dtos;

namespace WhatToRead.EntityFramework
{
    public class WhatToReadDbContext : DbContext
    {
        public WhatToReadDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BookDto> Books { get; set; }
    }
}
