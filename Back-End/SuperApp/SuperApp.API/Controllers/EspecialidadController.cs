using Microsoft.AspNetCore.Mvc;
using SuperApp.Services;
using SuperApp.Services.DTOs;
using SuperApp.Services.Sevices;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadController(EspecialidadServices especialidad) : ControllerBase
    {
        private readonly EspecialidadServices _especialidad = especialidad;

        // GET: api/<EspecialidadController>
        [HttpGet]
        public async Task<IEnumerable<MostrarEspecialidadDTO>> Get()
        {
            var lst = await _especialidad.GetAll();
            return lst;
        }

        // GET api/<EspecialidadController>/5
        [HttpGet("{id}")]
        public async Task<MostrarEspecialidadDTO> Get(int id)
        {
            return await _especialidad.Find(id);
        }

        // POST api/<EspecialidadController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrearEspecialidadDTO especialidad)
        {
            if(especialidad == null)
            {
                return BadRequest("Datos vacios");
            }
            string result = await _especialidad.Create(especialidad);
            return Ok(result);
        }

        // PUT api/<EspecialidadController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EspecialidadController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string result=await _especialidad.Delete(id);
            return Ok(result);
        }

        
    }
}
