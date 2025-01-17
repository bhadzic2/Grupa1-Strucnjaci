﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpaceCat;
using SpaceCat.Data;

namespace SpaceCat.Controllers
{
    public class KomentarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KomentarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Komentar
        public async Task<IActionResult> Index()
        {
            return View(await _context.Komentar.ToListAsync());
        }
        [Authorize(Roles = "admin")]
        // GET: Komentar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var komentar = await _context.Komentar
                .FirstOrDefaultAsync(m => m.ID == id);
            if (komentar == null)
            {
                return NotFound();
            }

            return View(komentar);
        }
        [Authorize(Roles = "admin")]
        // GET: Komentar/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        // POST: Komentar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Text,VrijemeObjave")] Komentar komentar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(komentar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(komentar);
        }
        [Authorize(Roles = "admin")]
        // GET: Komentar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var komentar = await _context.Komentar.FindAsync(id);
            if (komentar == null)
            {
                return NotFound();
            }
            return View(komentar);
        }
        [Authorize(Roles = "admin")]
        // POST: Komentar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Text,VrijemeObjave")] Komentar komentar)
        {
            if (id != komentar.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(komentar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KomentarExists(komentar.ID))
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
            return View(komentar);
        }
        [Authorize(Roles = "admin")]
        // GET: Komentar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var komentar = await _context.Komentar
                .FirstOrDefaultAsync(m => m.ID == id);
            if (komentar == null)
            {
                return NotFound();
            }

            return View(komentar);
        }
        [Authorize(Roles = "admin")]
        // POST: Komentar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var komentar = await _context.Komentar.FindAsync(id);
            _context.Komentar.Remove(komentar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KomentarExists(int id)
        {
            return _context.Komentar.Any(e => e.ID == id);
        }
    }
}
