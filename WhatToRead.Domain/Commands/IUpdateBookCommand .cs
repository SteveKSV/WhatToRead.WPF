using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToRead.Domain.Model;

namespace WhatToRead.Domain.Commands
{
    public interface IUpdateBookCommand
    {
        Task Execute(Book book);
    }
}
    