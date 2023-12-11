using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace LinkU.Areas.Identity.Data;

public class ApplicationUser : IdentityUser
{
      [StringLength(450)]
      public string? Address { get; set; }

      [NotMapped]
      [DataType(DataType.Upload)]
      public IFormFile? Avatar { get; set; }
      public string? AvatarPath { get; set; }

      // Documentation: applicant resume, company profile. 
      [NotMapped]
      [DataType(DataType.Upload)]
      public IFormFile? Documentation { get; set; }
      public string? DocumentationPath { get; set; }

      public DateOnly? Birthday { get; set; }
      [DataType(DataType.Date)]
      [Display(Name = "Created At")]
      public DateOnly? CreatedAt { get; set; }

      [DataType(DataType.Date)]
      [Display(Name = "Updated At")]
      public DateOnly? UpdatedAt { get; set; }
}
