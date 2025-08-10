using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myfirstProject.Models.VetEntities;

namespace myfirstProject.Controllers
{
    public class HistorialCitasController : Controller
    {
        private readonly VetContext _context;

        public HistorialCitasController(VetContext context)
        {
            _context = context;
        }

        // GET: HistorialCitas
        public async Task<IActionResult> Index()
        {
            var vetContext = _context.HistorialCitas.Include(h => h.Cita);
            return View(await vetContext.ToListAsync());
        }

        // GET: HistorialCitas/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialCitas = await _context.HistorialCitas
                .Include(h => h.Cita)
                .FirstOrDefaultAsync(m => m.HistorialId == id);
            if (historialCitas == null)
            {
                return NotFound();
            }

            return View(historialCitas);
        }

        // GET: HistorialCitas/Create
        public IActionResult Create()
        {
            ViewData["CitaId"] = new SelectList(_context.Citas, "CitaId", "CitaId");
            return View();
        }

        // POST: HistorialCitas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HistorialId,CitaId,Accion,InicioAnt,FinAnt,InicioNuevo,FinNuevo,Nota,FechaRegistro")] HistorialCitas historialCitas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historialCitas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CitaId"] = new SelectList(_context.Citas, "CitaId", "CitaId", historialCitas.CitaId);
            return View(historialCitas);
        }

        // GET: HistorialCitas/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialCitas = await _context.HistorialCitas.FindAsync(id);
            if (historialCitas == null)
            {
                return NotFound();
            }
            ViewData["CitaId"] = new SelectList(_context.Citas, "CitaId", "CitaId", historialCitas.CitaId);
            return View(historialCitas);
        }

        // POST: HistorialCitas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("HistorialId,CitaId,Accion,InicioAnt,FinAnt,InicioNuevo,FinNuevo,Nota,FechaRegistro")] HistorialCitas historialCitas)
        {
            if (id != historialCitas.HistorialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historialCitas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistorialCitasExists(historialCitas.HistorialId))
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
            ViewData["CitaId"] = new SelectList(_context.Citas, "CitaId", "CitaId", historialCitas.CitaId);
            return View(historialCitas);
        }

        // GET: HistorialCitas/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialCitas = await _context.HistorialCitas
                .Include(h => h.Cita)
                .FirstOrDefaultAsync(m => m.HistorialId == id);
            if (historialCitas == null)
            {
                return NotFound();
            }

            return View(historialCitas);
        }

        // POST: HistorialCitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var historialCitas = await _context.HistorialCitas.FindAsync(id);
            if (historialCitas != null)
            {
                _context.HistorialCitas.Remove(historialCitas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistorialCitasExists(long id)
        {
            return _context.HistorialCitas.Any(e => e.HistorialId == id);
        }
    }
}
