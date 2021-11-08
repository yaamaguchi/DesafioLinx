using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using DesafioLinx.Business;
using System.Data.SqlClient;
using BD.Dal;

namespace DesafioLinx
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
           /* var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();*/
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            /*services.AddScoped<Service>(); 
            //services.AddMvc();

            Func<IServiceProvider, SqlConnection> Connect =
                a => new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
            services.AddScoped(Connect);
            services.AddScoped(typeof(IConnection), typeof(Connection));
            services.AddScoped(typeof(IDalProdutos), typeof(DalProdutos));

            services.AddControllers()
                .AddFluentValidation();

            services.AddSwaggerGen(c => {

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Cadastro de Produtos",
                        Version = "v1",
                        Description = "Desafio Linx - cadastro de produtos",
                        Contact = new OpenApiContact
                        {
                            Name = "Matheus Yamaguchi",
                            Url = new Uri("https://github.com/yaamaguchi")
                        }
                    });
            });

            services.AddApplicationInsightsTelemetry(Configuration);*/

            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cadastro de Produtos");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });*/
        }
    }
}
