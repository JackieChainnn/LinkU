using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinkU.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkU.Models;

[Table("Feedback")]
public partial class Feedback
{
    [Key]
    [Column("ID")]
    public string Id { get; set; } = null!;

    [DataType(DataType.Date)]
    public DateOnly? TimeStamp { get; set; }

    [Column("Feedback")]
    [StringLength(450)]
    public string? FeedbackContent { get; set; }

    [Column("OwnerID")]
    [StringLength(450)]
    [Display(Name = "Owner ID")]
    public string? OwnerId { get; set; }

    [InverseProperty("Feedback")]
    public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();

    [ForeignKey("OwnerId")]
    [InverseProperty("Feedbacks")]
    public virtual Employee? Owner { get; set; }
}
