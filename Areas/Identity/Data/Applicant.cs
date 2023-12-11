using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinkU.Models;

namespace LinkU.Areas.Identity.Data;

public class Applicant : ApplicationUser
{
      [Display(Name = "Experience Years")]
      public int? ExperienceYears { get; set; }

      [Display(Name = "Status")]
      [EnumDataType(typeof(ApplicantStatus))]
      public ApplicantStatus Status { get; set; }

      [Column("HRId")]
      [StringLength(450)]
      [Display(Name = "HR")]
      public string? Hrid { get; set; }

      [InverseProperty("Applicant")]
      public virtual ICollection<Application> Applications { get; set; } = new List<Application>();


      [ForeignKey("Hrid")]
      [InverseProperty("Applicants")]
      public virtual Employee? Hr { get; set; }

      [InverseProperty("Customer")]
      public virtual ICollection<SupportRequest> SupportRequests { get; set; } = new List<SupportRequest>();

      [ForeignKey("ApplicantId")]
      [InverseProperty("Applicants")]
      public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}

public enum ApplicantStatus
{
      Active,
      Inactive,
      Hired,
      Banned
}