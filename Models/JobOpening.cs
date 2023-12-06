using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinkU.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkU.Models;

[Table("JobOpening")]
public partial class JobOpening
{
    [Key]
    [Column("ID")]
    public string Id { get; set; } = null!;

    [Required]
    [StringLength(450)]
    public string? Titile { get; set; }

    [StringLength(450)]
    public string? Description { get; set; }

    [Display(Name = "Status")]
    [EnumDataType(typeof(JobOpeningStatus))]
    public JobOpeningStatus Status { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Open Date")]
    public DateOnly? OpenDate { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Close Date")]
    public DateOnly? CloseDate { get; set; }

    [Column("OwnerID")]
    [StringLength(450)]
    public string? OwnerId { get; set; }

    [Column("DepartmentID")]
    [StringLength(450)]
    public string? DepartmentId { get; set; }

    public int? TotalOpening { get; set; }

    public int? ExperienceYears { get; set; }

    [InverseProperty("JobOpening")]
    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    [ForeignKey("DepartmentId")]
    [InverseProperty("JobOpenings")]
    public virtual Department? Department { get; set; }

    [ForeignKey("OwnerId")]
    [InverseProperty("JobOpenings")]
    public virtual Employee? Owner { get; set; }

    [ForeignKey("JobOpeningId")]
    [InverseProperty("JobOpenings")]
    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}

public enum JobOpeningStatus
{
    Open,
    Closed,
    Suspended
}