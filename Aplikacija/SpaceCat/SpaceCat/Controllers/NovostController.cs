using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpaceCat;
using SpaceCat.Data;

namespace SpaceCat.Controllers
{
    public class NovostController : Controller
    {
        private readonly SpaceCatContext _context;

        public NovostController(SpaceCatContext context)
        {
            _context = context;
        }

        // GET: Novost
        public async Task<IActionResult> Index()
        {
            return View(await _context.Novost.ToListAsync());
        }

        // GET: Novost/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novost = await _context.Novost
                .FirstOrDefaultAsync(m => m.ID == id);
            if (novost == null)
            {
                return NotFound();
            }

            return View(novost);
        }

        // GET: Novost/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Novost/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Naslov,Text,VrijemeObjave")] Novost novost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(novost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(novost);
        }

        // GET: Novost/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novost = await _context.Novost.FindAsync(id);
            if (novost == null)
            {
                return NotFound();
            }
            return View(novost);
        }

        // POST: Novost/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Naslov,Text,VrijemeObjave")] Novost novost)
        {
            if (id != novost.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(novost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NovostExists(novost.ID))
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
            return View(novost);
        }

        // GET: Novost/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novost = await _context.Novost
                .FirstOrDefaultAsync(m => m.ID == id);
            if (novost == null)
            {
                return NotFound();
            }

            return View(novost);
        }

        // POST: Novost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var novost = await _context.Novost.FindAsync(id);
            _context.Novost.Remove(novost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NovostExists(int id)
        {
            return _context.Novost.Any(e => e.ID == id);
        }
    }
}
