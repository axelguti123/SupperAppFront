using Microsoft.AspNetCore.Mvc;
using SuperApp.AccesoDatos;
using SuperApp.Services.DTOs;
using SuperApp.Services.Sevices;
using SupperApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidaController(PartidaServices partida) : ControllerBase
    {
        private readonly PartidaServices _partidaServices=partida;
        // GET: api/<PartidaController>
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var response = await _partidaServices.GetAll();
            
            return new JsonResult(response);
        }

        // GET api/<PartidaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PartidaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PartidaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PartidaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
