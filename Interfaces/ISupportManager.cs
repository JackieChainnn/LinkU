using System.Security.Claims;
using LinkU.Models;

namespace LinkU.Interfaces;

public interface ISupportManager
{
      /* 
      * Support Request
      * - Create
      * - Edit
      * - Delete
      * - Get
      * - List
      */
      Task<SupportRequest> CreateSupportRequestAsync(string title, string description, string userId);

      Task<SupportRequest> EditSupportRequestAsync(string id, string title, string description, ClaimsPrincipal user);

      Task<bool> DeleteSupportRequestAsync(string id, ClaimsPrincipal user);

      Task<SupportRequest> GetSupportRequestAsync(string id, ClaimsPrincipal user);

      Task<List<SupportRequest>> ListSupportRequestsAsync(ClaimsPrincipal user);

      /*
      * Agent
      * - List all support requests
      */
      Task<List<SupportRequest>> ListAllSupportRequestsAsync(ClaimsPrincipal user);

      string GetUserId(ClaimsPrincipal user);
}
