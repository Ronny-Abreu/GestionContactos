using GestionContactos.Domain.Entities;
using GestionContactos.Infrastructure.Repositories;


namespace GestionContactos.Application.Services
{
    public class DatoUsuarioService
    {
        private readonly DatoUsuarioRepository _repository;

        public DatoUsuarioService(DatoUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DatoUsuario>> ObtenerTodos()
        {
            return await _repository.ObtenerTodos();
        }

        public async Task<DatoUsuario> ObtenerPorId(int id)
        {
            return await _repository.ObtenerPorId(id);
        }

        public async Task Agregar(DatoUsuario producto)
        {
            await _repository.Agregar(producto);
        }

        public async Task Actualizar(DatoUsuario producto)
        {
            await _repository.Actualizar(producto);
        }

        public async Task Eliminar(int id)
        {
            await _repository.Eliminar(id);
        }
    }
}