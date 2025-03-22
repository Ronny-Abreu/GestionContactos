using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GestionContactos.Infrastructure.Context
{
    public class GestionContactosContextFactory : IDesignTimeDbContextFactory<GestionContactosContext>
    {
        public GestionContactosContext CreateDbContext(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<GestionContactosContext>();

            // Obtengo el directorio base del proyecto API
            string basePath = Directory.GetCurrentDirectory();
            Console.WriteLine($"Directorio actual: {basePath}");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            //Conexión a la base de datos
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine($"Cadena de conexión: {connectionString}");

            optionsBuilder.UseSqlServer(connectionString);


            return new GestionContactosContext(optionsBuilder.Options);
        }
    }
}
