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
    public class HorariosDoctorController : Controller
    {
        private readonly VetContext _context;

        public HorariosDoctorController(VetContext context)
        {
            _context = context;
        }

        // GET: HorariosDoctor
        public async Task<IActionResult> Index()
        {
            var vetContext = _context.HorariosDoctor.Include(h => h.Doctor);
            return View(await vetContext.ToListAsync());
        }

        // GET: HorariosDoctor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horariosDoctor = await _context.HorariosDoctor
                .Include(h => h.Doctor)
                .FirstOrDefaultAsync(m => m.HorarioId == id);
            if (horariosDoctor == null)
            {
                return NotFound();
            }

            return View(horariosDoctor);
        }

        // GET: HorariosDoctor/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctores, "DoctorId", "DoctorId");
            return View();
        }

        // POST: HorariosDoctor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HorarioId,DoctorId,DiaSemana,HoraInicio,HoraFin,DuracionMinutos")] HorariosDoctor horariosDoctor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horariosDoctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctores, "DoctorId", "DoctorId", horariosDoctor.DoctorId);
            return View(horariosDoctor);
        }

        // GET: HorariosDoctor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horariosDoctor = await _context.HorariosDoctor.FindAsync(id);
            if (horariosDoctor == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctores, "DoctorId", "DoctorId", horariosDoctor.DoctorId);
            return View(horariosDoctor);
        }

        // POST: HorariosDoctor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HorarioId,DoctorId,DiaSemana,HoraInicio,HoraFin,DuracionMinutos")] HorariosDoctor horariosDoctor)
        {
            if (id != horariosDoctor.HorarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horariosDoctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorariosDoctorExists(horariosDoctor.HorarioId))
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
            ViewData["DoctorId"] = new SelectList(_context.Doctores, "DoctorId", "DoctorId", horariosDoctor.DoctorId);
            return View(horariosDoctor);
        }

        // GET: HorariosDoctor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horariosDoctor = await _context.HorariosDoctor
                .Include(h => h.Doctor)
                .FirstOrDefaultAsync(m => m.HorarioId == id);
            if (horariosDoctor == null)
            {
                return NotFound();
            }

            return View(horariosDoctor);
        }

        // POST: HorariosDoctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horariosDoctor = await _context.HorariosDoctor.FindAsync(id);
            if (horariosDoctor != null)
            {
                _context.HorariosDoctor.Remove(horariosDoctor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorariosDoctorExists(int id)
        {
            return _context.HorariosDoctor.Any(e => e.HorarioId == id);
        }
    }
}
