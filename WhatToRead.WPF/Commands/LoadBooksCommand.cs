using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToRead.WPF.Stores;
using WhatToRead.WPF.ViewModel;

namespace WhatToRead.WPF.Commands
{
    public class LoadBooksCommand : AsyncCommandBase
    {
        private readonly BookViewModel _bookViewModel;
        private readonly BooksStore _booksStore;

        public LoadBooksCommand(BookViewModel bookViewModel, BooksStore booksStore)
        {
            _bookViewModel = bookViewModel;
            _booksStore = booksStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _bookViewModel.ErrorMessage = null;
            _bookViewModel.IsLoading = true;
            try
            {
                await _booksStore.Load();
            }
            catch (Exception)
            {
                _bookViewModel.ErrorMessage = "Failed to load books. Please restart the application.";

            }
            finally
            {
                _bookViewModel.IsLoading = false;
            }
        }
    }
}
