using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WhatToRead.WPF.Commands;
using WhatToRead.Domain.Model;
using WhatToRead.WPF.Stores;
using System;
using System.Collections.Specialized;

namespace WhatToRead.WPF.ViewModel
{
    public class BookListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<BookListingItemViewModel> _bookListingItemViewModels;
        private readonly BooksStore _booksStore;
        private readonly SelectedBookStore _selectedBookStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public IEnumerable<BookListingItemViewModel> BookListingItemViewModels => _bookListingItemViewModels;

        public BookListingItemViewModel SelectedBookListingItemViewModel
        {
            get
            {
                return _bookListingItemViewModels
                    .FirstOrDefault(y=>y.Book?.Id == _selectedBookStore.SelectedBook?.Id);
            }
            set
            {
                _selectedBookStore.SelectedBook = value?.Book;
            }
        }

        public BookListingViewModel(BooksStore booksStore, SelectedBookStore selectedBookStore, ModalNavigationStore modalNavigationStore)
        {
            _bookListingItemViewModels = new ObservableCollection<BookListingItemViewModel>();

            _booksStore = booksStore;
            _selectedBookStore = selectedBookStore;
            _modalNavigationStore = modalNavigationStore;

            _selectedBookStore.SelectedBookChanged += SelectedBookStore_SelectedBookChanged;

            _booksStore.BooksLoaded += BooksStore_BooksLoaded;
            _booksStore.BookAdded += BooksStore_BookAdded;
            _booksStore.BookUpdated += BooksStore_BookUpdated;
            _booksStore.BookDeleted += BooksStore_BookDeleted;

            _bookListingItemViewModels.CollectionChanged += BookListingViewModels_CollectionChanged;    
        }

        protected override void Dispose()
        {
            _selectedBookStore.SelectedBookChanged -= SelectedBookStore_SelectedBookChanged;

            _booksStore.BookAdded -= BooksStore_BookAdded;
            _booksStore.BookUpdated -= BooksStore_BookUpdated;
            _booksStore.BooksLoaded -= BooksStore_BooksLoaded;
            _booksStore.BookDeleted -= BooksStore_BookDeleted;
            base.Dispose();
        }
        private void AddBook(Book book)
        {
            _bookListingItemViewModels.Add(new BookListingItemViewModel(book, _booksStore, _modalNavigationStore));
        }

        private void BooksStore_BookAdded(Book book)
        {
            AddBook(book);
        }

        private void BooksStore_BookUpdated(Book book)
        {
            BookListingItemViewModel bookViewModel =
                _bookListingItemViewModels.FirstOrDefault(y => y.Book.Id == book.Id);

            if (bookViewModel != null)
            {
                bookViewModel.Update(book);
            }
        }

        private void BooksStore_BookDeleted(Guid id)
        {
            BookListingItemViewModel? itemViewModel = _bookListingItemViewModels.FirstOrDefault(y=>y.Book?.Id == id);

            if (itemViewModel != null)
            {
                _bookListingItemViewModels.Remove(itemViewModel);
            }
        }

        private void BooksStore_BooksLoaded()
        {
            _bookListingItemViewModels.Clear();

            foreach (Book book in _booksStore.Books)
            {
                AddBook(book);
            }
        }

        private void SelectedBookStore_SelectedBookChanged()
        {
            OnPropertyChanged(nameof(SelectedBookListingItemViewModel));
        }

        private void BookListingViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(SelectedBookListingItemViewModel));
        }
    }
}
