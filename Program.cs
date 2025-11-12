using Microsoft.EntityFrameworkCore;
using SupermercadoCRUD.Data;
using SupermercadoCRUD2.Data.Repositorios;
using SupermercadoCRUD2.Servicios;

namespace SupermercadoCRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<SupermercadoContext>(op =>
            {
                string cadena = "Server=localhost;Database=ventas;Uid=root;Pwd=curso;";
                op.UseMySql(cadena, ServerVersion.AutoDetect(cadena));
            });
            
            // Registrar repositorios y servicios de Ventas
            builder.Services.AddScoped<IVentaRepositorio, VentaRepositorio>();
            builder.Services.AddScoped<ServicioVentas>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Ventas/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Ventas}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
