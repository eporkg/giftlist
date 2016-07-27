using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gavelister.Data;
using Gavelister.Models;
using Microsoft.AspNetCore.Authorization;

namespace Gavelister.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministrationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdministrationController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Gifts
        public async Task<IActionResult> Index()
        {
            var gifts = await _context.Gift.ToListAsync();
            var sorted = gifts.OrderBy(x => x.SortOrder);
            return View(sorted);
        }

        // GET: Gifts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _context.Gift.SingleOrDefaultAsync(m => m.Id == id);
            if (gift == null)
            {
                return NotFound();
            }

            return View(gift);
        }

        // GET: Gifts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AmountBought,AmountRequested,Description,Url,SortOrder")] Gift gift)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gift);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(gift);
        }

        // GET: Gifts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _context.Gift.SingleOrDefaultAsync(m => m.Id == id);
            if (gift == null)
            {
                return NotFound();
            }
            return View(gift);
        }

        // POST: Gifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AmountRequested,Description,Url,SortOrder")] Gift gift)
        {
            if (id != gift.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiftExists(gift.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(gift);
        }

        // GET: Gifts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var gift = await _context.Gift.SingleOrDefaultAsync(m => m.Id == id);
            _context.Gift.Remove(gift);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool GiftExists(int id)
        {
            return _context.Gift.Any(e => e.Id == id);
        }
    }
}
