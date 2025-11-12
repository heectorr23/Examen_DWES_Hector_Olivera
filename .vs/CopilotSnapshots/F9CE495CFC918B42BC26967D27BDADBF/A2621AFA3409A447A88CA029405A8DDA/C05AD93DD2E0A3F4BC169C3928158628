using Microsoft.EntityFrameworkCore;
using SupermercadoCRUD2.Models.Entity;

namespace SupermercadoCRUD.Data
{
    public class SupermercadoContext : DbContext
    {
        public SupermercadoContext(DbContextOptions<SupermercadoContext> options)
            : base(options)
        {
        }

        public DbSet<Venta> Ventas { get; set; }
    }
}
