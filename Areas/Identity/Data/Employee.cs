using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinkU.Models;
using Microsoft.AspNetCore.Identity;

namespace LinkU.Areas.Identity.Data;

public class Employee : ApplicationUser
{
      [Required]
      [StringLength(450)]
      public string? DepartmentId { get; set; }

      [InverseProperty("Hr")]
      public virtual ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();

      [ForeignKey("DepartmentId")]
      [InverseProperty("Employees")]
      public virtual Department? Department { get; set; }

      [InverseProperty("Owner")]
      public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

      [InverseProperty("Interviewer")]
      public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();

      [InverseProperty("Owner")]
      public virtual ICollection<JobOpening> JobOpenings { get; set; } = new List<JobOpening>();

      [InverseProperty("Agent")]
      public virtual ICollection<SupportResponse> SupportResponses { get; set; } = new List<SupportResponse>();
}
