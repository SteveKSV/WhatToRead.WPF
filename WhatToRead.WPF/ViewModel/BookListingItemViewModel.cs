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
    public class BookListingItemViewModel : ViewModelBase
    {
        public Book Book { get; private set; }

        public string Title => Book.Title;
        public string Author => Book.Author;


        private bool _isDeleting;
        public bool IsDeleting
        {
            get
            {
                return _isDeleting;
            }
            set
            {
                _isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));

            }
        }
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public BookListingItemViewModel(Book book, BooksStore booksStore, ModalNavigationStore modalNavigationStore)
        {
            Book = book;

            EditCommand = new OpenEditBookCommand(this, booksStore, modalNavigationStore);
            DeleteCommand = new DeleteBookCommand(this, booksStore);
        }

        public void Update(Book book)
        {
            Book = book;
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Author));
        }
    }
}
