﻿using Microsoft.AspNetCore.Mvc;
using SuperApp.Services.DTOs;
using SuperApp.Services.Sevices;
using System.Collections.Specialized;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(UsuarioServices usuario) : ControllerBase
    {
        private readonly UsuarioServices _usuario = usuario;

        // GET: api/<UsuarioController>
        [HttpGet]
        public IEnumerable<MostrarUsuarioDTO> Get()
        {
            var lst= _usuario.GetAll();
            return lst;
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IActionResult Post([FromBody] CrearUsuarioDTO usuarioDTO)
        {
            if(usuarioDTO == null)
            {
                return BadRequest("Datos Vacios");
            }
            string result = _usuario.Create(usuarioDTO);
            return Ok(result);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}