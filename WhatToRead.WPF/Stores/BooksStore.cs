using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToRead.Domain.Commands;
using WhatToRead.Domain.Model;
using WhatToRead.Domain.Queries;

namespace WhatToRead.WPF.Stores
{
    public class BooksStore
    {
        private readonly ICreateBookCommand _createBookCommand;
        private readonly IUpdateBookCommand _updateBookCommand;
        private readonly IDeleteBookCommand _deleteBookCommand;
        private readonly IGetAllBooksQuery _getAllBooksQuery;

        private readonly List<Book> _books;

        public IEnumerable<Book> Books => _books;

        public BooksStore(
            ICreateBookCommand createBookCommand,
            IUpdateBookCommand updateBookCommand,
            IDeleteBookCommand deleteBookCommand, 
            IGetAllBooksQuery getAllBooksQuery)
        {
            _createBookCommand = createBookCommand;
            _updateBookCommand = updateBookCommand;
            _deleteBookCommand = deleteBookCommand;
            _getAllBooksQuery = getAllBooksQuery;

            _books = new List<Book>();
        }

        public event Action BooksLoaded;
        public event Action<Book> BookAdded;
        public event Action<Book> BookUpdated;
        public event Action<Guid> BookDeleted;

        public async Task Load()
        {
            IEnumerable<Book> books = await _getAllBooksQuery.Execute();

            _books.Clear();
            _books.AddRange(books);

            BooksLoaded?.Invoke();
        }

        public async Task Add(Book book)
        {
            await _createBookCommand.Execute(book);

            _books.Add(book);

            BookAdded?.Invoke(book);
        }

        public async Task Update(Book book)
        {
            await _updateBookCommand.Execute(book);

            int currentIndex = _books.FindIndex(y => y.Id == book.Id);

            if(currentIndex != -1)
            {
                _books[currentIndex] = book;
            }
            else
            {
                _books.Add(book);
            }

            BookUpdated?.Invoke(book);  
        }

        public async Task Delete(Guid id)
        {
            await _deleteBookCommand.Execute(id);

            _books.RemoveAll(y=> y.Id == id);

            BookDeleted?.Invoke(id);
        }
    }
}
