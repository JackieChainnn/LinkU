using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LinkU.Areas.Identity.Data;
using LinkU.Models;
using Microsoft.AspNetCore.Identity;
using LinkU.Interfaces;
using LinkU.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace LinkU.Controllers
{
    public class SupportRequestController : Controller
    {
        private readonly ISupportManager _supportManager;
        public SupportRequestController(ISupportManager supportManager)
        {
            _supportManager = supportManager;
        }

        // GET: SupportRequest
        public async Task<IActionResult> Index()
        {
            // return support requests of current user
            try
            {
                var supportRequests = await _supportManager.ListSupportRequestsAsync(User);
                return View(supportRequests);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }

        // GET: SupportRequest/Details/5
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var supportRequest = await _supportManager.GetSupportRequestAsync(id, User);
                if (supportRequest == null)
                {
                    return NotFound();
                }
                return View(supportRequest);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\nBy user: " + _supportManager.GetUserId(User));
                return NotFound();
            }
        }

        // GET: SupportRequest/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SupportRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description")] SupportViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _supportManager.CreateSupportRequestAsync(model.Title, model.Description, User);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return NotFound();
                }
            }
            return View(model);
        }

        // GET: SupportRequest/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                var supportRequest = await _supportManager.GetSupportRequestAsync(id, User);
                if (supportRequest == null)
                {
                    return NotFound();
                }

                return View(supportRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }

        // POST: SupportRequest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Description")] SupportViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _supportManager.EditSupportRequestAsync(id, model.Title, model.Description, User);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return NotFound();
                }
            }
            return View(model);
        }

        // GET: SupportRequest/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var supportRequest = await _supportManager.GetSupportRequestAsync(id, User);
                return View(supportRequest);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound();
            }

        }

        // POST: SupportRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var result = await _supportManager.DeleteSupportRequestAsync(id, User);
                if (!result)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }
    }
}
