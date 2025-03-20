using GestionContactos.Domain.Data;
using GestionContactos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionContactos.Infrastructure.Repositories
{
    public class DatoUsuarioRepository
    {
        private readonly GestionContactosContext _context;

        public DatoUsuarioRepository(GestionContactosContext context)
        {
            _context = context;
        }

        public async Task<List<DatoUsuario>> ObtenerTodos()
        {
            return await _context.Datos.ToListAsync();
        }

        public async Task<DatoUsuario> ObtenerPorId(int id)
        {
            return await _context.Datos.FindAsync(id);
        }

        public async Task Agregar(DatoUsuario datoUsuario)
        {
            _context.Datos.Add(datoUsuario);
            await _context.SaveChangesAsync();
        }

        public async Task Actualizar(DatoUsuario datoUsuario)
        {
            _context.Datos.Update(datoUsuario);
            await _context.SaveChangesAsync();
        }

        public async Task Eliminar(int id)
        {
            var dato = await _context.Datos.FindAsync(id);
            if (dato != null)
            {
                _context.Datos.Remove(dato);
                await _context.SaveChangesAsync();
            }
        }
    }
}
