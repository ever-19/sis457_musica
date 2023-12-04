using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMusica.Models;

namespace WebMusica.Controllers
{
    public class ProductosController : Controller
    {
        private readonly MusicaContext _context;

        public ProductosController(MusicaContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var musicaContext = _context.Productos.Include(p => p.IdCategoriaNavigation).Include(p => p.IdMarcaNavigation).Include(p => p.IdUnidadMedidaNavigation);
            return View(await musicaContext.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdMarcaNavigation)
                .Include(p => p.IdUnidadMedidaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Nombre");
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "Id", "Nombre");
            ViewData["IdUnidadMedida"] = new SelectList(_context.UnidadMedida, "Id", "Nombre");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Codigo,Nombre,Descripcion,Precio,CantidadExistente,UrlImagen,IdCategoria,IdMarca,IdUnidadMedida")] Producto producto)
        {
            if (!string.IsNullOrEmpty(producto.Nombre) && producto.CantidadExistente > -1)
            {
                producto.UsuarioRegistro = User.Identity?.Name;
                producto.FechaRegistro = DateTime.Now;
                producto.Estado = 1;
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Id", producto.IdCategoria);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "Id", "Id", producto.IdMarca);
            ViewData["IdUnidadMedida"] = new SelectList(_context.UnidadMedida, "Id", "Id", producto.IdUnidadMedida);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Nombre", producto.IdCategoria);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "Id", "Nombre", producto.IdMarca);
            ViewData["IdUnidadMedida"] = new SelectList(_context.UnidadMedida, "Id", "Nombre", producto.IdUnidadMedida);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,Nombre,Descripcion,Precio,CantidadExistente,UrlImagen,IdCategoria,IdMarca,IdUnidadMedida,Estado")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(producto.Nombre) && producto.Precio > 0 && producto.CantidadExistente > -1)
            {
                try
                {
                    producto.UsuarioRegistro = User.Identity?.Name;
                    producto.FechaRegistro = DateTime.Now;
                   
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Id", producto.IdCategoria);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "Id", "Id", producto.IdMarca);
            ViewData["IdUnidadMedida"] = new SelectList(_context.UnidadMedida, "Id", "Id", producto.IdUnidadMedida);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdMarcaNavigation)
                .Include(p => p.IdUnidadMedidaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
