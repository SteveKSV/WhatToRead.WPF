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
    public class DeleteBookCommand : AsyncCommandBase
    {
        private readonly BookListingItemViewModel _bookListingItemViewModel;
        private readonly BooksStore _booksStore;

        public DeleteBookCommand(BookListingItemViewModel bookListingItemViewModel, BooksStore booksStore)
        {
            _bookListingItemViewModel = bookListingItemViewModel;
            _booksStore = booksStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _bookListingItemViewModel.ErrorMessage = null;
            _bookListingItemViewModel.IsDeleting = true;

            Book book = _bookListingItemViewModel.Book;

            try
            {
                await _booksStore.Delete(book.Id);
            }
            catch (Exception)
            {

                _bookListingItemViewModel.ErrorMessage = "Failed to delete this book. Please try again later.";
            }
            finally
            {
                _bookListingItemViewModel.IsDeleting = false;
            }
        }
    }
}
