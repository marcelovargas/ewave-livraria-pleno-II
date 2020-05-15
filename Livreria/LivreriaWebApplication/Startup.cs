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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
