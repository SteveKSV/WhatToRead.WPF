using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhatToRead.WPF.Commands;
using WhatToRead.WPF.Stores;

namespace WhatToRead.WPF.ViewModel
{
    public class AddBookViewModel : ViewModelBase
    {

        public BookDetailsFormViewModel BookDetailsFormViewModel { get; }

        public AddBookViewModel(BooksStore booksStore, ModalNavigationStore modalNavigationStore)
        {
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            ICommand submitCommand = new AddBookCommand(this, booksStore, modalNavigationStore);
            BookDetailsFormViewModel = new BookDetailsFormViewModel(submitCommand, cancelCommand);
        }
    }
}
