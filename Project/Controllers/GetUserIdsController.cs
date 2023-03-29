using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public class GetUserIdsController : Controller
    {
        private readonly GetUserIdContext _context;

        public GetUserIdsController(GetUserIdContext context)
        {
            _context = context;
        }

        // GET: GetUserIds
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetUserId.ToListAsync());
        }

        // GET: GetUserIds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var getUserId = await _context.GetUserId
                .FirstOrDefaultAsync(m => m.Id == id);
            if (getUserId == null)
            {
                return NotFound();
            }

            return View(getUserId);
        }

        // GET: GetUserIds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GetUserIds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId")] GetUserId getUserId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(getUserId);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(getUserId);
        }

        // GET: GetUserIds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var getUserId = await _context.GetUserId.FindAsync(id);
            if (getUserId == null)
            {
                return NotFound();
            }
            return View(getUserId);
        }

        // POST: GetUserIds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId")] GetUserId getUserId)
        {
            if (id != getUserId.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(getUserId);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GetUserIdExists(getUserId.Id))
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
            return View(getUserId);
        }

        // GET: GetUserIds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var getUserId = await _context.GetUserId
                .FirstOrDefaultAsync(m => m.Id == id);
            if (getUserId == null)
            {
                return NotFound();
            }

            return View(getUserId);
        }

        // POST: GetUserIds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var getUserId = await _context.GetUserId.FindAsync(id);
            _context.GetUserId.Remove(getUserId);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GetUserIdExists(int id)
        {
            return _context.GetUserId.Any(e => e.Id == id);
        }
    }
}
