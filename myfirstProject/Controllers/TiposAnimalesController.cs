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
    public class TiposAnimalesController : Controller
    {
        private readonly VetContext _context;

        public TiposAnimalesController(VetContext context)
        {
            _context = context;
        }

        // GET: TiposAnimales
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposAnimales.ToListAsync());
        }

        // GET: TiposAnimales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposAnimales = await _context.TiposAnimales
                .FirstOrDefaultAsync(m => m.TipoAnimalId == id);
            if (tiposAnimales == null)
            {
                return NotFound();
            }

            return View(tiposAnimales);
        }

        // GET: TiposAnimales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposAnimales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoAnimalId,Nombre,Descripcion")] TiposAnimales tiposAnimales)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tiposAnimales);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tiposAnimales);
        }

        // GET: TiposAnimales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposAnimales = await _context.TiposAnimales.FindAsync(id);
            if (tiposAnimales == null)
            {
                return NotFound();
            }
            return View(tiposAnimales);
        }

        // POST: TiposAnimales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoAnimalId,Nombre,Descripcion")] TiposAnimales tiposAnimales)
        {
            if (id != tiposAnimales.TipoAnimalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiposAnimales);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiposAnimalesExists(tiposAnimales.TipoAnimalId))
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
            return View(tiposAnimales);
        }

        // GET: TiposAnimales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposAnimales = await _context.TiposAnimales
                .FirstOrDefaultAsync(m => m.TipoAnimalId == id);
            if (tiposAnimales == null)
            {
                return NotFound();
            }

            return View(tiposAnimales);
        }

        // POST: TiposAnimales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tiposAnimales = await _context.TiposAnimales.FindAsync(id);
            if (tiposAnimales != null)
            {
                _context.TiposAnimales.Remove(tiposAnimales);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiposAnimalesExists(int id)
        {
            return _context.TiposAnimales.Any(e => e.TipoAnimalId == id);
        }
    }
}
