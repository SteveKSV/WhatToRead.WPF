using System;
using WhatToRead.Domain.Model;

namespace WhatToRead.WPF.Stores
{
    public class SelectedBookStore
    {
        private readonly BooksStore _booksStore;

        private Book _selectedBook;
        public Book SelectedBook
        {
            get
            {
                return _selectedBook;
            }
            set
            {
                _selectedBook = value;
                SelectedBookChanged?.Invoke();
            }
        }
        public event Action SelectedBookChanged;

        public SelectedBookStore(BooksStore booksStore)
        {
            _booksStore = booksStore;

            _booksStore.BookAdded += BooksStore_BookAdded;
            _booksStore.BookUpdated += BooksStore_BookUpdated;
        }

        private void BooksStore_BookAdded(Book book)
        {
            SelectedBook = book;
        }

        private void BooksStore_BookUpdated(Book book)
        {
            if (book.Id == SelectedBook?.Id)
            {
                SelectedBook = book;
            }
        }
    }
}
