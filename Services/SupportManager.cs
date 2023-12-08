using System.Security.Claims;
using LinkU.Areas.Identity.Data;
using LinkU.Interfaces;
using LinkU.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LinkU.Services;

public class SupportManager : ISupportManager
{
      private readonly AppIdentityDbContext _context;
      private readonly UserManager<ApplicationUser> _userManager;
      private readonly ILogger<SupportManager> _logger;

      public SupportManager(AppIdentityDbContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<SupportManager> logger)
      {
            _context = context;
            _userManager = userManager;
            _logger = logger;
      }
      //  ClaimsPrincipal is used to access the user's information in a secure and standardized way.
      public async Task<SupportRequest> CreateSupportRequestAsync(string title, string description, string userId)
      {
            var supportRequest = new SupportRequest
            {
                  Title = title,
                  Description = description,
                  CustomerId = userId
            };

            _context.Add(supportRequest);
            await _context.SaveChangesAsync();

            return supportRequest;
      }

      public async Task<bool> DeleteSupportRequestAsync(string id, ClaimsPrincipal user)
      {
            try
            {
                  var userId = _userManager.GetUserId(user) ??
                        throw new InvalidOperationException("User not found.");
                  var supportRequest = await _context.SupportRequests.FindAsync(id)
                        ?? throw new DbUpdateConcurrencyException("Support request not found.");

                  if (supportRequest.CustomerId != userId)
                  {
                        throw new InvalidOperationException("User not authorized." + userId);
                  }

                  _context.SupportRequests.Remove(supportRequest);
                  await _context.SaveChangesAsync();
                  return true;
            }
            catch (Exception e)
            {
                  Console.WriteLine(e.Message);
                  return false;
            }
      }

      public async Task<SupportRequest> EditSupportRequestAsync(string id, string title, string description, ClaimsPrincipal user)
      {
            try
            {
                  var supportRequest = await _context.SupportRequests.FindAsync(id)
                        ?? throw new DbUpdateConcurrencyException("Support request not found.");

                  string userId = _userManager.GetUserId(user) ??
                        throw new InvalidOperationException("User not found.");

                  if (supportRequest.CustomerId != userId)
                  {
                        throw new InvalidOperationException("User not authorized." + userId);
                  }

                  supportRequest.Title = title;
                  supportRequest.Description = description;
                  supportRequest.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);

                  await _context.SaveChangesAsync();
                  return supportRequest;
            }
            catch (Exception e)
            {
                  throw new DbUpdateConcurrencyException(e.Message);
            }
      }

      public async Task<List<SupportRequest>> ListSupportRequestsAsync(ClaimsPrincipal user)
      {
            var userId = _userManager.GetUserId(user) ?? throw new InvalidOperationException("User not found.");
            return await _context.SupportRequests.Where(s => s.CustomerId == userId).ToListAsync();
      }

      public async Task<SupportRequest> GetSupportRequestAsync(string id, ClaimsPrincipal user)
      {
            try
            {
                  var supportRequest = await _context.SupportRequests.FindAsync(id) ??
                        throw new DbUpdateConcurrencyException("Support request not found.");

                  var userId = _userManager.GetUserId(user) ??
                        throw new InvalidOperationException("User not found.");
                  if (supportRequest.CustomerId != userId)
                  {
                        throw new InvalidOperationException("User not authorized." + userId);
                  }
                  return supportRequest;
            }
            catch (Exception e)
            {
                  throw new DbUpdateConcurrencyException(e.Message);
            }
      }

      public string GetUserId(ClaimsPrincipal user)
      {
            var userId = _userManager.GetUserId(user) ?? throw new InvalidOperationException("User not found.");
            return userId;
      }

      // Agent methods
      // list all support requests
      public Task<List<SupportRequest>> ListAllSupportRequestsAsync(ClaimsPrincipal user)
      {
            // check if user is employee
            bool isAgent = user.IsInRole("Employee");
            if (!isAgent)
            {
                  throw new InvalidOperationException("User not authorized.");
            }
            return _context.SupportRequests.ToListAsync();
      }
}
