using ApplicationApp.Interfaces;
using ApplicationApp.OpenApp;
using Domain;
using Infrastructure.Configuration;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Server.IISIntegration;
using ReflectionIT.Mvc.Paging;

namespace LivreriaWebApplication
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
            #region Serviços de Dados

            services.AddSingleton(typeof(IGeneric<>), typeof(GenericRepository<>));

            services.AddSingleton<IAutor, AutorRepository>();
            services.AddSingleton<IAutorApp, AutorApp>();

            services.AddSingleton<IEmprestimo, EmprestimoRepository>();
            services.AddSingleton<IEmprestimoApp, EmprestimoApp>();

            services.AddSingleton<IGenero, GeneroRepository>();
            services.AddSingleton<IGeneroApp, GeneroApp>();

            services.AddSingleton<IInstituicao, InstituicaoRepository>();
            services.AddSingleton<IInstituicaoApp, InstituicaoApp>();

            services.AddSingleton<ILivro, LivroRepository>();
            services.AddSingleton<ILivroApp, LivroApp>();

            services.AddSingleton<IReserva, ReservaRepository>();
            services.AddSingleton<IReservaApp, ReservaApp>();

            services.AddSingleton<IUsuario, UsuarioRepository>();
            services.AddSingleton<IUsuarioApp, UsuarioApp>();

            #endregion

            services.AddControllersWithViews();

            #region Swagger



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Editora To do API",
                    Description = "Swagger",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Carlos Marcelo Vargas Cuba",
                        Email = "marcelovargas.bo@gmail.com",
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            });
            #endregion

            services.AddPaging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Editora To do API");
                //c.RoutePrefix = string.Empty;
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
