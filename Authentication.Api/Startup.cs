using Authentication.Api.Configurations;
using Authentication.Domain.Handlers;
using Authentication.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Authentication.Domain.Repositories.Interfaces;
using Authentication.Domain.Repositories;
using Authentication.Domain.Services;
using System.Reflection;
using MediatR;

namespace Authentication.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerConfiguration();
            services.AddTokenConfiguration();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserHandler>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UserHandler>();
            services.AddMediatR(typeof(UserHandler).GetTypeInfo().Assembly);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerConfiguration();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
