using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LinkU.Areas.Identity.Data;

public class ApplicationUser : IdentityUser
{
      [StringLength(450)]
      public string? Address { get; set; }

      [StringLength(450)]
      public string? Avatar { get; set; }

      [DataType(DataType.Date)]
      [Display(Name = "Created At")]
      public DateOnly? CreatedAt { get; set; }

      [DataType(DataType.Date)]
      [Display(Name = "Updated At")]
      public DateOnly? UpdatedAt { get; set; }
}
