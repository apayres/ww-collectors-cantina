using CollectorsCantina.Domain.IRepositories;
using CollectorsCantina.Domain.IStorage;
using CollectorsCantina.Infrastructure.Configuration;
using CollectorsCantina.Infrastructure.Repositories;
using CollectorsCantina.Infrastructure.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace CollectorsCantina.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, Action<InfrastructureOptions> options)
        {
            services.Configure<InfrastructureOptions>(options);
            services.AddScoped<ICollectionRepository, CollectionRepository>();
            services.AddScoped<IFileStorageManager, BlobStorageManager>();
            services.AddScoped<IItemRepository, ItemRepository>();
            return services;
        }
    }
}
