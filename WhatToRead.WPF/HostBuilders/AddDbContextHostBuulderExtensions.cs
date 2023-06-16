using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WhatToRead.EntityFramework;

namespace WhatToRead.WPF.HostBuilders
{
    public static class AddDbContextHostBuulderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
             {
                 string connectionString = context.Configuration.GetConnectionString("Sqlite");
                 services.AddSingleton<DbContextOptions>(
                     new DbContextOptionsBuilder()
                     .UseSqlite(connectionString).Options);

                 services.AddSingleton<WhatToReadDbContextFactory>();
             });

            return hostBuilder;
        }
    }
}
