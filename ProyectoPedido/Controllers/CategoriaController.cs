using ProyectoPedido.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoPedido.Models;

namespace ProyectoPedido.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task<IActionResult> ListadoCategoria()
        {
            var categorias = await _context.Categorias.ToListAsync();

            return Ok(categorias);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCategoria([FromBody] Categoria categoria)
        {
            var nombreMayuscula = categoria.Nombres?.Trim().ToUpper();

            var existeCategoria = await _context.Categorias.AnyAsync(e => e.Nombres == nombreMayuscula);

            if (!existeCategoria)
            {
                var nuevaCategoria = new Categoria
                {
                    Nombres = nombreMayuscula,
                };
                _context.Add(nuevaCategoria);
                await _context.SaveChangesAsync();
                return Ok("categoria guardada");

            }

            return Ok();

        }


        [HttpPut("{categoriaId}")]
        public async Task<IActionResult> EditarCategoria(int categoriaID, [FromBody] Categoria categoria)
        {
            var nombreMayuscula = categoria.Nombres?.Trim().ToUpper(); // guardamos el nombre en mayuscula

            var editarCategoria = await _context.Categorias.Where(e => e.CategoriaId == categoriaID).SingleOrDefaultAsync();
            // le decimos que busque en el contexto de categorias un id que coincida con el parametro que le esto pasando

            if (editarCategoria == null)
            {
                return Ok("la categoria que quiere editar no existe");
            }
            ;

            var existeNombre = await _context.Categorias.AnyAsync(e => e.Nombres == nombreMayuscula && e.CategoriaId != categoriaID);

            // si Nombre es igual a la variable NombreMayuscula y  que sea distintp al id guardado
            if (!existeNombre)
            {
                editarCategoria.Nombres = nombreMayuscula;
                await _context.SaveChangesAsync();

                return Ok("categoria editada exitosamente");
            }

            return Ok("Ya existe otra categoria con ese nombre");
        }






        [HttpDelete("{categoriaId}")]
        public async Task<IActionResult> Eliminar(int categoriaId)
        {
            var categoria = await _context.Categorias.FindAsync(categoriaId);
            // pedimos que busque  la categoría directamente por su Id
            if (categoria == null)
            {
                return NotFound("Categoria no encontrada");
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return NoContent();




        }

        [HttpGet("{categoriaId}")]
        public async Task<IActionResult> ObtenerCategoria(int categoriaId)
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == categoriaId);

            if (categoria == null)
            {
                return NotFound("Categoria no encontrada");
            }

            return Ok(categoria);
        }

    }


}

