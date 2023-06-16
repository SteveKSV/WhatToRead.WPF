using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatToRead.Domain.Commands
{
    public interface IDeleteBookCommand
    {
        Task Execute(Guid id);
    }
}
