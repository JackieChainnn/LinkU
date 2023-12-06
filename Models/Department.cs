using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinkU.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkU.Models;

[Table("Department")]
[Index("Name", Name = "UQ__Departme__737584F6CAD530FA", IsUnique = true)]
public partial class Department
{
    [Key]
    [Column("ID")]
    public string Id { get; set; } = null!;

    [Required]
    [StringLength(450)]
    public string Name { get; set; } = null!;

    [DataType(DataType.Date)]
    [Display(Name = "Created At")]
    public DateOnly? CreatedAt { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [InverseProperty("Department")]
    public virtual ICollection<JobOpening> JobOpenings { get; set; } = new List<JobOpening>();

    [InverseProperty("Department")]
    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
