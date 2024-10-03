using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using apiPrueba.src.Modelo;
using apiPrueba.src.Data; // Ensure this is the correct namespace for ApplicacionDBContext

namespace apiPrueba.src.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicacionDBController _context; // Corrected spelling
        
        public UsuarioController(ApplicacionDBController context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(){
            var usuario = _context.usuarios.ToList();
            return Ok(usuario);
        }
        [HttpGet("{id}")]
        public IActionResult GetId(int id){
            var usuario = _context.usuarios.FirstOrDefault(x => x.Id == id);
            if(usuario == null){
                return NotFound();
            }
            return Ok(usuario);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario){
            if(ModelState.IsValid){
                _context.usuarios.Add(usuario);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute]int id, [FromBody] Usuario usuario){
            var usuarioDB = _context.usuarios.Find(id);
            if(usuarioDB == null){
                return NotFound();
            }
            var userToUpdate = _context.usuarios.FirstOrDefault(x => x.Id == id);
            if(userToUpdate == null){
                return NotFound();
            }
            userToUpdate.Nombre = usuario.Nombre;
            userToUpdate.RUT = usuario.RUT;
            userToUpdate.email = usuario.email;
            userToUpdate.genero = usuario.genero;
            _context.usuarios.Update(userToUpdate);
            _context.SaveChanges();
            return Ok(userToUpdate);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            var usuario = _context.usuarios.FirstOrDefault(x=> x.Id == id);
            if(usuario == null){
                return NotFound();
            }
            _context.usuarios.Remove(usuario);
            _context.SaveChanges();
            return Ok();
        }
        
    }
}