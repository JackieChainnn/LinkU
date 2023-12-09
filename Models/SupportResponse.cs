using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinkU.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkU.Models;

[Table("SupportResponse")]
public partial class SupportResponse
{
    [Column("ID")]
    public string Id { get; private set; }

    [Column("AgentID")]
    [StringLength(450)]
    public string AgentId { get; set; } = null!;

    [Column("RequestID")]
    [StringLength(450)]
    public string? RequestId { get; set; }

    [StringLength(450)]
    public string? Title { get; set; }

    [StringLength(450)]
    public string? Description { get; set; }

    public DateOnly? CreatedAt { get; set; }

    [ForeignKey("AgentId")]
    [InverseProperty("SupportResponses")]
    public virtual Employee Agent { get; set; } = null!;

    [ForeignKey("RequestId")]
    [InverseProperty("SupportResponses")]
    public virtual SupportRequest? Request { get; set; }
    public SupportResponse()
    {
        Id = Guid.NewGuid().ToString()[..6];
    }
}
