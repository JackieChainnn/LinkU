using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinkU.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkU.Models;

[Table("Interview")]
public partial class Interview
{
    [Key]
    [Column("ID")]
    public string Id { get; set; } = null!;

    [Column("InterviewerID")]
    [StringLength(450), Required]
    [Display(Name = "Interviewer ID")]
    public string InterviewerId { get; set; } = null!;

    [Column("ApplicationID")]
    [StringLength(450), Required]
    [Display(Name = "Application ID")]
    public string ApplicationId { get; set; } = null!;

    [DataType(DataType.Date)]
    [Display(Name = "Start Date")]
    public DateOnly? StartDate { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "End Date")]
    public DateOnly? EndDate { get; set; }

    [Display(Name = "Status")]
    [EnumDataType(typeof(InterviewStatus))]
    public InterviewStatus Status { get; set; }

    [Column("FeedbackID")]
    [StringLength(450)]
    [Display(Name = "Feedback ID")]
    public string? FeedbackId { get; set; }

    [ForeignKey("ApplicationId")]
    [InverseProperty("Interviews")]
    public virtual Application Application { get; set; } = null!;

    [ForeignKey("FeedbackId")]
    [InverseProperty("Interviews")]
    public virtual Feedback? Feedback { get; set; }

    [ForeignKey("InterviewerId")]
    [InverseProperty("Interviews")]
    public virtual Employee Interviewer { get; set; } = null!;
}

public enum InterviewStatus
{
    Pending,
    Approved,
    Rejected
}