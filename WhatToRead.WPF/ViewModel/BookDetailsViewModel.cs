using WhatToRead.Domain.Model;
using WhatToRead.WPF.Stores;

namespace WhatToRead.WPF.ViewModel
{
    public class BookDetailsViewModel : ViewModelBase
    {
        private readonly SelectedBookStore _selectedBookStore;
        private Book SelectedBook => _selectedBookStore.SelectedBook;

        public bool HasSelectedBook => SelectedBook != null;
        public string Title => SelectedBook?.Title ?? "Unknown";
        public string Author => SelectedBook?.Author ?? "Unknown";
        public string Language => SelectedBook?.Language ?? "Unknown";
        public int Pages => SelectedBook?.Pages ?? 0;
        public string Publisher => SelectedBook?.Publisher ?? "Unknown";
        public BookDetailsViewModel(SelectedBookStore selectedBookStore)
        {
            _selectedBookStore = selectedBookStore;

            _selectedBookStore.SelectedBookChanged += SelectedBookStore_SelectedBookChanged;
        }
        protected override void Dispose()
        {
            _selectedBookStore.SelectedBookChanged -= SelectedBookStore_SelectedBookChanged;

            base.Dispose();
        }
        private void SelectedBookStore_SelectedBookChanged()
        {
            OnPropertyChanged(nameof(HasSelectedBook));
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Author));
            OnPropertyChanged(nameof(Language));
            OnPropertyChanged(nameof(Pages));
            OnPropertyChanged(nameof(Publisher));
        }
        
    }
}
