using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinkU.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkU.Models;

[Table("Application")]
[Index("ApplicantId", "JobOpeningId", Name = "unique_application", IsUnique = true)]
public partial class Application
{
    [Key]
    [Column("ID")]
    public string Id { get; set; } = null!;

    [Required]
    [Column("ApplicantID")]
    [Display(Name = "Applicant ID")]
    public string ApplicantId { get; set; } = null!;

    [Required]
    [Column("JobOpeningID")]
    [Display(Name = "Job Opening ID")]
    public string JobOpeningId { get; set; } = null!;

    [DataType(DataType.Date)]
    [Display(Name = "Created Date")]
    public DateOnly? CreatedDate { get; set; }

    [Display(Name = "Status")]
    [EnumDataType(typeof(ApplicationStatus))]
    public ApplicationStatus Status { get; set; }

    [Column("FeedbackID")]
    [StringLength(450)]
    [Display(Name = "Feedback ID")]
    public string? FeedbackId { get; set; }

    [ForeignKey("ApplicantId")]
    [InverseProperty("Applications")]
    public virtual Applicant Applicant { get; set; } = null!;

    [InverseProperty("Application")]
    public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();

    [ForeignKey("JobOpeningId")]
    [InverseProperty("Applications")]
    public virtual JobOpening JobOpening { get; set; } = null!;
}

public enum ApplicationStatus
{
    Scheduled,
    Selected,
    Rejected
}