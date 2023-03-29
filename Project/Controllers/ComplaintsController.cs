using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly ComplaintsUserContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly GetUserIdContext _con;

        public ComplaintsController(ComplaintsUserContext context, UserManager<IdentityUser> userManager,GetUserIdContext con)
        {
            _context = context;
            this.userManager = userManager;
            _con = con;
        }

       
        [Authorize(Roles = "Admin")]
        // GET: Complaints
        public async Task<IActionResult> Index(string searchName, string searchString)
        {
            IQueryable<string> genderQuery = from m in _context.Complaint
                                             orderby m.ComplaintName
                                             select m.ComplaintName;
            var complaints = from m in _context.Complaint
                           select m;

            var userId = userManager.GetUserId(HttpContext.User);
           

          

            if (!string.IsNullOrEmpty(searchString))
            {
                complaints = complaints.Where(s => s.LocationComplaint.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(searchName))
            {
                complaints = complaints.Where(x => x.ComplaintName == searchName);
            }

            var ComplaintGenderListM = new GenderListModel
            {
                 user = userManager.FindByIdAsync(userId).Result,
            ComplaintName = new SelectList(await genderQuery.Distinct().ToListAsync()),
                Complaints = await complaints.ToListAsync()
     
          
        };

            return View(ComplaintGenderListM);
        }

        // GET: Complaints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaint
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        public IActionResult SaveRecord(GetUserId user)
        {
            ApplicationUser app = new ApplicationUser();

            GetUserId comp1 = new GetUserId();
            comp1.UserId = userManager.GetUserId(HttpContext.User);

            _con.GetUserId.Add(comp1);
            _con.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Complaints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Complaints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ComplaintName,LocationComplaint,DescriptionComplaint,Path")] Complaint complaint)
        {
            ApplicationUser app = new ApplicationUser();
         
            if (ModelState.IsValid)
            {
                _context.Add(complaint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complaint);
        }

        // GET: Complaints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaint.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }
            return View(complaint);
        }

        // POST: Complaints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ComplaintName,LocationComplaint,DescriptionComplaint,path")] Complaint complaint)
        {
            if (id != complaint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintExists(complaint.Id))
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
            return View(complaint);
        }

        // GET: Complaints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaint
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complaint = await _context.Complaint.FindAsync(id);
            _context.Complaint.Remove(complaint);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintExists(int id)
        {
            return _context.Complaint.Any(e => e.Id == id);
        }
    }
}
