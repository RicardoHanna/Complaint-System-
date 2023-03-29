using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Data;
using Project.Models;
using Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ComplaintsUserContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<IdentityUser> userManager;
        private readonly GetUserIdContext _con;

        public HomeController(ILogger<HomeController> logger, ComplaintsUserContext context,IWebHostEnvironment hostEnvironment ,UserManager<IdentityUser> userManager, GetUserIdContext con)        {
            _logger = logger;
            _context = context;
            webHostEnvironment = hostEnvironment;
            this.userManager = userManager;
            _con = con;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
           
            var userId = userManager.GetUserId(HttpContext.User);
                if (userId == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    IdentityUser user = userManager.FindByIdAsync(userId).Result;
                return View(user);
            }
                
            
        }
        

        public IActionResult New()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComplaintViewModel model, GetUserId user)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Complaint item = new Complaint
                {
                    ComplaintName = model.ComplaintName,
                   DescriptionComplaint = model.DescriptionComplaint,
                    LocationComplaint = model.LocationComplaint,
                   Path = uniqueFileName,
                };

                _context.Add(item);
                GetUserId comp1 = new GetUserId();
                ViewBag.userid = userManager.GetUserId(HttpContext.User);
                comp1.UserId = userManager.GetUserId(HttpContext.User);
              
                _con.GetUserId.Add(comp1);
                _con.SaveChanges();
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private string UploadedFile(ComplaintViewModel model)
        {
            string uniqueFileName = null;

            if (model.Path != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Path.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Path.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
