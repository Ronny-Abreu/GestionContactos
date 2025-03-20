using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GestionContactos.Frontend.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GestionContactos.Api.Dtos;

namespace GestionContactos.Frontend.Controllers
{
    public class DatoUsuarioController : Controller
    {
        private readonly HttpClient _httpClient;

        public DatoUsuarioController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        // GET: /DatosUsuario/Index
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/DatoUsuario");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var datosUsuariosResponse = JsonConvert.DeserializeObject<ApiResponse<List<DatoUsuarioDto>>>(result);

                var datos = datosUsuariosResponse?.Data;
                return View(datos);
            }
            else
            {
                return View("Error");
            }
        }

        // GET: /DatoUsuario/Details/id
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/DatoUsuario/{id}");
            if (response.IsSuccessStatusCode)
            {
                var datoUsuarioJson = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<DatoUsuarioDto>>(datoUsuarioJson);

                if (apiResponse?.Data != null)
                {
                    var datoUsuarioDto = apiResponse.Data;

                    var dato = new DatoUsuario
                    {
                        Id = datoUsuarioDto.Id,
                        Nombre = datoUsuarioDto.Nombre,
                        Apellido = datoUsuarioDto.Apellido,
                        Correo = datoUsuarioDto.Correo,
                        Telefono = datoUsuarioDto.Telefono
                    };

                    return View(dato);
                }
            }
            return NotFound();
        }



        // GET: /DatoUsuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /DatoUsuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DatoUsuario datoUsuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var content = new StringContent(JsonConvert.SerializeObject(datoUsuario), Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync("api/DatoUsuario", content);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Usuario creado correctamente";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Hubo un problema al crear el usuario.";
                        return View(datoUsuario);
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Ocurrió un error inesperado: {ex.Message}";
                    return View(datoUsuario);
                }
            }
            return View(datoUsuario);
        }




        // GET: /DatoUsuario/Edit/id
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/DatoUsuario/{id}");
            if (response.IsSuccessStatusCode)
            {
                var datoUsuarioJson = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<DatoUsuarioDto>>(datoUsuarioJson);

                if (apiResponse?.Data != null)
                {
                    var datoUsuarioDto = apiResponse.Data;

                    var dato = new DatoUsuario
                    {
                        Id = datoUsuarioDto.Id,
                        Nombre = datoUsuarioDto.Nombre,
                        Apellido = datoUsuarioDto.Apellido,
                        Correo = datoUsuarioDto.Correo,
                        Telefono = datoUsuarioDto.Telefono
                    };

                    return View(dato);
                }
            }
            return NotFound();
        }

        // POST: /DatoUsuario/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(DatoUsuario datoUsuario)
        {
            if (datoUsuario.Id <= 0)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(datoUsuario), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/DatoUsuario/{datoUsuario.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Producto actualizado exitosamente.";
                    TempData["ProductId"] = datoUsuario.Id; //Guardar edicion
                    return RedirectToAction("Edit", new { id = datoUsuario.Id });
                }
            }
            return View("Edit", datoUsuario);
        }




        // GET: /DatoUsuario/Delete/id
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"api/DatoUsuario/{id}");
            if (response.IsSuccessStatusCode)
            {
                var datoUsuarioJson = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<DatoUsuarioDto>>(datoUsuarioJson);

                if (apiResponse?.Data != null)
                {
                    var datoUsuarioDto = apiResponse.Data;

                    var dato = new DatoUsuario
                    {
                        Id = datoUsuarioDto.Id,
                        Nombre = datoUsuarioDto.Nombre,
                        Apellido = datoUsuarioDto.Apellido,
                        Correo = datoUsuarioDto.Correo,
                        Telefono = datoUsuarioDto.Telefono
                    };

                    return View(dato);
                }
            }
            return NotFound();
        }

        // POST: /DatoUsuario/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/DatoUsuario/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Hubo un problema al eliminar el usuario.";
            return RedirectToAction(nameof(Index));
        }

    }
}
