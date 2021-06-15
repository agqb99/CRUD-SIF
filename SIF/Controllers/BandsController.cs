using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIF.Data;
using SIF.Models;

namespace SIF.Controllers
{
    public class BandsController : Controller
    {
        private readonly ApplicationDbContext db;
        public BandsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {

            return View(await db.Bands.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var band = await db.Bands.FirstOrDefaultAsync(d => d.BandId == id);
            if (band == null)
            {
                return NotFound();
            }
            return View(band);
        }


        //Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Band band)
        {
            if (ModelState.IsValid)
            {
                db.Add(band);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(band);
        }

        //Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var band = await db.Bands.FindAsync(id);

            if (band == null)
            {
                return NotFound();
            }

            return View(band);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Band band)
        {
            if (id != band.BandId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(band);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(band);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var band = await db.Bands.FirstOrDefaultAsync(d => d.BandId == id);
            if (band == null)
            {
                return NotFound();
            }

            return View(band);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var band = await db.Bands.FindAsync(id);
            db.Bands.Remove(band);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    

}
}
