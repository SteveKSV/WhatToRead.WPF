using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhatToRead.WPF.Commands;
using WhatToRead.Domain.Model;
using WhatToRead.WPF.Stores;

namespace WhatToRead.WPF.ViewModel
{
    public class EditBookViewModel : ViewModelBase
    {
        public Guid BookId { get; }
        public BookDetailsFormViewModel BookDetailsFormViewModel { get; }

        public EditBookViewModel(Book book, BooksStore _booksStore, ModalNavigationStore modalNavigationStore)
        {
            BookId = book.Id;

            ICommand submitCommand = new EditBookCommand(this, _booksStore, modalNavigationStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore); 
            BookDetailsFormViewModel = new BookDetailsFormViewModel(submitCommand, cancelCommand)
            {
                Title = book.Title,
                Author = book.Author,
                Pages = book.Pages,
                Language = book.Language,
                Publisher = book.Publisher,
            };
        }
    }
}
