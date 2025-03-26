using CollectorsCantina.Application.UseCases.Collections;
using CollectorsCantina.Application.UseCases.Images;
using CollectorsCantina.Application.UseCases.Items;
using CollectorsCantina.Domain.IUseCases.Collections;
using CollectorsCantina.Domain.IUseCases.Images;
using CollectorsCantina.Domain.IUseCases.Items;
using Microsoft.Extensions.DependencyInjection;

namespace CollectorsCantina.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICreateCollectionHandler, CreateCollectionHandler>();
            services.AddScoped<IUpdateCollectionHandler, UpdateCollectionHandler>();
            services.AddScoped<IDeleteCollectionHandler, DeleteCollectionHandler>();
            services.AddScoped<IGetCollectionHandler, GetCollectionHandler>();
            services.AddScoped<IUploadImageHandler, UploadImageHandler>();
            services.AddScoped<IDeleteImageHandler, DeleteImageHandler>();
            services.AddScoped<ICreateItemHandler, CreateItemHandler>();
            services.AddScoped<IUpdateItemHandler, UpdateItemHandler>();
            services.AddScoped<IDeleteItemHandler, DeleteItemHandler>();
            services.AddScoped<IGetItemHandler, GetItemHandler>();
            return services;
        }
    }
}
