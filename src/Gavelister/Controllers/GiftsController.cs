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
    [Authorize(Roles = "User")]
    public class GiftsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GiftsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Gifts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gift.ToListAsync());
        }

        public async Task<IActionResult> Buy(int id, int amountReserved)
        {
            if (amountReserved == 0)
                return View();

            var gift = await _context.Gift.SingleOrDefaultAsync(m => m.Id == id);
            var originalAmount = gift.AmountBought;
            var newAmount = originalAmount + amountReserved;
            if (newAmount > gift.AmountRequested)
                return BadRequest("Antallet overstiger ønsket antall");
            gift.AmountBought += newAmount;
            await _context.SaveChangesAsync();
            return View();
        }
    }
}
