using System.Windows.Input;
using WhatToRead.WPF.Commands;
using WhatToRead.WPF.Stores;

namespace WhatToRead.WPF.ViewModel
{
    public class BookViewModel : ViewModelBase
    {
        public BookListingViewModel BookListingViewModel { get; }
        public BookDetailsViewModel BookDetailsViewModel { get; }

        private bool _isLoading;

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
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

        public ICommand AddBookCommand { get; }
        public ICommand LoadBooksCommand { get; }
        public BookViewModel(SelectedBookStore _selectedBookStore, ModalNavigationStore modalNavigationStore, BooksStore booksStore)
        {
            BookListingViewModel = new BookListingViewModel(booksStore, _selectedBookStore, modalNavigationStore);
            BookDetailsViewModel = new BookDetailsViewModel(_selectedBookStore);

            LoadBooksCommand = new LoadBooksCommand(this, booksStore);
            AddBookCommand = new OpenAddBookCommand(booksStore, modalNavigationStore);
        }

        public static BookViewModel LoadViewModel(
            BooksStore booksStore,
            SelectedBookStore selectedBookStore,
            ModalNavigationStore modalNavigationStore
            )
        {
            BookViewModel viewModel = new BookViewModel(selectedBookStore, modalNavigationStore, booksStore);

            viewModel.LoadBooksCommand.Execute(null);
             
            return viewModel;
        }
    }
}
