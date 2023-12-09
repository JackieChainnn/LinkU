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
      Task<SupportRequest> CreateSupportRequestAsync(string title, string description, ClaimsPrincipal user);

      Task<SupportRequest> EditSupportRequestAsync(string id, string title, string description, ClaimsPrincipal user);

      Task<bool> DeleteSupportRequestAsync(string id, ClaimsPrincipal user);

      Task<SupportRequest> GetSupportRequestAsync(string id, ClaimsPrincipal user);

      Task<List<SupportRequest>> ListSupportRequestsAsync(ClaimsPrincipal user);

      string GetUserId(ClaimsPrincipal user);

      /*
      * Support Response
      * - Create
      //// - Edit !  Nobody can edit a support response
      * - Delete
      * - Get
      * - List
      */
      Task<SupportResponse> CreateSupportResponseAsync(string supportRequestId, string title, string description, ClaimsPrincipal user);
      Task<bool> DeleteSupportResponseAsync(string id, ClaimsPrincipal user);
      Task<SupportResponse> GetSupportResponseAsync(string id, ClaimsPrincipal user);
      Task<List<SupportResponse>> ListSupportResponsesAsync(ClaimsPrincipal user);

}
