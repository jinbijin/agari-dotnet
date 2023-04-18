using System;
using System.Text.Json.Serialization;
using Autofac;
using Logic;
using Logic.RoundRobin;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApi
{
    public class Startup
    {
        private readonly string _policy = "agariPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson().AddJsonOptions(config =>
            {
                config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            services.AddCors(options =>
            {
                options.AddPolicy(_policy, builder =>
                {
                    builder.WithOrigins("*")
                        .WithMethods("POST", "OPTIONS")
                        .WithHeaders("Content-Type", "Authentication")
                        .SetPreflightMaxAge(new TimeSpan(TimeSpan.TicksPerDay));
                });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<LogicModule>();
            builder.RegisterModule<RoundRobinModule>();
            builder.RegisterModule<WebApiModule>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_policy);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
