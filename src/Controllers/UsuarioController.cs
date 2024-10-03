using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using apiPrueba.src.Modelo;
using apiPrueba.src.Data;

namespace apiPrueba.src.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicacionDBContext _context; // Cambiado de Controller a Context
        
        public UsuarioController(ApplicacionDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(){
            var usuarios = _context.usuarios.ToList();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id){
            var usuario = _context.usuarios.FirstOrDefault(x => x.Id == id);
            if (usuario == null){
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            
            if (usuario.Nombre.Length < 3 || usuario.Nombre.Length > 100)
            {
                return BadRequest("El nombre debe tener entre 3 y 100 caracteres.");
            }

            
            try
            {
                var addr = new System.Net.Mail.MailAddress(usuario.email);
                if (addr.Address != usuario.email)
                {
                    return BadRequest("Correo electrónico no válido.");
                }
            }
            catch
            {
                return BadRequest("Correo electrónico no válido.");
            }


            var generosValidos = new List<string> { "masculino", "femenino", "otro", "prefiero no decirlo" };
            if (!generosValidos.Contains(usuario.genero.ToLower()))
            {
                return BadRequest("El género debe ser 'masculino', 'femenino', 'otro' o 'prefiero no decirlo'.");
            }


            if (DateTime.TryParse(usuario.fechaNacimiento, out DateTime fechaNacimiento))
            {
                if (fechaNacimiento >= DateTime.Now)
                {
                    return BadRequest("La fecha de nacimiento debe ser anterior a la fecha actual.");
                }
            }
            else
            {
                return BadRequest("Formato de fecha de nacimiento inválido.");
            }

            var rutExistente = _context.usuarios.FirstOrDefault(x => x.RUT == usuario.RUT);
            if (rutExistente != null)
            {
                return Conflict("El RUT ya existe.");
            }


            if (ModelState.IsValid)
            {
                _context.usuarios.Add(usuario);
                _context.SaveChanges();
                return StatusCode(201, "Usuario creado exitosamente.");
            }

            return BadRequest("Datos inválidos.");
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Usuario usuario){
            var usuarioDB = _context.usuarios.Find(id);
            if (usuarioDB == null){
                return NotFound();
            }
            usuarioDB.Nombre = usuario.Nombre;
            usuarioDB.RUT = usuario.RUT;
            usuarioDB.email = usuario.email;
            usuarioDB.genero = usuario.genero;
            _context.usuarios.Update(usuarioDB);
            _context.SaveChanges();
            return Ok(usuarioDB);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            var usuario = _context.usuarios.FirstOrDefault(x => x.Id == id);
            if (usuario == null){
                return NotFound();
            }
            _context.usuarios.Remove(usuario);
            _context.SaveChanges();
            return Ok();
        }
    }
}
