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
    public class CiudadesController : Controller
    {
        private readonly MusicaContext _context;

        public CiudadesController(MusicaContext context)
        {
            _context = context;
        }

        // GET: Ciudades
        public async Task<IActionResult> Index()
        {
            var musicaContext = _context.Ciudads.Include(c => c.IdDepartamentoNavigation).Include(c => c.IdPaisNavigation);
            return View(await musicaContext.ToListAsync());
        }

        // GET: Ciudades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudad = await _context.Ciudads
                .Include(c => c.IdDepartamentoNavigation)
                .Include(c => c.IdPaisNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciudad == null)
            {
                return NotFound();
            }

            return View(ciudad);
        }

        // GET: Ciudades/Create
        public IActionResult Create()
        {
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Nombre");
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Nombre");
            return View();
        }

        // POST: Ciudades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,IdPais,IdDepartamento")] Ciudad ciudad)
        {
            if (!string.IsNullOrEmpty(ciudad.Nombre))
            {
                ciudad.UsuarioRegistro = "SIS457";
                ciudad.FechaRegistro = DateTime.Now;
                ciudad.Estado = 1;
                _context.Add(ciudad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", ciudad.IdDepartamento);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Id", ciudad.IdPais);
            return View(ciudad);
        }

        // GET: Ciudades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudad = await _context.Ciudads.FindAsync(id);
            if (ciudad == null)
            {
                return NotFound();
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Nombre", ciudad.IdDepartamento);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Nombre", ciudad.IdPais);
            return View(ciudad);
        }

        // POST: Ciudades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,IdPais,IdDepartamento")] Ciudad ciudad)
        {
            if (id != ciudad.Id)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(ciudad.Nombre))
            {
                try
                {
                    ciudad.UsuarioRegistro = "SIS457";
                    ciudad.FechaRegistro = DateTime.Now;
                    ciudad.Estado = 1;
                    _context.Update(ciudad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CiudadExists(ciudad.Id))
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
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", ciudad.IdDepartamento);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Id", ciudad.IdPais);
            return View(ciudad);
        }

        // GET: Ciudades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudad = await _context.Ciudads
                .Include(c => c.IdDepartamentoNavigation)
                .Include(c => c.IdPaisNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciudad == null)
            {
                return NotFound();
            }

            return View(ciudad);
        }

        // POST: Ciudades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ciudad = await _context.Ciudads.FindAsync(id);
            if (ciudad != null)
            {
                _context.Ciudads.Remove(ciudad);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CiudadExists(int id)
        {
            return _context.Ciudads.Any(e => e.Id == id);
        }
    }
}
