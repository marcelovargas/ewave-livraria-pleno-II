using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using LivreriaWeb.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Domain;
using ApplicationApp.Interfaces;
using Infrastructure.Repository;
using ApplicationApp.OpenApp;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using ReflectionIT.Mvc.Paging;

namespace LivreriaWeb
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

            services.AddSingleton<ILeitor, LeitorRepository>();
            services.AddSingleton<ILeitorApp, LeitorApp>();

            #endregion

            #region Serviços do App Web

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();


            #endregion

            services.AddControllersWithViews();
            services.AddRazorPages();

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
            #region Swagger

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Editora To do API");
                //c.RoutePrefix = string.Empty;
            });

            #endregion
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
