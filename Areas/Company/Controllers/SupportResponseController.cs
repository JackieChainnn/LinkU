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
using LinkU.ViewModels;

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
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<SupportResponse> supportResponses = await _supportManager.ListSupportResponsesAsync(User);
                if (supportResponses == null)
                {
                    return NotFound();
                }
                return View(supportResponses);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }

        // GET: Company/SupportResponse/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supportResponse = await _supportManager.GetSupportResponseAsync(id, User);
            if (supportResponse == null)
            {
                return NotFound();
            }

            return View(supportResponse);
        }

        // GET: Company/SupportResponse/Create
        public async Task<IActionResult> Create(string id)
        {
            try
            {
                // get support request by id to display title and description
                var supportRequest = await _supportManager.GetSupportRequestAsync(id, User);
                if (supportRequest == null)
                {
                    return NotFound();
                }
                var model = new SupportViewModel
                {
                    SupportRequestId = supportRequest.Id,
                    Title = supportRequest.Title,
                    Description = supportRequest.Description!
                };
                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound();
            }
        }

        // POST: Company/SupportResponse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupportRequestId,Title,Description")] SupportViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _supportManager.CreateSupportResponseAsync(model.SupportRequestId!, model.Title, model.Description, User);
                    // redirect to details of support response by id
                    if (response != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return NotFound();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return NotFound();
                }
            }
            return View(model);
        }

        // GET: Company/SupportResponse/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var supportResponse = await _supportManager.GetSupportResponseAsync(id, User);
                if (supportResponse == null)
                {
                    return NotFound();
                }

                return View(supportResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound();
            }
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
