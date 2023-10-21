using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_PRUEBAS.Models;
using Microsoft.AspNetCore.Cors;

namespace API_PRUEBAS.Controllers
{


    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        public readonly DbIotContext _context;

        public UsuarioController(DbIotContext context)
        {
            _context = context;
        }

        // GET : OBTENER TODOS LOS USUARIOS
        [HttpGet]
        [Route("listarUsuario")]
        public IActionResult ListarUsuario()
        {
            List<Usuario> listaUsuario = new List<Usuario>();
            try
            {
                listaUsuario = _context.Usuarios.Include(c => c.Eventos).ToList();
                return StatusCode(StatusCodes.Status200OK, listaUsuario);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

        }



        // GET : OBTENER TODOS LOS EVENTOS
        [HttpGet]
        [Route("listarEvento")]
        public IActionResult listarEventos()
        {
            List<Evento> listarEventos = new List<Evento>();
            try
            {
                listarEventos = _context.Eventos.ToList();
                return StatusCode(StatusCodes.Status200OK, listarEventos);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

        }

        // GET : OBTENER UN USUARIO POR ID
        [HttpGet]
        [Route("obtenerUsuario/{id}")]
        public IActionResult ObtenerUsuario(string id)
        {
            Usuario usuario = _context.Usuarios.Find(id);
            if(usuario == null)
            {
                return BadRequest("No hay Usuario");
            }

            try
            {
               
                usuario = _context.Usuarios.Include(c => c.Eventos).Where(predicate: c => c.Id == id).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, usuario);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

        }

        // POST : CREAR UN USUARIO
        [HttpPost]
        [Route("crearUsuario")]
        public IActionResult CrearUsuario([FromBody] Usuario usuario)
        {
            try
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, usuario);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

        }


        // POST : CREAR UN USUARIO
        [HttpPut]
        [Route("editarUsuario")]
        public IActionResult editar([FromBody] Usuario usuario)
        {

            Usuario usuarioupdate = _context.Usuarios.Find(usuario.Id);

            if (usuario == null)
            {
                return BadRequest("No hay Usuario");
            }
            try
            {
                usuarioupdate.Nombre = usuario.Nombre is null ? usuarioupdate.Nombre : usuario.Nombre;
                usuarioupdate.Apellido = usuario.Apellido is null ? usuarioupdate.Apellido : usuario.Apellido;
                usuarioupdate.Email = usuario.Email is null ? usuarioupdate.Email : usuario.Email;
                usuarioupdate.Clave = usuario.Clave is null ? usuarioupdate.Clave : usuario.Clave;
                _context.Usuarios.Update(usuarioupdate);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, usuario);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

        }

        // DELETE : ELIMINAR UN USUARIO
        [HttpDelete]
        [Route("eliminarUsuario/{id}")]
        public IActionResult eliminarUsuario(string id)
        {
            Usuario usuario = _context.Usuarios.Find(id);
            if (usuario == null)
            {
                return BadRequest("No hay Usuario");
            }
            try
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new {mensaje = "Eliminado"});
            }
            catch (System.Exception)
            {
                return BadRequest();
            }   
        }


        //POST : CREAR EVENTO ABRIR
        [HttpPost]
        [Route("abrir")]
        public IActionResult abrir([FromBody] string usuarioId)
        {
            try
            {
                var evento = new Evento
                {
                    UsuarioId = usuarioId,
                    FechaCreacion = DateTime.Now,
                    Evento1 = "Abrir"
                };

                _context.Eventos.Add(evento);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, evento);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        //POST : CREAR EVENTO CERRAR
        [HttpPost]
        [Route("cerrar")]
        public IActionResult cerrar([FromBody] string usuarioId)
        {
            try
            {
                var evento = new Evento
                {
                    UsuarioId = usuarioId,
                    FechaCreacion = DateTime.Now,
                    Evento1 = "Cerrar"
                };

                _context.Eventos.Add(evento);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, evento);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }



    }
}
