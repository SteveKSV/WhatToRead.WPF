using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToRead.WPF.Stores;

namespace WhatToRead.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        public BookViewModel BookViewModel { get; }
        public ViewModelBase CurrentViewModel => _modalNavigationStore.CurrentViewModel;
        public bool IsModalOpen => _modalNavigationStore.IsOpen;
        public MainViewModel(ModalNavigationStore modalNavigationStore, BookViewModel bookViewModel)
        {
            _modalNavigationStore = modalNavigationStore;
            BookViewModel = bookViewModel;

            _modalNavigationStore.CurrentViewModelChanged += ModalNavigationStore_CurrentViewModelChanged;
        }

        protected override void Dispose()
        {
            _modalNavigationStore.CurrentViewModelChanged -= ModalNavigationStore_CurrentViewModelChanged;
            base.Dispose();
        }

        private void ModalNavigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
        }
    }
}
