using Microsoft.AspNetCore.Mvc;
using SuperApp.Services.DTOs;
using SuperApp.Services.Sevices;
using System.Collections.Generic;
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
        public async Task<JsonResult> Get()
        {
            var lst=await _usuario.GetAll();
            return new JsonResult(lst);
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public  async Task<IActionResult> Get(int id)
        {
            var result= await _usuario.Find(id);
            return Ok(result);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrearUsuarioDTO usuarioDTO)
        {
            if(usuarioDTO == null)
            {
                return BadRequest("Datos Vacios");
            }
            var result = await _usuario.Create(usuarioDTO);
            return Ok(result);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ModificarUsuarioDTO user)
        {
            if (id != user.IDUsuario)
            {
                return BadRequest("El id no coincide");
            }
            var response= await _usuario.Update(user);
            return Ok(response);
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result=await _usuario.Delete(id);
            return Ok(result);
        }
    }
}
