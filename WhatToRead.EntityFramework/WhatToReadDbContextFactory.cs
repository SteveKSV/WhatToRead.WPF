using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatToRead.EntityFramework
{
    public class WhatToReadDbContextFactory
    {
        private readonly DbContextOptions _options;

        public WhatToReadDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        public WhatToReadDbContext Create()
        {
            return new WhatToReadDbContext(_options);
        }
    }

}
