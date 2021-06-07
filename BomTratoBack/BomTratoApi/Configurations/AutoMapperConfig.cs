using BomTratoApp.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
namespace BomTratoApi.Configurations
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddAutoMapper(typeof(EntityToModelMapping), typeof(ModelToEntityMapping));
        }
    }
}
