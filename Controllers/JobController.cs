using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoApp.Models;

namespace DemoApp.Controllers
{
    public class JobController : Controller
    {
        private DemoContext _context;

        public JobController(DemoContext context)
        {
            _context = context;
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            var DemoContext = _context.Jobs.Include(c => c.AssignedTo);
            return View(await DemoContext.ToListAsync());
        }

        // GET: Job/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToAssignId = id;
            
            ViewBag.UserId = new SelectList(_context.Users,"UserId","FullName", userToAssignId);
            return View();
        }

        // POST: Job/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("JobId","Title","DueDate","UserId","AssignedTo")] Job newJob, int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return NotFound();
                }
                
                var user = _context.Users.FirstOrDefault(b => b.UserId == id);
                newJob.AssignedTo = user;
                _context.Jobs.Add(newJob);
                _context.SaveChanges();
                return RedirectToAction("Index", "User");
            }

            ViewBag.UserId = new SelectList(_context.Users,"UserId","FullName");
            return View(newJob);
        }

        // GET: Job/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .Include(s => s.AssignedTo)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.JobId == id);

            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // GET: Job/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs.SingleOrDefaultAsync(m => m.JobId == id);

            if (job == null)
            {
                return NotFound();
            }
            
            var userToAssignId = id;

            ViewBag.UserId = new SelectList(_context.Users, "UserId", "FullName",userToAssignId);
            return View(job);
        }

        // POST: Job/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = _context.Users.FirstOrDefault(b => b.UserId == id);
                
            var jobToUpdate = await _context.Jobs.SingleOrDefaultAsync(s => s.JobId == id);
            jobToUpdate.AssignedTo = user;

            if (await TryUpdateModelAsync<Job>(
                jobToUpdate,
                "",
                s => s.Title, s => s.DueDate, s => s.IsComplete, s => s.AssignedTo))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }

            ViewBag.UserId = new SelectList(_context.Users, "UserId", "FullName");
            return View(jobToUpdate);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.JobId == id);
            if (job == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(job);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Jobs
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.JobId == id);
            if (job == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }
        private bool UserExists(int id)
        {
            return _context.Jobs.Any(e => e.JobId == id);
        }
    }
}