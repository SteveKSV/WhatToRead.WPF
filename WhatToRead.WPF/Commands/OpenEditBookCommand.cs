using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToRead.Domain.Model;
using WhatToRead.WPF.Stores;
using WhatToRead.WPF.ViewModel;

namespace WhatToRead.WPF.Commands
{
    public class OpenEditBookCommand : CommandBase
    { 
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly BookListingItemViewModel _bookListingItemViewModel;
        private readonly BooksStore _booksStore;

        public OpenEditBookCommand(BookListingItemViewModel bookListingItemViewModel, BooksStore booksStore, ModalNavigationStore modalNavigationStore)
        {
            _bookListingItemViewModel = bookListingItemViewModel;
            _booksStore = booksStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object? parameter)
        {
            Book book = _bookListingItemViewModel.Book;

            EditBookViewModel editBookViewModel = new EditBookViewModel(book, _booksStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = editBookViewModel;
        }
    }
}
