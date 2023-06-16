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
    public class EditBookCommand : AsyncCommandBase
    {
        private readonly EditBookViewModel _editBookViewModel;
        private readonly BooksStore _booksStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public EditBookCommand(EditBookViewModel editBookViewModel, BooksStore booksStore, ModalNavigationStore modalNavigationStore)
        {
            _editBookViewModel = editBookViewModel;
            _booksStore = booksStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var formViewModel = _editBookViewModel.BookDetailsFormViewModel;

            formViewModel.ErrorMessage = null;
            formViewModel.IsSubmitting = true;

            Book book = new Book(
                _editBookViewModel.BookId,
                formViewModel.Title,
                formViewModel.Author,
                formViewModel.Pages,
                formViewModel.Language,
                formViewModel.Publisher);

            try
            {
                await _booksStore.Update(book);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                formViewModel.ErrorMessage = "Failed to update book. Please try again later.";
            }
            finally
            {
                formViewModel.IsSubmitting = false;
            }
        }
    }
}
