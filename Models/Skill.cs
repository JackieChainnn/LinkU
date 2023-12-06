using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinkU.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkU.Models;

[Table("Skill")]
public partial class Skill
{
    [Key]
    [Column("ID")]
    public string Id { get; set; } = null!;

    [Column("DepartmentID")]
    [StringLength(450)]
    public string? DepartmentId { get; set; }

    [Required]
    [StringLength(450)]
    public string Name { get; set; } = null!;

    [StringLength(450)]
    public string? Description { get; set; }

    [ForeignKey("DepartmentId")]
    [InverseProperty("Skills")]
    public virtual Department? Department { get; set; }

    [ForeignKey("SkillId")]
    [InverseProperty("Skills")]
    public virtual ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();

    [ForeignKey("SkillId")]
    [InverseProperty("Skills")]
    public virtual ICollection<JobOpening> JobOpenings { get; set; } = new List<JobOpening>();
}
