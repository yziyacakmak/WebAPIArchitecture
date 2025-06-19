using App.Domain.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Bus
{
    public static class BusExtensions
    {
        public static IServiceCollection AddBusExt(this IServiceCollection services,IConfiguration configuration)
        {
            var serviceBusOption= configuration.GetSection(nameof(ServiceBusOption)).Get<ServiceBusOption>();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(serviceBusOption!.Url), h =>
                    { 
                        h.Username("guest");
                        h.Password("guest");
                    });
                });
            });


            return services;
        }
    }
}
