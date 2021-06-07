using BomTratoApi.Configurations;
using BomTratoApp.Hubs;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetDevPack.Identity;
using NetDevPack.Identity.User;

namespace BomTratoApi
{
    public class Startup
    {
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDatabaseConfiguration(Configuration);
            services.AddApiIdentityConfiguration(Configuration);
            services.AddAspNetUserConfiguration();
            services.AddAutoMapperConfiguration();
            services.AddSwaggerConfiguration();
            services.AddMediatR(typeof(Startup));
            services.AddDependencyInjectionConfiguration();
            services.AddCors(o => o.AddPolicy("AppPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithOrigins("http://localhost:4200",
                       "http://localhost:4201",
                       "http://localhost:4202")
                       .AllowCredentials();
            }));
            services.AddSignalR();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AppPolicy");
            app.UseAuthConfiguration();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<LiveChatHub>("/liveChatHub");
            });
            app.UseSwaggerSetup();
        }
    }
}
