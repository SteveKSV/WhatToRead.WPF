using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToRead.Domain.Model;

namespace WhatToRead.Domain.Queries
{
    public interface IGetAllBooksQuery
    {
        Task<IEnumerable<Book>> Execute();
    }
}
