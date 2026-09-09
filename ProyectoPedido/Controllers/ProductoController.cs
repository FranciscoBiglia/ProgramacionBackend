using ProyectoPedido.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoPedido.Models;

namespace ProyectoPedido.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task<IActionResult> ListadoProducto()
        {
            var listadoProducto = await _context.Productos.Include(p => p.Categoria).ToListAsync();

            var productoMostrar = listadoProducto.Select(p => new VistaProducto
            {
                ProductoId = p.ProductoId,
                NombreProducto = p.Nombres,
                DescripcionProducto = p.Descripcion,
                CostoProducto = p.Costo,
                VentaProducto = p.Venta,
                stockProducto = p.stock,
                CategoriaId = p.CategoriaId,

                NombreCategoria = p.Categoria?.Nombres
            }).ToList();
            return Ok(productoMostrar);
        }

        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] Producto producto)
        {
            var nombreMayuscula = producto.Nombres?.Trim().ToUpper();

            var existeProducto = await _context.Productos.AnyAsync(e => e.Nombres == nombreMayuscula);

            if (!existeProducto)
            {
                var nuevoProducto = new Producto
                {
                    Nombres = nombreMayuscula,
                    Descripcion = producto.Descripcion,
                    Costo = producto.Costo,
                    Venta = producto.Venta,
                    stock = producto.stock,
                    CategoriaId = producto.CategoriaId
                };
                _context.Add(nuevoProducto);
                await _context.SaveChangesAsync();
                return Ok("producto guardado");

            }

            return Ok();

        }

        [HttpPut("{productoId}")]
        public async Task<IActionResult> EditarProducto(int productoId, [FromBody] Producto producto)
        {
            var nombreMayuscula = producto.Nombres?.Trim().ToUpper(); // guardamos el nombre en mayuscula

            var editarProducto = await _context.Productos.Where(e => e.ProductoId == productoId).SingleOrDefaultAsync();
            // le decimos que busque en el contexto de productos un id que coincida con el parametro que le esto pasando

            if (editarProducto == null)
            {
                return Ok("el Producto que quiere editar no existe");
            }
            ;

            var existeNombre = await _context.Productos.AnyAsync(e => e.Nombres == nombreMayuscula && e.ProductoId != productoId);

            // si Nombre es igual a la variable NombreMayuscula y que sea distinto al id guardado
            if (!existeNombre)
            {
                editarProducto.Nombres = nombreMayuscula;
                editarProducto.Descripcion = producto.Descripcion;
                editarProducto.Costo = producto.Costo;
                editarProducto.Venta = producto.Venta;
                editarProducto.stock = producto.stock;
                editarProducto.CategoriaId = producto.CategoriaId;

                await _context.SaveChangesAsync();
                return Ok("producto editado exitosamente");
            }

            return Ok("Ya existe otro producto con ese nombre");
        }

        [HttpDelete("{productoId}")]
        public async Task<IActionResult> Eliminar(int productoId)
        {
            var producto = await _context.Productos.FindAsync(productoId);
            // pedimos que busque el producto directamente por su Id
            if (producto == null)
            {
                return NotFound("Producto no encontrado");
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpGet("{productoId}")]
        public async Task<IActionResult> ObtenerProducto(int productoId)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(c => c.ProductoId == productoId);

            if (producto == null)
            {
                return NotFound("Producto no encontrado");
            }

            return Ok(producto);
        }
    }
}