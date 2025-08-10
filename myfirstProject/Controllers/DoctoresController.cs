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
    public class DoctoresController : Controller
    {
        private readonly VetContext _context;

        public DoctoresController(VetContext context)
        {
            _context = context;
        }

        // GET: Doctores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Doctores.ToListAsync());
        }

        // GET: Doctores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctores = await _context.Doctores
                .FirstOrDefaultAsync(m => m.DoctorId == id);
            if (doctores == null)
            {
                return NotFound();
            }

            return View(doctores);
        }

        // GET: Doctores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorId,Nombre,Especialidad,Activo")] Doctores doctores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctores);
        }

        // GET: Doctores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctores = await _context.Doctores.FindAsync(id);
            if (doctores == null)
            {
                return NotFound();
            }
            return View(doctores);
        }

        // POST: Doctores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoctorId,Nombre,Especialidad,Activo")] Doctores doctores)
        {
            if (id != doctores.DoctorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctoresExists(doctores.DoctorId))
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
            return View(doctores);
        }

        // GET: Doctores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctores = await _context.Doctores
                .FirstOrDefaultAsync(m => m.DoctorId == id);
            if (doctores == null)
            {
                return NotFound();
            }

            return View(doctores);
        }

        // POST: Doctores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctores = await _context.Doctores.FindAsync(id);
            if (doctores != null)
            {
                _context.Doctores.Remove(doctores);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctoresExists(int id)
        {
            return _context.Doctores.Any(e => e.DoctorId == id);
        }
    }
}
