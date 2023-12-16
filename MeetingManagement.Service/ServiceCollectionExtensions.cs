using MeetingManagement.DAL;
using MeetingManagement.Repository;
using Microsoft.Extensions.DependencyInjection;



namespace MeetingManagement.Service
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddWebDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<WebDbContext>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMeetingRepository, MeetingRepository>();
            services.AddScoped<IMeetingItemRepository, MeetingItemRepository>();
            services.AddScoped<IMeetingItemsRepository, MeetingItemsRepository>();
            return services;
        }

        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddScoped<IMeetingService, MeetingService>();
            return services;
        }
    }
}
