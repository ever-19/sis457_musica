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
    public class VentasController : Controller
    {
        private readonly MusicaContext _context;

        public VentasController(MusicaContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var musicaContext = _context.Venta.Include(v => v.IdCiudadNavigation).Include(v => v.IdDepartamentoNavigation).Include(v => v.IdPaisNavigation).Include(v => v.IdUsuarioNavigation);
            return View(await musicaContext.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventum = await _context.Venta
                .Include(v => v.IdCiudadNavigation)
                .Include(v => v.IdDepartamentoNavigation)
                .Include(v => v.IdPaisNavigation)
                .Include(v => v.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ventum == null)
            {
                return NotFound();
            }

            return View(ventum);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Nombre");
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Nombre");
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Nombre");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUsuario,Direccion,Celular,IdPais,IdDepartamento,IdCiudad")] Ventum ventum)
        {
            if (!string.IsNullOrEmpty(ventum.Direccion))
            {
                ventum.UsuarioRegistro = User.Identity?.Name;
                ventum.FechaRegistro = DateTime.Now;
                ventum.Estado = 1;
                _context.Add(ventum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Id", ventum.IdCiudad);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", ventum.IdDepartamento);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Id", ventum.IdPais);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", ventum.IdUsuario);
            return View(ventum);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventum = await _context.Venta.FindAsync(id);
            if (ventum == null)
            {
                return NotFound();
            }
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Nombre", ventum.IdCiudad);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Nombre", ventum.IdDepartamento);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Id", ventum.IdPais);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Nombre", ventum.IdUsuario);
            return View(ventum);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUsuario,Direccion,Celular,IdPais,IdDepartamento,IdCiudad")] Ventum ventum)
        {
            if (id != ventum.Id)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(ventum.Direccion))
            {
                try
                {
                    ventum.UsuarioRegistro = User.Identity?.Name;
                    ventum.FechaRegistro = DateTime.Now;
                    ventum.Estado = 1;
                    _context.Update(ventum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentumExists(ventum.Id))
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
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Id", ventum.IdCiudad);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", ventum.IdDepartamento);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Id", ventum.IdPais);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", ventum.IdUsuario);
            return View(ventum);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventum = await _context.Venta
                .Include(v => v.IdCiudadNavigation)
                .Include(v => v.IdDepartamentoNavigation)
                .Include(v => v.IdPaisNavigation)
                .Include(v => v.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ventum == null)
            {
                return NotFound();
            }

            return View(ventum);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ventum = await _context.Venta.FindAsync(id);
            if (ventum != null)
            {
                _context.Venta.Remove(ventum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentumExists(int id)
        {
            return _context.Venta.Any(e => e.Id == id);
        }
    }
}
