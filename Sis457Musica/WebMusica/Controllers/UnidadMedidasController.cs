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
    public class UnidadMedidasController : Controller
    {
        private readonly MusicaContext _context;

        public UnidadMedidasController(MusicaContext context)
        {
            _context = context;
        }

        // GET: UnidadMedidas
        public async Task<IActionResult> Index()
        {
            return View(await _context.UnidadMedida.ToListAsync());
        }

        // GET: UnidadMedidas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadMedidum = await _context.UnidadMedida
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidadMedidum == null)
            {
                return NotFound();
            }

            return View(unidadMedidum);
        }

        // GET: UnidadMedidas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnidadMedidas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] UnidadMedidum unidadMedidum)
        {
            if (!string.IsNullOrEmpty(unidadMedidum.Nombre))
            {
                unidadMedidum.UsuarioRegistro = User.Identity?.Name;
                unidadMedidum.FechaRegistro = DateTime.Now;
                unidadMedidum.Estado = 1;
                _context.Add(unidadMedidum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unidadMedidum);
        }

        // GET: UnidadMedidas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadMedidum = await _context.UnidadMedida.FindAsync(id);
            if (unidadMedidum == null)
            {
                return NotFound();
            }
            return View(unidadMedidum);
        }

        // POST: UnidadMedidas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Estado")] UnidadMedidum unidadMedidum)
        {
            if (id != unidadMedidum.Id)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(unidadMedidum.Nombre))
            {
                try
                {
                    unidadMedidum.UsuarioRegistro = User.Identity?.Name;
                    unidadMedidum.FechaRegistro = DateTime.Now;
                    
                    _context.Update(unidadMedidum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadMedidumExists(unidadMedidum.Id))
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
            return View(unidadMedidum);
        }

        // GET: UnidadMedidas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadMedidum = await _context.UnidadMedida
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidadMedidum == null)
            {
                return NotFound();
            }

            return View(unidadMedidum);
        }

        // POST: UnidadMedidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unidadMedidum = await _context.UnidadMedida.FindAsync(id);
            if (unidadMedidum != null)
            {
                _context.UnidadMedida.Remove(unidadMedidum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnidadMedidumExists(int id)
        {
            return _context.UnidadMedida.Any(e => e.Id == id);
        }
    }
}
