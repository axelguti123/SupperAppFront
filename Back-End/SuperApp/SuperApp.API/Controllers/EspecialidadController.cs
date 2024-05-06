﻿using Microsoft.AspNetCore.Mvc;
using SuperApp.Services;
using SuperApp.Services.DTOs;
using SuperApp.Services.Sevices;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {
        private readonly EspecialidadServices _especialidad;

        public EspecialidadController(EspecialidadServices especialidad)
        {
            _especialidad = especialidad;
        }

        // GET: api/<EspecialidadController>
        [HttpGet]
        public IEnumerable<MostrarEspecialidadDTO> Get()
        {
            return _especialidad.GetAll();
        }

        // GET api/<EspecialidadController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EspecialidadController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EspecialidadController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EspecialidadController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        
    }
}