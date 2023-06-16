using System.Windows.Input;

namespace WhatToRead.WPF.ViewModel
{
    public class BookDetailsFormViewModel : ViewModelBase
    {
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        private string _author;
        public string Author
        {
            get
            {
                return _author;
            }
            set
            {
                _author = value;
                OnPropertyChanged(nameof(Author));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        private int _pages;
        public int Pages
        {
            get
            {
                return _pages;
            }
            set
            {
                _pages = value;
                OnPropertyChanged(nameof(Pages));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        private string _language;
        public string Language
        {
            get
            {
                return _language;
            }
            set
            {
                _language = value;
                OnPropertyChanged(nameof(Language));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        private string _publisher;
        public string Publisher
        {
            get
            {
                return _publisher;
            }
            set
            {
                _publisher = value;
                OnPropertyChanged(nameof(Publisher));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
        
        private bool _isSubmitting;
        public bool IsSubmitting
        {
            get
            {
                return _isSubmitting;
            }
            set
            {
                _isSubmitting = value;
                OnPropertyChanged(nameof(IsSubmitting));
            }
        }

        public bool CanSubmit => (!string.IsNullOrEmpty(_title) && !string.IsNullOrEmpty(_author) &&
                                    !string.IsNullOrEmpty(_language) && !string.IsNullOrEmpty(_publisher) && _pages > 0);

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
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public BookDetailsFormViewModel(ICommand submitCommand, ICommand cancelCommand)
        {
            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }
    }
}
