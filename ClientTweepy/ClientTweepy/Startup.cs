using Application.Service;
using Application.Service.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System.IO;

namespace ClientTweepy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            string stringDeConexao = Configuration.GetConnectionString("conexaoMySQL");

            services.AddControllers();
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClientTweepy", Version = "v1" });
            });

           services.AddSingleton((ILogger)new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(Path.Combine("/var/log/ms_clienttweepy", "ms_clienttweepy.log"), rollingInterval: RollingInterval.Day)
            .WriteTo.Console(Serilog.Events.LogEventLevel.Debug)
            .CreateLogger());

            services.AddScoped<IFunctionService, FunctionService>();


            services.AddScoped<IFunctionRepository, FunctionRepository>();

        }

      


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClientTweepy v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
