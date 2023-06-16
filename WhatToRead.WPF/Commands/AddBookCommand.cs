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
    public class AddBookCommand : AsyncCommandBase
    {
        private readonly AddBookViewModel _addBookViewModel;
        private readonly BooksStore _booksStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public AddBookCommand(AddBookViewModel addBookViewModel, BooksStore booksStore, ModalNavigationStore modalNavigationStore)
        {
            _addBookViewModel = addBookViewModel;
            _booksStore = booksStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var formViewModel = _addBookViewModel.BookDetailsFormViewModel;

            formViewModel.ErrorMessage = null;
            formViewModel.IsSubmitting = true;

            formViewModel.IsSubmitting = true;
            Book book = new Book(
                Guid.NewGuid(),
                formViewModel.Title, 
                formViewModel.Author,
                formViewModel.Pages, 
                formViewModel.Language,
                formViewModel.Publisher);

            try
            {
                await _booksStore.Add(book);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                formViewModel.ErrorMessage = "Failed to add book. Please try again later."; 
            }
            finally { formViewModel.IsSubmitting = false; }
        }
    }
}
