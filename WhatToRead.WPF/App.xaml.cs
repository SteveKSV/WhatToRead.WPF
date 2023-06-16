using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using WhatToRead.Domain.Commands;
using WhatToRead.Domain.Queries;
using WhatToRead.EntityFramework;
using WhatToRead.EntityFramework.Commands;
using WhatToRead.EntityFramework.Queries;
using WhatToRead.WPF.Stores;
using WhatToRead.WPF.ViewModel;
using WhatToRead.WPF.HostBuilders;
namespace WhatToRead.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host; 
        public App()
        {

            _host = Host.CreateDefaultBuilder()
                .AddDbContext()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IGetAllBooksQuery, GetAllBooksQuery>();
                    services.AddSingleton<ICreateBookCommand, CreateBookCommand>();
                    services.AddSingleton<IUpdateBookCommand, UpdateBookCommand>();
                    services.AddSingleton<IDeleteBookCommand, DeleteBookCommand>();

                    services.AddSingleton<ModalNavigationStore>();
                    services.AddSingleton<BooksStore>();
                    services.AddSingleton<SelectedBookStore>();

                    services.AddTransient<BookViewModel>(CreateBookViewModel);

                    services.AddSingleton<MainViewModel>();

                    services.AddSingleton<MainWindow>((services) => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        private BookViewModel CreateBookViewModel(IServiceProvider services)
        {
            return BookViewModel.LoadViewModel(
                    services.GetRequiredService<BooksStore>(),
                    services.GetRequiredService<SelectedBookStore>(),
                    services.GetRequiredService<ModalNavigationStore>()
                );
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            var whatToReadDbContextFactory = _host.Services.GetRequiredService<WhatToReadDbContextFactory>();

            using (WhatToReadDbContext context = whatToReadDbContextFactory.Create())
            {
                context.Database.Migrate();
            }

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
