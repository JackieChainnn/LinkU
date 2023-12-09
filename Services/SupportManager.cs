using System.Security.Claims;
using LinkU.Areas.Identity.Data;
using LinkU.Interfaces;
using LinkU.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LinkU.Services;

public class SupportManager : ISupportManager
{
      private readonly AppIdentityDbContext _context;
      private readonly UserManager<ApplicationUser> _userManager;

      public SupportManager(AppIdentityDbContext context,
            UserManager<ApplicationUser> userManager)
      {
            _context = context;
            _userManager = userManager;
      }
      //  ClaimsPrincipal is used to access the user's information in a secure and standardized way.
      public async Task<SupportRequest> CreateSupportRequestAsync(string title, string description, ClaimsPrincipal user)
      {

            try
            {
                  var userId = _userManager.GetUserId(user) ?? throw new InvalidOperationException("User not found.");
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
            catch (Exception e)
            {
                  throw new DbUpdateConcurrencyException(e.Message);
            }
      }

      public async Task<bool> DeleteSupportRequestAsync(string id, ClaimsPrincipal user)
      {
            try
            {
                  var userId = _userManager.GetUserId(user) ??
                        throw new InvalidOperationException("User not found.");
                  var supportRequest = await _context.SupportRequests.FindAsync(id)
                        ?? throw new DbUpdateConcurrencyException("Support request not found.");

                  // employee can delete any support request
                  if (user.IsInRole("Employee") || supportRequest.CustomerId == userId)
                  {
                        _context.SupportRequests.Remove(supportRequest);
                        await _context.SaveChangesAsync();
                  }
                  else
                  {
                        throw new InvalidOperationException("User not authorized." + userId);
                  }

                  return true;
            }
            catch (Exception e)
            {
                  throw new DbUpdateConcurrencyException(e.Message);
            }
      }

      public async Task<SupportRequest> EditSupportRequestAsync(string id, string title, string description, ClaimsPrincipal user)
      {
            try
            {
                  string userId = _userManager.GetUserId(user) ??
                        throw new InvalidOperationException("User not found.");

                  var supportRequest = await _context.SupportRequests.FindAsync(id)
                        ?? throw new DbUpdateConcurrencyException("Support request not found.");

                  // employee can edit any support request
                  if (user.IsInRole("Employee") || supportRequest.CustomerId == userId)
                  {
                        supportRequest.Title = title;
                        supportRequest.Description = description;
                        supportRequest.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);

                        await _context.SaveChangesAsync();
                  }
                  else
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

      public async Task<List<SupportRequest>> ListSupportRequestsAsync(ClaimsPrincipal user)
      {
            var userId = _userManager.GetUserId(user) ?? throw new InvalidOperationException("User not found.");

            // employee can get any support request
            if (user.IsInRole("Employee"))
            {
                  return await _context.SupportRequests.OrderByDescending(s => s.CreatedAt).ToListAsync();
            }
            return await _context.SupportRequests.Where(s => s.CustomerId == userId)
                  .OrderByDescending(s => s.CreatedAt)
                  .ToListAsync();
      }

      public async Task<SupportRequest> GetSupportRequestAsync(string id, ClaimsPrincipal user)
      {
            try
            {
                  var supportRequest = await _context.SupportRequests.FindAsync(id) ??
                        throw new DbUpdateConcurrencyException("Support request not found.");

                  var userId = _userManager.GetUserId(user) ??
                        throw new InvalidOperationException("User not found.");

                  // employee can get any support request
                  if (user.IsInRole("Employee") || supportRequest.CustomerId == userId)
                  {
                        return await _context.SupportRequests.FindAsync(id) ??
                              throw new DbUpdateConcurrencyException("Support request not found.");
                  }
                  else
                  {
                        throw new InvalidOperationException("User not authorized." + userId);
                  }
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

      /*
      * Support Response
      * - Create
      * - Delete
      * - Get
      * - List
      */
      [Authorize(Roles = "Employee")]
      public async Task<SupportResponse> CreateSupportResponseAsync(string supportRequestId, string title, string description, ClaimsPrincipal user)
      {
            try
            {
                  var userId = _userManager.GetUserId(user) ?? throw new InvalidOperationException("User not found.");

                  var supportResponse = new SupportResponse
                  {
                        AgentId = userId,
                        RequestId = supportRequestId,
                        Title = title,
                        Description = description
                  };
                  _context.Add(supportResponse);

                  // update support request status
                  var supportRequest = await _context.SupportRequests.FindAsync(supportRequestId) ??
                        throw new DbUpdateConcurrencyException("Support request not found.");
                  supportRequest.Status = SupportRequestStatus.Pending;

                  await _context.SaveChangesAsync();

                  return supportResponse;
            }
            catch (Exception e)
            {
                  throw new DbUpdateConcurrencyException(e.Message);
            }
      }

      public async Task<SupportResponse> GetSupportResponseAsync(string id, ClaimsPrincipal user)
      {
            try
            {
                  // return support response include agent info
                  var supportResponse = await _context.SupportResponses
                        .FirstOrDefaultAsync(s => s.Id == id) ??
                        throw new DbUpdateConcurrencyException("Support response not found.");

                  var userId = _userManager.GetUserId(user) ??
                        throw new InvalidOperationException("User not found.");

                  // employee can get any support response
                  if (user.IsInRole("Employee") || supportResponse.AgentId == userId)
                  {
                        return await _context.SupportResponses.FindAsync(id) ??
                              throw new DbUpdateConcurrencyException("Support response not found.");
                  }
                  else
                  {
                        throw new InvalidOperationException("User not authorized." + userId);
                  }
            }
            catch (Exception e)
            {
                  throw new DbUpdateConcurrencyException(e.Message);
            }
      }

      public Task<List<SupportResponse>> ListSupportResponsesAsync(ClaimsPrincipal user)
      {
            try
            {
                  var userId = _userManager.GetUserId(user) ?? throw new InvalidOperationException("User not found.");

                  // employee can get any support response
                  if (user.IsInRole("Employee"))
                  {
                        return _context.SupportResponses.OrderByDescending(s => s.CreatedAt).ToListAsync();
                  }
                  return _context.SupportResponses.Where(s => s.AgentId == userId)
                      .OrderByDescending(s => s.CreatedAt)
                      .ToListAsync();
            }
            catch (Exception e)
            {
                  throw new DbUpdateConcurrencyException(e.Message);
            }
      }

      [Authorize(Roles = "Employee")]
      public async Task<bool> DeleteSupportResponseAsync(string id, ClaimsPrincipal user)
      {
            try
            {
                  var userId = GetUserId(user) ?? throw new InvalidOperationException("User not found.");
                  var supportResponse = await _context.SupportResponses.FindAsync(id) ??
                        throw new DbUpdateConcurrencyException("Support response not found.");

                  if (supportResponse.AgentId == userId)
                  {
                        _context.SupportResponses.Remove(supportResponse);
                        await _context.SaveChangesAsync();
                  }
                  else
                  {
                        throw new InvalidOperationException("User not authorized." + userId);
                  }
                  return true;
            }
            catch (Exception e)
            {
                  throw new DbUpdateConcurrencyException(e.Message);
            }

      }
}
