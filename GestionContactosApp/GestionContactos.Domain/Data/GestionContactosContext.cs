using Microsoft.EntityFrameworkCore;
using GestionContactos.Domain.Entities;

namespace GestionContactos.Domain.Data
{
    public class GestionContactosContext : DbContext
    {
        public GestionContactosContext(DbContextOptions<GestionContactosContext> options) : base(options) { }

        public DbSet<DatoUsuario> Datos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Datos iniciales
            modelBuilder.Entity<DatoUsuario>().HasData(
                new DatoUsuario { Id = 1, Nombre = "Ronny", Apellido = "De León", Correo = "dleonRonny@gmail.com", Telefono = "809-865-2927" },
                new DatoUsuario { Id = 2, Nombre = "Pipe", Apellido = "Almonte", Correo = "PipeAlm@gmail.com", Telefono = "829-092-6767" },
                new DatoUsuario { Id = 3, Nombre = "Diego", Apellido = "Perez", Correo = "DiegoPPerez", Telefono = "809-000-0000" }
            );
        }
    }
}
