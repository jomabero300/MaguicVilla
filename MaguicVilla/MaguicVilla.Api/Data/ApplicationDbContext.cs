using MaguicVilla.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MaguicVilla.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<Gps> Gps { get; set; }
        public DbSet<GpsTrasabilidad> GpsTrasabilidads { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                    new Villa(){
                        Id= 1,
                        Nombre="Villa Real",
                        Detalle="detalle de la villa",
                        ImagenUrl="",
                        Ocupantes=5,
                        MetrosCuadrados=50,
                        Tarifa=200,
                        Amenida="",
                        FechaActualizacion=DateTime.Now,
                        FechaCreacion=DateTime.Now
                    },
                    new Villa(){
                        Id= 2,
                        Nombre="Premio Vista a la piscina",
                        Detalle="detalle de la vista a la piscina",
                        ImagenUrl="",
                        Ocupantes=4,
                        MetrosCuadrados=40,
                        Tarifa=150,
                        Amenida="",
                        FechaActualizacion=DateTime.Now,
                        FechaCreacion=DateTime.Now
                    }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
