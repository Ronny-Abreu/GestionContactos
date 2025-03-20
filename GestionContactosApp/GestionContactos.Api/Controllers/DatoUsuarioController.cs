using GestionContactos.Application.Services;
using GestionContactos.Domain.Entities;
using GestionContactos.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GestionContactos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatoUsuarioController : ControllerBase
    {
        private readonly DatoUsuarioService _service;

        public DatoUsuarioController(DatoUsuarioService service)
        {
            _service = service;
        }

        // Obtener todos los datos de todos los usuarios
        [HttpGet]
        public async Task<ActionResult<List<DatoUsuarioDto>>> GetDatosUsuario()
        {
            var datos = await _service.ObtenerTodos();
            var DatoUsuarioDto = datos.Select(p => new DatoUsuario
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                Correo = p.Correo,
                Telefono = p.Telefono
            }).ToList();

            if (DatoUsuarioDto.Count == 0)
            {
                return NotFound(new { message = "No se encontraron los datos de este usuario." });
            }

            return Ok(new { message = "Datos obtenidos exitosamente.", data = DatoUsuarioDto });
        }

        // Obtener datos de un usuario por Id
        [HttpGet("{id}")]
        public async Task<ActionResult<DatoUsuarioDto>> GetDatosUsuario(int id)
        {
            var dato = await _service.ObtenerPorId(id);
            if (dato == null)
            {
                return NotFound(new { message = "Datos de este usuario no encontrado." });
            }

            var DatoUsuarioDto = new DatoUsuarioDto
            {
                Id = dato.Id,
                Nombre = dato.Nombre,
                Apellido = dato.Apellido,
                Correo = dato.Correo,
                Telefono= dato.Telefono
            };

            return Ok(new { message = "Datos obtenido exitosamente.", data = DatoUsuarioDto });
        }

        // Crear un usuario con sus datos
        [HttpPost]
        public async Task<IActionResult> PostDatosUsuario([FromBody] DatoUsuarioDto datoUsuarioDto)
        {
            if (datoUsuarioDto == null)
            {
                return BadRequest(new { message = "Los datos del usuario no son válidos." });
            }

            var datos = new DatoUsuario
            {
                Id = 0,
                Nombre = datoUsuarioDto.Nombre,
                Apellido = datoUsuarioDto.Apellido,
                Correo = datoUsuarioDto.Correo,
                Telefono = datoUsuarioDto.Telefono
            };

            await _service.Agregar(datos);
            return CreatedAtAction(nameof(GetDatosUsuario), new { id = datos.Id }, new { message = "Datos del usuario creado exitosamente." });
        }

        // Actualizar un datos de un usuario
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDatosUsuarios(int id, [FromBody] DatoUsuarioDto datoUsuarioDto)
        {
            if (id != datoUsuarioDto.Id)
            {
                return BadRequest(new { message = "El ID del usuario no coincide." });
            }

            var datos = new DatoUsuario
            {
                Id = datoUsuarioDto.Id,
                Nombre = datoUsuarioDto.Nombre,
                Apellido = datoUsuarioDto.Apellido,
                Correo = datoUsuarioDto.Correo,
                Telefono = datoUsuarioDto.Telefono
            };

            await _service.Actualizar(datos);
            return Ok(new { message = "Datos del usuario actualizado exitosamente." });
        }

        // Eliminar un Usuario
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            await _service.Eliminar(id);
            return Ok(new { message = "Usuario eliminado exitosamente." });
        }
    }
}

