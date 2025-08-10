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
    public class CitasController : Controller
    {
        private readonly VetContext _context;

        public CitasController(VetContext context)
        {
            _context = context;
        }

        // GET: Citas
        public async Task<IActionResult> Index()
        {
            var vetContext = _context.Citas.Include(c => c.Animal).Include(c => c.Doctor);
            return View(await vetContext.ToListAsync());
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citas = await _context.Citas
                .Include(c => c.Animal)
                .Include(c => c.Doctor)
                .FirstOrDefaultAsync(m => m.CitaId == id);
            if (citas == null)
            {
                return NotFound();
            }

            return View(citas);
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animales, "AnimalId", "AnimalId");
            ViewData["DoctorId"] = new SelectList(_context.Doctores, "DoctorId", "DoctorId");
            return View();
        }

        // POST: Citas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CitaId,DoctorId,AnimalId,Inicio,Fin,Estado,FechaCreacion")] Citas citas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(citas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_context.Animales, "AnimalId", "AnimalId", citas.AnimalId);
            ViewData["DoctorId"] = new SelectList(_context.Doctores, "DoctorId", "DoctorId", citas.DoctorId);
            return View(citas);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citas = await _context.Citas.FindAsync(id);
            if (citas == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_context.Animales, "AnimalId", "AnimalId", citas.AnimalId);
            ViewData["DoctorId"] = new SelectList(_context.Doctores, "DoctorId", "DoctorId", citas.DoctorId);
            return View(citas);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("CitaId,DoctorId,AnimalId,Inicio,Fin,Estado,FechaCreacion")] Citas citas)
        {
            if (id != citas.CitaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitasExists(citas.CitaId))
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
            ViewData["AnimalId"] = new SelectList(_context.Animales, "AnimalId", "AnimalId", citas.AnimalId);
            ViewData["DoctorId"] = new SelectList(_context.Doctores, "DoctorId", "DoctorId", citas.DoctorId);
            return View(citas);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citas = await _context.Citas
                .Include(c => c.Animal)
                .Include(c => c.Doctor)
                .FirstOrDefaultAsync(m => m.CitaId == id);
            if (citas == null)
            {
                return NotFound();
            }

            return View(citas);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var citas = await _context.Citas.FindAsync(id);
            if (citas != null)
            {
                _context.Citas.Remove(citas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitasExists(long id)
        {
            return _context.Citas.Any(e => e.CitaId == id);
        }
    }
}
