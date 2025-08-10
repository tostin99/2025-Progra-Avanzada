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
    public class AnimalesController : Controller
    {
        private readonly VetContext _context;

        public AnimalesController(VetContext context)
        {
            _context = context;
        }

        // GET: Animales
        public async Task<IActionResult> Index()
        {
            var vetContext = _context.Animales.Include(a => a.Cliente).Include(a => a.TipoAnimal);
            return View(await vetContext.ToListAsync());
        }

        // GET: Animales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animales = await _context.Animales
                .Include(a => a.Cliente)
                .Include(a => a.TipoAnimal)
                .FirstOrDefaultAsync(m => m.AnimalId == id);
            if (animales == null)
            {
                return NotFound();
            }

            return View(animales);
        }

        // GET: Animales/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId");
            ViewData["TipoAnimalId"] = new SelectList(_context.TiposAnimales, "TipoAnimalId", "TipoAnimalId");
            return View();
        }

        // POST: Animales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalId,Nombre,FechaNacimiento,Sexo,TipoAnimalId,ClienteId,Notas")] Animales animales)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animales);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", animales.ClienteId);
            ViewData["TipoAnimalId"] = new SelectList(_context.TiposAnimales, "TipoAnimalId", "TipoAnimalId", animales.TipoAnimalId);
            return View(animales);
        }

        // GET: Animales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animales = await _context.Animales.FindAsync(id);
            if (animales == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", animales.ClienteId);
            ViewData["TipoAnimalId"] = new SelectList(_context.TiposAnimales, "TipoAnimalId", "TipoAnimalId", animales.TipoAnimalId);
            return View(animales);
        }

        // POST: Animales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalId,Nombre,FechaNacimiento,Sexo,TipoAnimalId,ClienteId,Notas")] Animales animales)
        {
            if (id != animales.AnimalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animales);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalesExists(animales.AnimalId))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", animales.ClienteId);
            ViewData["TipoAnimalId"] = new SelectList(_context.TiposAnimales, "TipoAnimalId", "TipoAnimalId", animales.TipoAnimalId);
            return View(animales);
        }

        // GET: Animales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animales = await _context.Animales
                .Include(a => a.Cliente)
                .Include(a => a.TipoAnimal)
                .FirstOrDefaultAsync(m => m.AnimalId == id);
            if (animales == null)
            {
                return NotFound();
            }

            return View(animales);
        }

        // POST: Animales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animales = await _context.Animales.FindAsync(id);
            if (animales != null)
            {
                _context.Animales.Remove(animales);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalesExists(int id)
        {
            return _context.Animales.Any(e => e.AnimalId == id);
        }
    }
}
