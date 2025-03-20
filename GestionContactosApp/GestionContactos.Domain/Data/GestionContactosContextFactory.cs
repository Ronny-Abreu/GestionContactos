using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GestionContactos.Domain.Data
{
    public class GestionContactosContextFactory : IDesignTimeDbContextFactory<GestionContactosContext>
    {
        public GestionContactosContext CreateDbContext(string[] args)
        {
         
            var optionsBuilder = new DbContextOptionsBuilder<GestionContactosContext>();
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../GestionContactosApp.Api")) // Ruta al proyecto API
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new GestionContactosContext(optionsBuilder.Options);
        }
    }
}
