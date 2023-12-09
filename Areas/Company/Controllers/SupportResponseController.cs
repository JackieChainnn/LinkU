using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LinkU.Areas.Identity.Data;
using LinkU.Models;
using Microsoft.AspNetCore.Authorization;
using LinkU.Interfaces;

namespace LinkU.Areas.Company.Controllers
{
    [Area("Company")]
    [Authorize(Roles = "Employee")]
    public class SupportResponseController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly ISupportManager _supportManager;

        public SupportResponseController(AppIdentityDbContext context,
            ISupportManager supportManager)
        {
            _context = context;
            _supportManager = supportManager;
        }

        // GET: Company/SupportResponse
        public async Task<IActionResult> Index()
        {
            IEnumerable<SupportRequest> supportRequests = await _supportManager.ListSupportRequestsAsync(User);
            if (supportRequests == null)
            {
                return NotFound();
            }
            return View(supportRequests);
        }

        // GET: Company/SupportResponse/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supportResponse = await _context.SupportResponses
                .Include(s => s.Agent)
                .Include(s => s.Request)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supportResponse == null)
            {
                return NotFound();
            }

            return View(supportResponse);
        }

        // GET: Company/SupportResponse/Create
        public IActionResult Create()
        {
            ViewData["RequestId"] = new SelectList(_context.SupportRequests, "Id", "Id");
            return View();
        }

        // POST: Company/SupportResponse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestId,Title,Description,CreatedAt")] SupportResponse supportResponse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supportResponse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RequestId"] = new SelectList(_context.SupportRequests, "Id", "Id", supportResponse.RequestId);
            return View(supportResponse);
        }

        // GET: Company/SupportResponse/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supportResponse = await _context.SupportResponses.FindAsync(id);
            if (supportResponse == null)
            {
                return NotFound();
            }
            ViewData["AgentId"] = new SelectList(_context.Set<Employee>(), "Id", "Id", supportResponse.AgentId);
            ViewData["RequestId"] = new SelectList(_context.SupportRequests, "Id", "Id", supportResponse.RequestId);
            return View(supportResponse);
        }

        // POST: Company/SupportResponse/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,AgentId,RequestId,Title,Description,CreatedAt")] SupportResponse supportResponse)
        {
            if (id != supportResponse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supportResponse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupportResponseExists(supportResponse.Id))
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
            ViewData["AgentId"] = new SelectList(_context.Set<Employee>(), "Id", "Id", supportResponse.AgentId);
            ViewData["RequestId"] = new SelectList(_context.SupportRequests, "Id", "Id", supportResponse.RequestId);
            return View(supportResponse);
        }

        // GET: Company/SupportResponse/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supportResponse = await _context.SupportResponses
                .Include(s => s.Agent)
                .Include(s => s.Request)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supportResponse == null)
            {
                return NotFound();
            }

            return View(supportResponse);
        }

        // POST: Company/SupportResponse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var supportResponse = await _context.SupportResponses.FindAsync(id);
            if (supportResponse != null)
            {
                _context.SupportResponses.Remove(supportResponse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupportResponseExists(string id)
        {
            return _context.SupportResponses.Any(e => e.Id == id);
        }
    }
}
