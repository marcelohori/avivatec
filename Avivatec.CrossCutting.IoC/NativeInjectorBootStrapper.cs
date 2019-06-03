using AutoMapper;
using Avivatec.Business.Mapping;
using Avivatec.CrossCutting.IoC.Middleware;
using Avivatec.CrossCutting.IoC.ScopeInjectors;
using Avivatec.Data.Context;
using Avivatec.Data.Repositories;
using Avivatec.Domain.Interfaces.Repositories;
using Avivatec.Domain.UoW;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Text;

namespace Avivatec.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AvivatecDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
         
            
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
           

            RepositoryScopeInjector.Add(services);
            ServiceScopeInjector.Add(services);

           
            services.AddCors(o => o.AddPolicy("Policy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

           

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Avivatec API",
                        Version = "v1",
                    }
                );
            });



            services.AddMvcCore()//.SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddAuthorization()
             .AddJsonFormatters()
            .AddApiExplorer();

         
            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
            {

                //Metodo para tratar virgula no banco
                var usCulture = new CultureInfo("en-US");
                var supportedCultures = new[] { usCulture };

                if (env.IsDevelopment())
                {
                    app.UseMiddleware(typeof(ErrorHandlingMiddleware));
                }

                app.UseRequestLocalization(new RequestLocalizationOptions
                {
                    DefaultRequestCulture = new RequestCulture(usCulture),
                    SupportedCultures = supportedCultures,
                    SupportedUICultures = supportedCultures
                });

              

                app.UseCors("Policy");
               
                app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", "Avivatec v1");
            });

            app.UseMvc();

        }
    }
}

