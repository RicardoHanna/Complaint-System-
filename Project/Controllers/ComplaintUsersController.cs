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
    public class ComplaintUsersController : Controller
    {
        private readonly ComplaintsUserContext _context;

        public ComplaintUsersController(ComplaintsUserContext context)
        {
            _context = context;
        }

        // GET: ComplaintUsers
        public async Task<IActionResult> Index()
        {
            var complaintsUserContext = _context.ComplaintUser.Include(c => c.IdentityUser);
            return View(await complaintsUserContext.ToListAsync());
        }

        // GET: ComplaintUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintUser = await _context.ComplaintUser
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.ComplaintId == id);
            if (complaintUser == null)
            {
                return NotFound();
            }

            return View(complaintUser);
        }

        // GET: ComplaintUsers/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            return View();
        }

        // POST: ComplaintUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,ComplaintId")] ComplaintUser complaintUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaintUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", complaintUser.UserId);
            return View(complaintUser);
        }

        // GET: ComplaintUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintUser = await _context.ComplaintUser.FindAsync(id);
            if (complaintUser == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", complaintUser.UserId);
            return View(complaintUser);
        }

        // POST: ComplaintUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("UserId,ComplaintId")] ComplaintUser complaintUser)
        {
            if (id != complaintUser.ComplaintId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaintUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintUserExists(complaintUser.ComplaintId))
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
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", complaintUser.UserId);
            return View(complaintUser);
        }

        // GET: ComplaintUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintUser = await _context.ComplaintUser
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.ComplaintId == id);
            if (complaintUser == null)
            {
                return NotFound();
            }

            return View(complaintUser);
        }

        // POST: ComplaintUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var complaintUser = await _context.ComplaintUser.FindAsync(id);
            _context.ComplaintUser.Remove(complaintUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintUserExists(int? id)
        {
            return _context.ComplaintUser.Any(e => e.ComplaintId == id);
        }
    }
}
